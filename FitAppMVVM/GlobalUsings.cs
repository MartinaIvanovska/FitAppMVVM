﻿global using System.Collections.Immutable;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Hosting;
global using Microsoft.Extensions.Localization;
global using Microsoft.Extensions.Logging;
global using Microsoft.Extensions.Options;
global using FitAppMVVM.Models;
global using FitAppMVVM.Presentation;
global using FitAppMVVM.Services.Endpoints;
global using FitAppMVVM.DataContracts;
global using FitAppMVVM.DataContracts.Serialization;
global using FitAppMVVM.Services.Caching;
global using FitAppMVVM.Client;
global using Uno.Extensions.Http.Kiota;
global using ApplicationExecutionState = Windows.ApplicationModel.Activation.ApplicationExecutionState;
global using CommunityToolkit.Mvvm.ComponentModel;
global using CommunityToolkit.Mvvm.Input;
