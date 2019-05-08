using StarsWars.Business.Managers;
using StarsWars.Common.Entities;
using StarsWars.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ConsoleApp.Test
{
    class Program
    {
        static StarsWarsManager _starsWarsManager = new StarsWarsManager();

        static void Main(string[] args)
        {
            Character character = new Character()
            {
                Name = "Dart Vader"
            };

            Episode episode = new Episode { Id = 2, Name = "EMPIRE" };
            Friend friend = new Friend { Id = 1, Name = "C-3PO" };

            //CreateCharacter_Test(character);
            //AddEpisode_Test(2, episode);
            //GetCharacters_Test();
            //UpdateCharacter_Test(character);
            //RemoveCharacter_Test(1);
            //UpdateEpisode_Test(episode);
            //RemoveEpisode_Test(2,2);
            //AddFriend_Test(2, friend);
            //UpdateFriend_Test(friend);
            //RemoveFriend_Test(2,1);
            //GetDetailsCharacter_Test(2);
            Console.ReadKey();

        }

        public static void GetCharacters_Test()
        {

            var list = _starsWarsManager.GetCharacters();
            foreach (var i in list)
                Console.WriteLine(i);
        }

        public static void GetDetailsCharacter_Test(int characterId)
        {

            _starsWarsManager.GetCharacter(characterId);
        }

        public static void CreateCharacter_Test(Character item)
        {

            _starsWarsManager.CreateCharacter(item);

        }

        public static void AddEpisode_Test(int characterId, Episode episode)
        {

            _starsWarsManager.AddEpisode(characterId, episode);
        }

        public static void UpdateCharacter_Test(Character item)
        {
            _starsWarsManager.UpdateCharacter(item);
        }

        public static void RemoveCharacter_Test(int characterId)
        {
            _starsWarsManager.RemoveCharacter(1);
        }

        public static void UpdateEpisode_Test(Episode episode)
        {
            _starsWarsManager.UpdateEpisode(episode);
        }

        public static void RemoveEpisode_Test(int characterId, int episodeId)
        {
            _starsWarsManager.RemoveEpisode(characterId, episodeId);
        }

        public static void AddFriend_Test(int characterId, Friend friend)
        {
            _starsWarsManager.AddFriend(characterId, friend);
        }

        public static void UpdateFriend_Test(Friend friend)
        {
            _starsWarsManager.UpdateFriend(friend);
        }

        public static void RemoveFriend_Test(int characterId, int friendId)
        {
            _starsWarsManager.RemoveFriend(characterId, friendId);
        }

    }
}
