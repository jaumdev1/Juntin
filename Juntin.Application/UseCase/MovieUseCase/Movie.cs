using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
using Domain.Common;
using Domain.Dtos.Movie;
using Juntin.Application.Interfaces.Movie;
using Microsoft.Extensions.Configuration;

namespace Juntin.Application.UseCase.MovieUseCase;

public class Movie : IMovieUseCase
{
    private readonly IConfiguration _configuration;
    private readonly HttpClient _httpClient;

    public Movie(IConfiguration configuration, HttpClient httpClient)
    {
        _configuration = configuration;
        _httpClient = httpClient;
    }

    public async Task<BasicResult<List<MovieResult>>> Execute(MovieDto input)
    {
        try
        {
            var apiKey = _configuration["TMDB:ApiKey"];
            var bearerToken = _configuration["TMDB:BearerToken"];
            var url =
                $"https://api.themoviedb.org/3/search/movie?query={input.Title}&include_adult=false&language=en-US&page=1";

            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var movieResponse = JsonSerializer.Deserialize<MovieResponse>(responseContent, options);

                var movies = movieResponse.Results.Select(s => new MovieResult(s.Title,
                    s.Poster_Path,
                    s.Overview,
                    s.Id)).ToList();

                return BasicResult.Success(movies);
            }

            return BasicResult.Failure<List<MovieResult>>(new Error(HttpStatusCode.InternalServerError,
                response.ReasonPhrase));
        }
        catch (Exception ex)
        {
            return BasicResult.Failure<List<MovieResult>>(new Error(HttpStatusCode.InternalServerError, ex.Message));
        }
    }
}