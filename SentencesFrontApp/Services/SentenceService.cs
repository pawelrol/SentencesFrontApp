using SentencesFrontApp.Core;
using SentencesFrontApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SentencesFrontApp.Services
{
    public class SentenceService
    {
        CoreApp _coreApp;

        public SentenceService(CoreApp coreApp)
        {
            _coreApp = coreApp;
        }

        static Random rnd = new Random();

        public async Task<List<Sentence>> GetSentences()
        {
            var uri = _coreApp.CreateRequestUri("Sentences");
            List<Sentence> result = await _coreApp.GetAsync<List<Sentence>>(uri);
            return result;
        }

        public async Task<Sentence> PostSentence(string text)
        {
            var uri = _coreApp.CreateRequestUri("Sentences");
            Sentence testSentence = new Sentence();
            testSentence.SentencePhrase = text;
            var result = await _coreApp.PostAsync<Sentence>(uri, testSentence);
            return result;

        }

        public async Task<Sentence> GetRandomSentence()
        {
            List<Sentence> sentences = await GetSentences();
            int r = rnd.Next(sentences.Count);
            return sentences[r];

        }
    }
}
