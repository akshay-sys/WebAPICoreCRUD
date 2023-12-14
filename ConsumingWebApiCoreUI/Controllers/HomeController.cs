using ConsumingWebApiCoreUI.Models;
using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsumingWebApiCoreUI.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<Products> productList = new List<Products>();
            using (var httpClient = new HttpClient())
            {
                using (var responce = await httpClient.GetAsync("https://localhost:44373/api/products"))
                {
                    string apiResponse = await responce.Content.ReadAsStringAsync();
                    productList = JsonConvert.DeserializeObject<List<Products>>(apiResponse);
                }
            }

            return View(productList);
        }


        public ViewResult GetProduct() => View();

        [HttpPost]
        public async Task<IActionResult> GetProduct(int id)
        {
            Products products = new Products();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44373/api/products/" + id))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        products = JsonConvert.DeserializeObject<Products>(apiResponse);
                    }
                    else
                    {
                        ViewBag.StatusCode = response.StatusCode;
                    }
                }
            }
            return View(products);
        }

        public ViewResult AddProduct() => View();

        [HttpPost]
        public async Task<IActionResult> AddProduct(Products product)
        {
            Products receivedProducts = new Products();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:44373/api/products", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    receivedProducts = JsonConvert.DeserializeObject<Products>(apiResponse);
                }
            }
            return RedirectToAction("Index", receivedProducts);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProduct(int id)
        {
            Products product = new Products();
            var httpClient = new HttpClient();

            var request = new HttpRequestMessage(HttpMethod.Get, $"https://localhost:44373/api/products/{id}");
            var response = await httpClient.SendAsync(request);
            string apiResponse = await response.Content.ReadAsStringAsync();
            product = JsonConvert.DeserializeObject<Products>(apiResponse);

            return View(product);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProduct(Products products)
        {
            Products receivedProducts = new Products();

            var httpClient = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Put, $"https://localhost:44373/api/products/{products.productId}")
            {
                Content = new StringContent(new JavaScriptSerializer().Serialize(products), Encoding.UTF8, "application/json")
            };
            var response = await httpClient.SendAsync(request);
            string apiResponse = await response.Content.ReadAsStringAsync();
            receivedProducts = JsonConvert.DeserializeObject<Products>(apiResponse);

            return RedirectToAction("Index", receivedProducts);
        }



        [Route("DeleteProduct/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            // var httpClient = new HttpClient();
            Products ProductDel = new Products();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("https://localhost:44373/api/products/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }

            return RedirectToAction("Index");

        }

    }
}
