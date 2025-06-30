namespace FitAppMVVM.Services.Caching;
using WeatherForecast = FitAppMVVM.Client.Models.WeatherForecast;
public interface IWeatherCache
{
    ValueTask<IImmutableList<WeatherForecast>> GetForecast(CancellationToken token);
}
