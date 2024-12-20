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
    class HighScore:IHighScore
    {
        private MainDbContext _context;
        public HighScore(MainDbContext context)
        {
            _context = context;
        }
        public void AddScore(int userId,int score)
        {
            var user = _context.HighScores
                .FirstOrDefault(h = h.UserId == userId);
            if (user != null)
            {
                var scoresList = string.IsNUllOrEmpty(highScoreEntity.Scores) ? new List<int>() : JsonSerializer.Deserialize<List<int>>(user.Scores);
                scoresList.Add(score);

                user.HighScore = scoresList.Max();

                user.Scores = JsonSerializer.Serialize(scoresList);
                _context.SaveChanges();
            }
            else
            {
                var scoresList = new List<int> { score };
                var newUser = new HighScoreEntity
                {
                    UserId = userId,
                    HighScore = score,
                    scoresList = JsonSerializer.Serialize(scoresList)
                };
                _context.HighScores.Add(newUser);
                _context.SaveChanges();
            }
        }

        public int GetHighScore(int userId)
        {
            var user = _context.HighScores.FirstOrDefault(h => h.UserId == userId);
            if (user != null)
            {
                return user.HighScore;
            }
            return 0;
        }
        //getScoresList veradardzni Json
    }
}
