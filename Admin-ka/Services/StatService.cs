﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Admin.Model;
using Newtonsoft.Json;

namespace Admin.Services
{
    public class StatService : IStatService
    {
        private readonly IReadOnlyDictionary<string, HttpClient> _clientDict;

        public StatService(IReadOnlyDictionary<string, Uri> uriDict)
        {
            _clientDict = uriDict.ToDictionary(uri => uri.Key, uri => new HttpClient { BaseAddress = uri.Value, Timeout = TimeSpan.FromSeconds(10) });
        }

        public async Task<StatContract[]> GetStatsAsync(string type)
        {
            try
            {
                var result = await _clientDict[type].GetAsync("stats").ConfigureAwait(false);
                var content = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<StatContract[]>(content);
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
                return null;
            }
        }

        public async Task<ModuleInfo> GetInfoAsync(string type)
        {
            try
            {
                var result = await _clientDict[type].GetAsync("info").ConfigureAwait(false);
                var content = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ModuleInfo>(content);
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
                return null;
            }
        }
    }
}
