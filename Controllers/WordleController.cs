using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using System.IO;
using System.Collections.Generic;


namespace WordleAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WordleController : ControllerBase
    {
        private static readonly string[] words = System.IO.File.ReadAllLines("data/words.txt");


        [HttpPost]
        public ActionResult<string[]> Post(WordleData data)
        {
            string[] valid = getValidWords(data).ToArray();
            return valid;
        }

        private List<string> getValidWords(WordleData data)
        {
            List<string> validWords = new(words);
            if (data.rightPos != null)
                if (data.rightPos.Count > 0) CleanByPos(data, validWords);

            if (data.rightLetters != null)
                if (data.rightLetters.Count > 0) CleanByRightLetters(data, validWords);

            if (data.wrongLetters != null)
                if (data.wrongLetters.Count > 0) CleanByWrongLetters(data, validWords);

            return validWords;
        }

        private static void CleanByWrongLetters(WordleData data, List<string> validWords)
        {
            validWords.RemoveAll(e =>
            {
                foreach (char c in data.wrongLetters)
                {
                    if (e.Contains(c)) return true;
                }
                return false;
            });
        }

        private static void CleanByRightLetters(WordleData data, List<string> validWords)
        {
            int removed = validWords.RemoveAll(e =>
            {
                foreach (char c in data.rightLetters)
                {
                    if (!e.Contains(c)) return true;
                }
                return false;
            });

        }

        private static void CleanByPos(WordleData data, List<string> validWords)
        {
            validWords.RemoveAll(e =>
            {
                bool isSafe = false;
                foreach (var c in data.rightPos)
                {
                    if (e[c.Key] != c.Value) isSafe = true;
                }

                return isSafe;
            });
        }
    }
}