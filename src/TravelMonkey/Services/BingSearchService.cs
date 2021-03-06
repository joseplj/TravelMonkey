﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.Search.ImageSearch;
using TravelMonkey.Models;

namespace TravelMonkey.Services
{
    public class BingSearchService
    {
        public async Task<List<Destination>> GetDestinations()
        {
            var searchDestinations = new[] { "Seattle", "Maui", "Amsterdam", "Antarctica" };

            try
            {
                var client = new ImageSearchClient(new ApiKeyServiceClientCredentials(ApiKeys.BingImageSearch));

                var resultDestinations = new List<Destination>();

                foreach (var destination in searchDestinations)
                {
                    var result = await client.Images.SearchAsync(destination, color: "blue", minWidth: 500, minHeight: 500, imageType: "Photo", license: "Public", count: 1, maxHeight: 1200, maxWidth: 1200);

                    resultDestinations.Add(new Destination
                    {
                        Title = destination,
                        ImageUrl = result.Value[0].ContentUrl,
                        MoreInfoUrl = result.Value[0].HostPageUrl
                    });
                }

                return resultDestinations;
            }
            catch
            {
                return new List<Destination> {
                    new Destination
                    {
                        Title = "Something went wrong :( Here is a cat instead!",
                        ImageUrl = "https://cataas.com/cat",
                        MoreInfoUrl = "https://cataas.com/"
                    }
                };
            }
        }
    }
}