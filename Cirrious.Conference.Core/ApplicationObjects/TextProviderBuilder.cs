﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Cirrious.MvvmCross.Localization;

namespace Cirrious.Conference.Core.ApplicationObjects
{
    public class TextProviderBuilder
        : MvxTextProviderBuilder    
    {
        public TextProviderBuilder() 
            : base(Constants.GeneralNamespace, Constants.RootFolderForResources)
        {
        }

        protected override IDictionary<string, string> ResourceFiles
        {
            get
            {
#if NETFX_CORE
                var dictionary = this.GetType()
                    .GetTypeInfo()
                    .Assembly
                    .DefinedTypes
                    .Where(t => t.Name.EndsWith("ViewModel"))
                    .Where(t => !t.Name.StartsWith("Base"))
                    .ToDictionary(t => t.Name, t => t.Name);
#else
                var dictionary = this.GetType()
                    .Assembly
                    .GetTypes()
                    .Where(t => t.Name.EndsWith("ViewModel"))
                    .Where(t => !t.Name.StartsWith("Base"))
                    .ToDictionary(t => t.Name, t => t.Name);
#endif

                dictionary[Constants.Shared] = Constants.Shared;    
                return dictionary;
            }
        }
    }
}
