﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Reoria.Application;
using Reoria.Client.FNA;

internal class Program
{
    private static void Main(string[] args)
    {
        new ApplicationBuilder(args).ConfigureConfiguration((builder, configuration) =>
        {
            var appSettingsLoader = new AppSettingsLoader();

            configuration.AddConfiguration(appSettingsLoader.Builder.Build());
        }).ConfigureServices((context, services) =>
        {
            services.AddTransient<IClientService, ClientService>();
        }).BuildApplication<IClientService>()?.Run();
    }

    private interface IClientService
    {
        void Run();
    }

    private class ClientService : IClientService
    {
        public void Run()
        {
            using var game = new GameInstance();
            game.Run();
        }
    }
}