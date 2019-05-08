using log4net;
using StarsWars.Common.Entities;
using StarsWars.Common.Managers;
using StarsWars.Common.Exceptions;
using StarsWars.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace StarsWars.Business.Managers
{
    public class StarsWarsManager : IStarsWarsManager
    {
        private static ILog _log;

        public StarsWarsManager()
        {
            _log = LogManager.GetLogger(this.GetType().FullName);
        }

        public int GetCharactersCount() {

            _log.Info("begin GetCharactersCount");

            try
            {
                using (var context = new StarsWarsDbContext())
                {
                    var count = context.Characters.Count();

                    return count;
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                throw;
            }
        }

        public IList<Character> GetCharacters()
        {
            _log.Info("begin GetCharacters");

            try
            {
                using (var context = new StarsWarsDbContext())
                {
                    var characters = (from u in context.Characters.Include("Episodes").Include("Friends")
                                      select u).ToList();

                    if (characters == null)
                    {
                        _log.ErrorFormat("Characters not found");
                        throw new EntityNotFoundException("Characters not found");
                    }

                    return characters;
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                throw;
            }
        }

        public Character GetCharacter(int characterId) {

            _log.Info("begin GetCharacter");

            try
            {
                using (var context = new StarsWarsDbContext())
                {
                    var item = context.Characters
                               .Include("Episodes").Include("Friends")
                               .FirstOrDefault(u => u.Id == characterId);

                    if (item == null)
                    {
                        _log.ErrorFormat("Character not found");
                        throw new EntityNotFoundException("Character not found");
                    }

                    return item;
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                throw;
            }

        }

        public void CreateCharacter(Character item)
        {
            _log.Info("begin CreateCharacter");

            try
            {
                if (item == null)
                {
                    _log.Error("Character object is null");
                    throw new ArgumentNullException("Character not created");
                }

                if (string.IsNullOrEmpty(item.Name))
                {

                    _log.Error("Name is empty");
                    throw new ArgumentNullException("Name is empty");
                }

                using (var context = new StarsWarsDbContext())
                {
                    var character = context.Characters.FirstOrDefault(u => u.Name == item.Name);

                    if (character != null)
                    {
                        _log.ErrorFormat($"Character {character.Name} already exists");
                        throw new StarsWarsException($"Character {character.Name} already exists");
                    }

                    context.Characters.Add(item);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                throw;
            }
        }

        public void UpdateCharacter(Character item)
        {
            _log.Info("begin UpdateCharacter");

            try
            {
                using (var context = new StarsWarsDbContext())
                {
                    var character = context.Characters.Find(item.Id);

                    if (character == null)
                    {
                        _log.ErrorFormat($"Character {item.Name} not found");
                        throw new EntityNotFoundException($"Character {item.Name} not found");
                    }

                    var characterVal = context.Characters.FirstOrDefault(u => u.Name == item.Name);

                    if (characterVal != null)
                    {
                        _log.ErrorFormat($"Character {item.Name} already exists");
                        throw new StarsWarsException($"Character {item.Name} already exists");
                    }

                    context.Entry(character).CurrentValues.SetValues(item);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                throw;
            }

        }

        public void RemoveCharacter(int characterId)
        {
            _log.Info("begin RemoveCharacter");

            try
            {
                using (var context = new StarsWarsDbContext())
                {
                    var item = context.Characters.FirstOrDefault(u => u.Id == characterId);

                    if (item == null)
                    {
                        _log.ErrorFormat("Character not found");
                        throw new EntityNotFoundException("Character not found");
                    }

                    context.Entry<Character>(item).State = System.Data.Entity.EntityState.Deleted;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                throw;
            }
        }

        public void AddEpisode(int characterId, Episode episode)
        {
            _log.Info("begin AddEpisode");

            try
            {
                if (episode == null)
                {
                    _log.Error("Episode not added");
                    throw new StarsWarsException("Episode not added");
                }

                using (var context = new StarsWarsDbContext())
                {
                    var character = context.Characters.FirstOrDefault(e => e.Id == characterId);

                    if (character == null)
                    {
                        _log.ErrorFormat("Character not found");
                        throw new EntityNotFoundException("Character not found");
                    }

                    var episodeItem = context.Episodes.FirstOrDefault(e => e.Character.Id == characterId && e.Name == episode.Name);

                    if (episodeItem != null)
                    {
                        _log.ErrorFormat("Episode already exists");
                        throw new StarsWarsException("Episode already exists");
                    }

                    episode.Character = character;
                    character.Episodes.Add(episode);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                throw;
            }

        }

        public void UpdateEpisode(Episode episode)
        {
            _log.Info("begin UpdateEpisode");

            try
            {
                using (var context = new StarsWarsDbContext())
                {
                    var episodeItem = context.Episodes.Find(episode.Id);

                    if (episodeItem == null)
                    {
                        _log.ErrorFormat($"Episode {episode.Name} not found");
                        throw new EntityNotFoundException($"Episode {episode.Name} not found");
                    }

                    var episodeVal = context.Episodes.FirstOrDefault(u => u.Name == episode.Name && u.Character.Id == episodeItem.Character.Id);

                    if (episodeVal != null)
                    {
                        _log.ErrorFormat($"Episode {episode.Name} already exists");
                        throw new StarsWarsException($"Episode {episode.Name} already exists");
                    }

                    context.Entry(episodeItem).CurrentValues.SetValues(episode);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                throw;
            }

        }

        public void RemoveEpisode(int characterId, int episodeId)
        {
            _log.Info("begin RemoveEpisode");

            try
            {
                using (var context = new StarsWarsDbContext())
                {

                    var episode = context.Episodes.FirstOrDefault(e => e.Id == episodeId && e.Character.Id == characterId);

                    if (episode == null)
                    {
                        _log.ErrorFormat("Episode not found");
                        throw new EntityNotFoundException("Episode not found");
                    }

                    context.Entry<Episode>(episode).State = System.Data.Entity.EntityState.Deleted;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                throw;
            }

        }

        public void AddFriend(int characterId, Friend friend)
        {
            _log.Info("begin AddFriend");

            try
            {
                if (friend == null)
                {
                    _log.Error("Friends not added");
                    throw new StarsWarsException("Friends not added");
                }

                using (var context = new StarsWarsDbContext())
                {
                    var character = context.Characters.FirstOrDefault(e => e.Id == characterId);

                    if (character == null)
                    {
                        _log.Error("Character not found");
                        throw new EntityNotFoundException("Character not found");
                    }

                    var friendItem = context.Friends.FirstOrDefault(e => e.Character.Id == characterId && e.Name == friend.Name);

                    if (friendItem != null)
                    {
                        _log.Error("Friend already exists");
                        throw new StarsWarsException("Friend already exists");
                    }

                    friend.Character = character;
                    character.Friends.Add(friend);

                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                throw;
            }
        }


        public void UpdateFriend(Friend friend)
        {
            _log.Info("begin UpdateFriend");

            try
            {
                using (var context = new StarsWarsDbContext())
                {
                    var friendItem = context.Friends.Find(friend.Id);

                    if (friendItem == null)
                    {
                        _log.ErrorFormat($"Friend {friend.Name} not found");
                        throw new EntityNotFoundException($"Friend {friend.Name} not found");
                    }

                    var friendVal = context.Friends.FirstOrDefault(u => u.Name == friend.Name && u.Character.Id == friendItem.Character.Id);

                    if (friendVal != null)
                    {
                        _log.ErrorFormat($"Friend {friend.Name} already exists");
                        throw new StarsWarsException($"Friend {friend.Name} already exists");
                    }

                    context.Entry(friendItem).CurrentValues.SetValues(friend);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                throw;
            }
        }

        public void RemoveFriend(int characterId, int friendId)
        {
            _log.Info("begin RemoveFriend");

            try
            {
                using (var context = new StarsWarsDbContext())
                {
                    var friend = context.Friends.FirstOrDefault(e => e.Id == friendId && e.Character.Id == characterId);

                    if (friend == null)
                    {
                        _log.Error("Friend object is null");
                        throw new EntityNotFoundException("Friend not found");
                    }

                    context.Entry<Friend>(friend).State = System.Data.Entity.EntityState.Deleted;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                throw;
            }
        }

    }
}
