using StarsWars.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarsWars.Common.Managers
{
    public interface IStarsWarsManager
    {
        int GetCharactersCount();
        void CreateCharacter(Character item);
        void UpdateCharacter(Character item);
        void RemoveCharacter(int characterId);
        IList<Character> GetCharacters();
        Character GetCharacter(int id);
        void AddEpisode(int characterId, Episode episode);
        void UpdateEpisode(Episode episode);
        void RemoveEpisode(int characterId, int episodeId);
        void AddFriend(int characterId, Friend friend);
        void UpdateFriend(Friend friend);
        void RemoveFriend(int characterId, int friendId);
    }
}
