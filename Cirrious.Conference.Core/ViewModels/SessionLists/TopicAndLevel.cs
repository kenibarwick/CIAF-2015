//using Cirrious.Conference.Core.Models.Raw;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace Cirrious.Conference.Core.ViewModels.SessionLists
//{
//    public class TopicAndLevel
//    {
//        //public string Topic { get; private set; }
//        //public string Level { get; private set; }
//        public string ocation { get; private set; }

//        public TopicAndLevel(Session session)
//        {
//            //Topic = session.Type;
//            //Level = session.Level;
//            Location = session.Where;
//        }

//        public override string ToString()
//        {
//            return string.Format("{0}", Location);                //return string.Format("{0} ({1})", Location, Level);
//        }

//        public override bool Equals(object obj)
//        {
//            var rhs = obj as TopicAndLevel;
//            if (rhs == null)
//                return false;
//            return ToString().GetHashCode();
//        }
//    }
//}


//            return rhs.Location == Location;

//            //return rhs.Topic == Topic;
//            //&& rhs.Level == Level;
//        }

//        public override int GetHashCode()
//        {