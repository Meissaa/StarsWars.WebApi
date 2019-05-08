using AutoMapper;
using StarsWars.Common.Entities;
using StarsWars.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StarsWars.Services
{
    public static class AutoMapperConfig
    {
        public static void Initialize()
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
        }
    }
}