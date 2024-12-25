using System;
using API_Aggregation.Models;

namespace API_Aggregation.Services.Interfaces
{
	public interface IGitHubService
	{


        Task<GitHubApiResponse> GetUserAsync(string userName);

    }
}

