using _00016339_Feedback_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
namespace _00016339_Feedback_MVC.Controllers

{
    public class FeedbackController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7147/api");
        private readonly HttpClient _httpClient;
        public FeedbackController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseAddress;
        }

        // Get all in list in form of INDEX
        [HttpGet]
        public IActionResult Index()
        {
            List<FeedbackViewModel> feedback = new List<FeedbackViewModel>();
            HttpResponseMessage respoonse = _httpClient.GetAsync(_httpClient.BaseAddress + "/Feedback").Result;
            if (respoonse.IsSuccessStatusCode)
            {
                string data = respoonse.Content.ReadAsStringAsync().Result;
                feedback = JsonConvert.DeserializeObject<List<FeedbackViewModel>>(data);
            }

            return View(feedback);
        }

        // Create a new entity
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(FeedbackViewModel feedback)
        {
            try
            {
                string data = JsonConvert.SerializeObject(feedback);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _httpClient.PostAsync(baseAddress + "/feedback/create", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                return View();
            }
            return View();
        }

        // Edit View
        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                FeedbackViewModel model = new FeedbackViewModel();
                HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "/feedback/getbyid/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    model = JsonConvert.DeserializeObject<FeedbackViewModel>(data);
                }
                return View(model);
            }
            catch (Exception e)
            {
                return View();
                throw;
            }
        }

        //Edit Post
        [HttpPost]
        public IActionResult Edit(FeedbackViewModel m)
        {
            string d = JsonConvert.SerializeObject(m);
            StringContent c = new StringContent(d, Encoding.UTF8, "application/json");
            HttpResponseMessage response = _httpClient.PutAsync(_httpClient.BaseAddress + "/feedback/update", c).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        // Delete Get
        // Get The Data before deleting
        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                FeedbackViewModel feedbackViewModel = new FeedbackViewModel();
                HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "/feedback/getbyid/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    feedbackViewModel = JsonConvert.DeserializeObject<FeedbackViewModel>(data);
                }
                return View(feedbackViewModel);
            }
            catch (Exception e)
            {
                return View();
            }
        }

        // Delete Confirm
        [HttpPost, ActionName("Delete")]
        public IActionResult DelteConfirm(int id)
        {
            try
            {
                HttpResponseMessage response = _httpClient.DeleteAsync(_httpClient.BaseAddress + "/feedback/delete/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {

                return View();
            }
            return View();

        }


    }
}
