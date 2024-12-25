using System;
using API_Aggregation.Models;

namespace API_Aggregation.Services.Interfaces
{
	public interface INewsService
	{

        Task<NewsApiResponse> GetTopHeadlinesNewsAsync(string dateFrom, string sortBy);


    }
}

