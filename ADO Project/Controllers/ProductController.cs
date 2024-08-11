using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ADO_Project.Data_Access_Layer;
using ADO_Project.Models;


namespace ADO_Project.Controllers
{
    public class ProductController : Controller
    {
        Product_DAL _productDAL = new Product_DAL();

        // GET: Product
        public ActionResult Index()
        {
            var productList = _productDAL.GetAllProducts();
         
            if(productList.Count == 0)
            {
                TempData["InfoMessage"] = "Currently products not available";
            }
            return View(productList);
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(Product product)
        {
            bool IsInserted = false;
            try
            {
                if (ModelState.IsValid)
                {
                    IsInserted = _productDAL.InsertProduct(product);

                    if (IsInserted)
                    {
                        TempData["SuccessMessage"] = "Product details saved correctly!";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Unable to save the product details!";
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }


        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            var products = _productDAL.GetProductByID(id).FirstOrDefault();
            if(products == null)
            {
                TempData["InfoMessage"] = "Product not available with ID" + id.ToString();
                return RedirectToAction("Index");
            }
            return View(products);
        }

        // POST: Product/Edit/5
        [HttpPost,ActionName("Edit")]
        public ActionResult UpdateProduct(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool IsUpdated = _productDAL.UpdateProduct(product);

                    if (IsUpdated)
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
                return View();
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var product = _productDAL.GetProductByID(id).FirstOrDefault();

                if (product == null)
                {
                    TempData["InfoMessage"] = "Product not available with ID" + id.ToString();
                    return RedirectToAction("Index");
                }
                return View(product);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        // POST: Product/Delete/5
        [HttpPost,ActionName("Delete")]
        public ActionResult DeleteConfirmation(int id)
        {
            try
            {
                string result = _productDAL.DeleteProduct(id);

                if (result.Contains("deleted"))
                {
                    TempData["SuccessMessage"] = result;
                }
                else
                {
                    TempData["ErrorMessage"] = result;
                }
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }
    }
}
