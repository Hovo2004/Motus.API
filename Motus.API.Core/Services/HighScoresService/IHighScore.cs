using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motus.API.Core.Services.HighScoresService
{
    public interface IHighScore
    {
        public void AddScore(int userId, int score);
        public int GetHighScore(int userId);
    }
}
