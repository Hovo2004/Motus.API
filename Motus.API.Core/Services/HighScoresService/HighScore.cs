using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Motus.API.Data.DAO;
using Motus.API.Data.Entities;

namespace Motus.API.Core.Services.HighScoresService
{
    public class HighScore:IHighScore
    {
        private MainDbContext _context;
        public HighScore(MainDbContext context)
        {
            _context = context;
        }
        public void AddScore(string email,int score)
        {
            var user = _context.Users.FirstOrDefault(user =>  user.Email == email);
            if (user != null)
            {
                var scoresList = string.IsNullOrEmpty(user.Scores) ? new List<int>() : JsonSerializer.Deserialize<List<int>>(user.Scores);
                scoresList.Add(score);

                user.HighScore = scoresList.Max();

                user.Scores = JsonSerializer.Serialize(scoresList);
                _context.SaveChanges();
            }
            else
            {
                Console.WriteLine("No such User");
            }
        }

        public int GetHighScore(string email)
        {
            var user = _context.Users.FirstOrDefault(user => user.Email == email);
            if (user != null)
            {
                return user.HighScore;
            }
            return 0;
        }
        //getScoresList veradardzni Json
    }
}
