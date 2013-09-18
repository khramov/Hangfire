﻿using System;
using HangFire.Web;

[assembly: WebActivatorEx.PostApplicationStartMethod(
    typeof(HangFire.MvcSample.HangFireConfig), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethod(
    typeof(HangFire.MvcSample.HangFireConfig), "Stop")]

namespace HangFire.MvcSample
{
    public class HangFireConfig
    {
        private static AspNetBackgroundJobServer _server;

        public static void Start()
        {
            // If you have custom Redis installation, use the
            // following method to configure HangFire:
            JobStorage.Configure(x => { x.RedisDb = 3; });
            
            _server = new AspNetBackgroundJobServer
            {
                ServerName = Environment.MachineName,
                QueueName = "default",
                WorkersCount = Environment.ProcessorCount * 2
            };

            GlobalJobFilters.Filters.Add(new PreserveCultureFilter());

            //_server.Start();
        }

        public static void Stop()
        {
            _server.Stop();
        }
    }
}