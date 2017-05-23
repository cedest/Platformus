﻿// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.Domain.Backend.ViewModels.Shared
{
  public class MicrocontrollerViewModel : ViewModelBase
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string UrlTemplate { get; set; }
    public string ViewName { get; set; }
    public int? Position { get; set; }
  }
}