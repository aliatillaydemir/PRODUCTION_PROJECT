using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ADO_Project.Data_Access_Layer;
using ADO_Project.Models;
using System.Net.Http;
using System.Threading.Tasks; // For async methods


namespace ADO_Project.Controllers
{
    public class ProductController : Controller  //Frontend, Web App
    {
        //Product_DAL _productDAL = new Product_DAL();

        //// GET: Product
        //public ActionResult Index()
        //{
        //    var productList = _productDAL.GetAllProducts();

        //    if(productList.Count == 0)
        //    {
        //        TempData["InfoMessage"] = "Currently products not available";
        //    }
        //    return View(productList);
        //}


        //// GET: Product/Details/5
        //public ActionResult Details(int id)
        //{
        //    try
        //    {
        //        var product = _productDAL.GetProductByID(id).FirstOrDefault();
        //        if (product == null)
        //        {
        //            TempData["InfoMessage"] = "Product not available with ID" + id.ToString();
        //            return RedirectToAction("Index");
        //        }
        //        return View(product);
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["ErrorMessage"] = ex.Message;
        //        return View();
        //    }
        //}

        //// GET: Product/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Product/Create
        //[HttpPost]
        //public ActionResult Create(Product product)
        //{
        //    bool IsInserted = false;
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            IsInserted = _productDAL.InsertProduct(product);

        //            if (IsInserted)
        //            {
        //                TempData["SuccessMessage"] = "Product details saved correctly!";
        //            }
        //            else
        //            {
        //                TempData["ErrorMessage"] = "Unable to save the product details!";
        //            }
        //        }
        //        return RedirectToAction("Index");
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["ErrorMessage"] = ex.Message;
        //        return View();
        //    }
        //}


        //// GET: Product/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    var products = _productDAL.GetProductByID(id).FirstOrDefault();
        //    if(products == null)
        //    {
        //        TempData["InfoMessage"] = "Product not available with ID" + id.ToString();
        //        return RedirectToAction("Index");
        //    }
        //    return View(products);
        //}

        //// POST: Product/Edit/5
        //[HttpPost,ActionName("Edit")]
        //public ActionResult UpdateProduct(Product product)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            bool IsUpdated = _productDAL.UpdateProduct(product);  // returned false why?? çünkü Product_DAL'a post formdan id göndermemişim.

        //            if (IsUpdated)
        //            {
        //                TempData["SuccessMessage"] = "Product updated correctly!";
        //            }
        //            else
        //            {
        //                TempData["ErrorMessage"] = "Unable to update the product details!";
        //            }
        //        }
        //        return RedirectToAction("Index");
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["ErrorMessage"] = ex.Message;
        //        return View();
        //    }
        //}

        //// GET: Product/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    try
        //    {
        //        var product = _productDAL.GetProductByID(id).FirstOrDefault();

        //        if (product == null)
        //        {
        //            TempData["InfoMessage"] = "Product not available with ID" + id.ToString();
        //            return RedirectToAction("Index");
        //        }
        //        return View(product);
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["ErrorMessage"] = ex.Message;
        //        return View();
        //    }
        //}

        //// POST: Product/Delete/5
        //[HttpPost,ActionName("Delete")]
        //public ActionResult DeleteConfirmation(int id)
        //{
        //    try
        //    {
        //        string result = _productDAL.DeleteProduct(id);

        //        if (result.Contains("deleted"))
        //        {
        //            TempData["SuccessMessage"] = result;
        //        }
        //        else
        //        {
        //            TempData["ErrorMessage"] = result;
        //        }
        //        return RedirectToAction("Index");

        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["ErrorMessage"] = ex.Message;
        //        return View();
        //    }
        //}


        private readonly HttpClient _httpClient;

        public ProductController()
        {
            // Initialize HttpClient
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:52715/") // Replace with your API base URL
            };
        }

        public async Task<ActionResult> Index()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("api/ProductsWeb");

            if (response.IsSuccessStatusCode)
            {
                var productList = await response.Content.ReadAsAsync<IEnumerable<Product>>();

                if (!productList.Any())
                {
                    TempData["InfoMessage"] = "Currently products not available";
                }

                return View(productList);
            }
            else
            {
                TempData["ErrorMessage"] = "Error retrieving products.";
                return View(new List<Product>());
            }
        }

        // GET: Product/Details/5
        public async Task<ActionResult> Details(int id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"api/ProductsWeb/{id}");
            if (response.IsSuccessStatusCode)
            {
                var products = await response.Content.ReadAsAsync<IEnumerable<Product>>();

                // Liste içinde sadece bir ürün olduğunu varsayıyoruz
                var product = products.FirstOrDefault();

                if (product == null)
                {
                    TempData["InfoMessage"] = "Product not available with ID " + id.ToString();
                    return RedirectToAction("Index");
                }
                return View(product);
            }
            else
            {
                TempData["ErrorMessage"] = "Error retrieving product details.";
                return RedirectToAction("Index");
            }
        }

        // GET: Product/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"api/ProductsWeb/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var products = await response.Content.ReadAsAsync<IEnumerable<Product>>();
                    // Liste içinde sadece bir ürün olduğunu varsayıyoruz
                    var product = products.FirstOrDefault();

                    if (product == null)
                    {
                        TempData["InfoMessage"] = "Product not available with ID " + id.ToString();
                        return RedirectToAction("Index");
                    }
                    return View(product);
                }
                else
                {
                    TempData["ErrorMessage"] = "Error retrieving product details.";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        // POST: Product/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"api/ProductsWeb/{product.ProductID}", product);

                    if (response.IsSuccessStatusCode)
                    {
                        TempData["SuccessMessage"] = "Product updated correctly!";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Unable to update the product details!";
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View(product);
            }
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public async Task<ActionResult> Create(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // API'ye POST isteği gönderme
                    HttpResponseMessage response = await _httpClient.PostAsJsonAsync("api/ProductsWeb", product);

                    if (response.IsSuccessStatusCode)
                    {
                        TempData["SuccessMessage"] = "Product created successfully!";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Error while creating product. Please try again.";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return View(product);
        }

        // GET: Product/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"api/ProductsWeb/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var products = await response.Content.ReadAsAsync<IEnumerable<Product>>();
                    // Liste içinde sadece bir ürün olduğunu varsayıyoruz
                    var product = products.FirstOrDefault();

                    if (product == null)
                    {
                        TempData["InfoMessage"] = "Product not found with ID " + id.ToString();
                        return RedirectToAction("Index");
                    }
                    return View(product);  // Delete.cshtml sayfasına ürün bilgisi ile yönlendirilir
                }
                else
                {
                    TempData["ErrorMessage"] = "Error retrieving product details.";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        // DELETE: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmation(int id)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync($"api/ProductsWeb/{id}");

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Product deleted successfully!";
                }
                else
                {
                    TempData["ErrorMessage"] = "Failed to delete the product.";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return RedirectToAction("Index");
        }


    }

}
