using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using Valudator;

namespace Valuator.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IStorage _storage;

        public IndexModel(ILogger<IndexModel> logger, IStorage storage)
        {
            _logger = logger;
            _storage = storage;
        }

        public void OnGet()
        {

        }

        public IActionResult OnPost(string text)
        {
            _logger.LogDebug(text);

            string id = Guid.NewGuid().ToString();

            string rankKey = "RANK-" + id;
            string rank = GetRank(text).ToString("0.##");
            _storage.Store(rankKey, rank);

            string similarityKey = "SIMILARITY-" + id;
            if (_storage.IsExistsByValue(text)) {
                _storage.Store(similarityKey, "1");
            } else {
                _storage.Store(similarityKey, "0");
            }

            string textKey = "TEXT-" + id;
            _storage.Store(textKey, text);

            return Redirect($"summary?id={id}");
        }

        private double GetRank(string text)
        {
            if (text == null) {
                return 0;
            }
            int notLetterCharsCount = text.Where(ch => !char.IsLetter(ch)).Count();
            return notLetterCharsCount / (double) text.Length;
        }
    }
}
