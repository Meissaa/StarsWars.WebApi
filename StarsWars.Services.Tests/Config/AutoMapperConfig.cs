using AutoMapper;
using StarsWars.Common.Entities;
using StarsWars.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarsWars.Services.Tests.Config
{
    public static class AutoMapperConfig
    {
        public static object _thisLock = new object();
        private static bool _initialized = false;

        public static void Initialize()
        {
            lock (_thisLock)
            {
                if (!_initialized)
                {
                    Mapper.Initialize(cfg =>
                    {
                        cfg.CreateMap<CharacterRequest, Character>();
                        cfg.CreateMap<Character, CharacterRequest>();
                        cfg.CreateMap<EpisodeRequest, Episode>();
                        cfg.CreateMap<Episode, EpisodeRequest>();
                        cfg.CreateMap<FriendRequest, Friend>();
                        cfg.CreateMap<Friend, FriendRequest>();

                    });
                    _initialized = true;
                }
            }
        }
    }
}
