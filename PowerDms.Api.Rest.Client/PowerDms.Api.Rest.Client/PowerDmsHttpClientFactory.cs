﻿using System;
using System.Net.Http;

namespace PowerDms.Api.Rest.Client
{
    public class PowerDmsHttpClientFactory
    {
        public static HttpClient CreateHttpClient()
        {
            return CreateHttpClient(ApiVersion.Version1);
        }

        public static HttpClient CreateHttpClient(ApiVersion apiVersion)
        {
            return CreateHttpClient(RestApiRoutes.PowerDmsRestApiDomain, apiVersion);
        }

        public static HttpClient CreateOAuthHttpClient()
        {
            return CreateHttpClient(RestApiRoutes.PowerDmsOAuthDomain, null);
        }

        public static HttpClient CreateHttpClient(
            string powerDmsRestApiDomain, 
            ApiVersion apiVersion)
        {
            apiVersion = apiVersion ?? ApiVersion.Version1;

            var httpClient = new HttpClient
            {
                BaseAddress = new Uri($"{powerDmsRestApiDomain}/{apiVersion}/")
            };

            return httpClient;
        }
    }
}