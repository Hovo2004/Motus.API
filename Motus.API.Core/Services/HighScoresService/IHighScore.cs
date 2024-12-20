using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motus.API.Core.Services.HighScoresService
{
    public interface IHighScore
    {
        public void AddScore(string email, int score);
        public int GetHighScore(string email);
    }
}
