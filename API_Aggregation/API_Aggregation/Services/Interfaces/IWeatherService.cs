using System;
using API_Aggregation.Models;

namespace API_Aggregation.Services.Interfaces
{
	public interface IWeatherService
	{
        Task<WeatherApiResponse> GetWeatherAsync(string city);

    }
}

