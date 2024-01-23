using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebSpasialView.Models;

namespace WebSpasialView.Controllers
{
    public class TempatController : Controller
    {
        Uri baseAddress = new Uri("http://localhost:5102/api");
        private object encoding;
        private object ex;
        private readonly HttpClient _client;
        public TempatController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;  
        }

        [HttpGet]
        public IActionResult Index()
        {
            TempatViewModel tempat = new TempatViewModel();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Get/WebSpasial").Result;
            List<TempatViewModel>? tempatList = null;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;

                if (data.StartsWith("{"))
                {
                    TempatViewModel singleItem = JsonConvert.DeserializeObject<TempatViewModel>(data);
                    tempatList = new List<TempatViewModel> { singleItem };
                }
                else if (data.StartsWith("["))
                {
                    tempatList = JsonConvert.DeserializeObject<List<TempatViewModel>>(data);
                }
            }

            return View(tempatList ?? new List<TempatViewModel>());


        }

        [HttpGet]
        public IActionResult create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult create(TempatViewModel tempat)
        {
            try
            {
                string data = JsonConvert.SerializeObject(tempat);
                StringContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Post/WebSpasial/TambahData", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Data Berhasil Ditambahkan!!";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return View(tempat);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                TempatViewModel tempat = new TempatViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Get/WebSpasial/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    tempat = JsonConvert.DeserializeObject<TempatViewModel>(data);
                }
                return View(tempat);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }

        }

        [HttpPost]
        public IActionResult Edit(TempatViewModel model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + "/Put/WebSpasial/UbahData/", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Data Berhasil Diubah!!";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return View();
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                TempatViewModel tempat = new TempatViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Get/WebSpasial/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    tempat = JsonConvert.DeserializeObject<TempatViewModel>(data);
                }
                return View(tempat);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }

        }

        [HttpPost,ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress + "/Delete/WebSpasial/HapusData/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Data Berhasil Dihapus!!";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return View();
        }

    }
}
