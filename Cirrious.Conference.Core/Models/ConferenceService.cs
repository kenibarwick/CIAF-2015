﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Threading;
using Cirrious.Conference.Core.Interfaces;
using Cirrious.Conference.Core.Models.Raw;
using Cirrious.MvvmCross.Core;
using Cirrious.MvvmCross.ExtensionMethods;
using Cirrious.MvvmCross.Interfaces.Localization;
using Cirrious.MvvmCross.Interfaces.Platform;
using Cirrious.MvvmCross.Interfaces.ServiceProvider;
using Newtonsoft.Json;
using RestSharp;

namespace Cirrious.Conference.Core.Models
{
    public class ConferenceService
        : IConferenceService
            , IMvxServiceConsumer<IMvxResourceLoader>
            , IMvxServiceConsumer<IMvxSimpleFileStoreService>
    {
        private readonly FavoritesSaver _favoritesSaver = new FavoritesSaver();
        // a hashtable of favorites
        private IDictionary<string, SessionWithFavoriteFlag> _favoriteSessions;
        // is loading setup
        private bool _isLoading;

        public bool IsLoading
        {
            get { return _isLoading; }
            private set
            {
                _isLoading = value;
                FireLoadingChanged();
            }
        }

        public event EventHandler LoadingChanged;
        // the basic lists
        public IDictionary<string, SessionWithFavoriteFlag> Sessions { get; private set; }
        public IDictionary<string, Sponsor> Exhibitors { get; private set; }
        public IDictionary<string, Sponsor> Sponsors { get; private set; }
        public IDictionary<string, Team> Team { get; private set; }

        public IDictionary<string, SessionWithFavoriteFlag> GetCopyOfFavoriteSessions()
        {
            lock (this)
            {
                if (_favoriteSessions == null)
                    return new Dictionary<string, SessionWithFavoriteFlag>();

                var toReturn = new Dictionary<string, SessionWithFavoriteFlag>(_favoriteSessions);
                return toReturn;
            }
        }

        public event EventHandler FavoritesSessionsChanged;

        public void BeginAsyncLoad()
        {
            IsLoading = true;
            MvxAsyncDispatcher.BeginAsync(Load);
        }

        public void DoSyncLoad()
        {
            IsLoading = true;
            Load();
        }

        private void FireLoadingChanged()
        {
            var handler = LoadingChanged;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        private void FireFavoriteSessionsChanged()
        {
            var handler = FavoritesSessionsChanged;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        private void Load()
        {
            LoadSessions();
            LoadFavorites();
            // LoadSponsors();
            LoadTeam();

            IsLoading = false;
        }

        private void LoadSponsors()
        {
            var file = this.GetService<IMvxResourceLoader>().GetTextResource("ConfResources/Sponsors.txt");
            var items = JsonConvert.DeserializeObject<List<Sponsor>>(file);
            // Sponsors = items.ToDictionary(x => x.Name);
            Sponsors = items.Where(x => x.Level != "Exhibitor").ToDictionary(x => x.Name);
            Exhibitors = items.Where(x => x.Level == "Exhibitor").ToDictionary(x => x.Name);
        }

        private void LoadTeam()
        {
            var file = this.GetService<IMvxResourceLoader>().GetTextResource("ConfResources/Team.txt");
            var items = JsonConvert.DeserializeObject<List<Team>>(file);
            Team = items.ToDictionary(x => string.Format("{0} {1}", x.Firstname, x.Lastname));
        }

        private void LoadFavorites()
        {
            lock (this)
            {
                _favoriteSessions = new Dictionary<string, SessionWithFavoriteFlag>();
            }
            FireFavoriteSessionsChanged();

            var files = this.GetService<IMvxSimpleFileStoreService>();
            string json;
            if (!files.TryReadTextFile(Constants.FavoritesFileName, out json))
                return;

            var parsedKeys = JsonConvert.DeserializeObject<List<string>>(json);
            if (parsedKeys != null)
            {
                foreach (var key in parsedKeys)
                {
                    SessionWithFavoriteFlag session;
                    if (Sessions.TryGetValue(key, out session))
                        session.IsFavorite = true;
                }
            }
        }

        private bool TryLoadSessionsFromStorage(out PocketConferenceModel conferenceModel)
        {
            conferenceModel = null;

            var fileStore = this.GetService<IMvxSimpleFileStoreService>();

            string fileStoreContents;
            if (!fileStore.TryReadTextFile(Constants.SessionsFileName, out fileStoreContents))
            {
                return false;
            }

            try
            {
                conferenceModel = JsonConvert.DeserializeObject<PocketConferenceModel>(fileStoreContents);
            }
            catch (Exception exception)
            {
                Trace.Warn("Masking error in loading sessions from disk {0}", exception.ToLongString());
            }
            return true;
        }

        private PocketConferenceModel LoadSessionsFromResources()
        {
            // because this is in resources it will never fail to deserialise!
            var file = this.GetService<IMvxResourceLoader>().GetTextResource("ConfResources/Sessions.txt");
            return JsonConvert.DeserializeObject<PocketConferenceModel>(file);
        }

        private void LoadSessions()
        {
            // try to get the data from the service
            var request = new RestRequest(@"/api/schedule");

            var client = new RestClient("https://ciafadmin.herokuapp.com");

            var loaded = false;

            string contents;
            this.GetService<IMvxSimpleFileStoreService>().TryReadTextFile("RESTDATA.json", out contents);

            client.ExecuteAsyncGet(request, (response, handle) =>
            {
                // do something with the response.StatusCode and response.Stream
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    this.GetService<IMvxSimpleFileStoreService>().WriteFile("RESTDATA.json", response.Content);

                    var data = JsonConvert.DeserializeObject<SessionData[]>(response.Content);

                    this.ParseSponsors(data);

                    this.ParseExhibitors(data);

                    this.ParseSessions(data);

                    loaded = true;
                }
            }, "GET");

            while (!loaded)
            {
                Thread.Sleep(500);
            }

            //PocketConferenceModel conferenceModel;
            //if (!TryLoadSessionsFromStorage(out conferenceModel))
            //{
            //    conferenceModel = LoadSessionsFromResources();
            //}
            //LoadSessionsFromPocketConferenceModel(conferenceModel);
        }

        private void ParseSessions(SessionData[] data)
        {
            Sessions = new Dictionary<string, SessionWithFavoriteFlag>();
            var i = 0;
            foreach (var sessionData in data)
            {
                var session = new Session
                {
                    Day = 0,
                    Id = sessionData._id,
                    RoomName = sessionData.location,
                    Slot = new Slot(),
                    SlotId = String.Empty,
                    SpeakerTwitterName = String.Empty,
                    SpeakerWebsiteURL = String.Empty,
                    Speaker = sessionData.contributor,
                    Synopsis = String.Empty,
                    Title = sessionData.description,
                    TrackName = String.Empty
                };

                Sessions.Add(session.Speaker, new SessionWithFavoriteFlag { Session = session});
                i++;
            }
        }

        private void ParseExhibitors(SessionData[] data)
        {
            Exhibitors = new Dictionary<string, Sponsor>();
            var i = 0;
            foreach (var location in data.Select(sessionData => sessionData.location).Where(s => !string.IsNullOrEmpty(s)).Distinct())
            {
                if (!Exhibitors.ContainsKey(location))
                {
                    Exhibitors.Add(location,
                        new Sponsor
                        {
                            Description = location,
                            DisplayOrder = i,
                            Level = "Premium",
                            Name = location,
                            Image = "",
                            Url = ""
                        });
                }
                i++;
            }
        }

        private void ParseSponsors(SessionData[] data)
        {
            Sponsors = new Dictionary<string, Sponsor>();
            var i = 0;
            foreach (var sessionData in data.Where(sessionData => !string.IsNullOrEmpty(sessionData.name)))
            {
                if (!Sponsors.ContainsKey(sessionData.name))
                {
                    Sponsors.Add(sessionData.name,
                        new Sponsor
                        {
                            Description = sessionData.description,
                            DisplayOrder = i,
                            Level = "Premium",
                            Name = sessionData.name,
                            Image = "",
                            Url = ""
                        });
                }
                i++;
            }
        }

        private void ClearExistingSessions()
        {
            if (Sessions == null)
                return;

            foreach (var sessionWithFavoriteFlag in Sessions.Values)
            {
                sessionWithFavoriteFlag.PropertyChanged -= SessionWithFavoriteFlagOnPropertyChanged;
            }
        }

        private void LoadSessionsFromPocketConferenceModel(PocketConferenceModel conferenceModel)
        {
            ClearExistingSessions();

            // patch up the sessions with slots
            foreach (var session in conferenceModel.Sessions.Values)
            {
                Slot slot;
                if (conferenceModel.Slots.TryGetValue(session.SlotId, out slot))
                {
                    session.Slot = slot;
                }
            }

            Sessions = conferenceModel.Sessions.Select(x => new SessionWithFavoriteFlag
            {
                Session = x.Value,
                IsFavorite = false
            }).ToDictionary(x => x.Session.Id, x => x);

            foreach (var sessionWithFavoriteFlag in Sessions.Values)
            {
                sessionWithFavoriteFlag.PropertyChanged += SessionWithFavoriteFlagOnPropertyChanged;
            }
        }

        private void SessionWithFavoriteFlagOnPropertyChanged
            (object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName != "IsFavorite")
                return;

            var session = (SessionWithFavoriteFlag) sender;
            lock (this)
            {
                if (_favoriteSessions == null)
                    return;

                if (session.IsFavorite)
                {
                    _favoriteSessions[session.Session.Id] = session;
                }
                else
                {
                    if (_favoriteSessions.ContainsKey(session.Session.Id))
                        _favoriteSessions.Remove(session.Session.Id);
                }

                _favoritesSaver.RequestAsyncSave(_favoriteSessions.Keys.ToList());
            }

            FireFavoriteSessionsChanged();
        }
    }
}