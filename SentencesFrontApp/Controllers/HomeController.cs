using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SentencesFrontApp.Models;
using SentencesFrontApp.Services;

namespace SentencesFrontApp.Controllers
{
    public class HomeController : Controller
    {
        
        SentenceService _sentenceService;
        public HomeController(SentenceService sentenceService)
        {
            _sentenceService = sentenceService;
        }

        [HttpGet]
        public IActionResult CreateSentence()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateSentence(Phrase phrase)
        {
            string text = phrase.textPhrase;
            Sentence sentence = await _sentenceService.PostSentence(text);
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> RandomSentence()
        {
            Sentence sentence = await _sentenceService.GetRandomSentence();
            return View(sentence);
        }



        public async Task<IActionResult> Index()
        {
            List<Sentence> sentences = await _sentenceService.GetSentences();
            return View(sentences);
        }

        public async Task<IActionResult> About()
        {

            Sentence sentence = await _sentenceService.PostSentence("Carpe Diem");

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
