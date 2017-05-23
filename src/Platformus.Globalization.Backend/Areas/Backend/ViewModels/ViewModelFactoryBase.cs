﻿// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Platformus.Barebone;
using Platformus.Globalization.Data.Abstractions;
using Platformus.Globalization.Data.Models;

namespace Platformus.Globalization.Backend.ViewModels
{
  public abstract class ViewModelFactoryBase : Platformus.Barebone.Backend.ViewModels.ViewModelFactoryBase
  {
    public ViewModelFactoryBase(IRequestHandler requestHandler)
      : base(requestHandler)
    {
      this.RequestHandler = requestHandler;
    }

    public string GetLocalizationValue(int dictionaryId)
    {
      Localization localization = this.RequestHandler.Storage.GetRepository<ILocalizationRepository>().WithDictionaryIdAndCultureId(
        dictionaryId, CultureManager.GetCurrentCulture(this.RequestHandler.Storage).Id
      );

      if (localization == null)
        return string.Empty;

      return localization.Value;
    }

    protected IEnumerable<Platformus.Barebone.Backend.Localization> GetLocalizations(Dictionary dictionary = null)
    {
      List<Platformus.Barebone.Backend.Localization> localizations = new List<Platformus.Barebone.Backend.Localization>();

      foreach (Platformus.Globalization.Data.Models.Culture culture in this.RequestHandler.Storage.GetRepository<ICultureRepository>().All().ToList())
      {
        Platformus.Globalization.Data.Models.Localization localization = null;

        if (dictionary != null)
          localization = this.RequestHandler.Storage.GetRepository<ILocalizationRepository>().FilteredByDictionaryId(dictionary.Id).FirstOrDefault(l => l.CultureId == culture.Id);

        localizations.Add(
          new Platformus.Barebone.Backend.Localization(
            new Platformus.Barebone.Backend.Culture(culture.Code),
            localization == null ? null : localization.Value
          )
        );
      }

      return localizations;
    }
  }
}