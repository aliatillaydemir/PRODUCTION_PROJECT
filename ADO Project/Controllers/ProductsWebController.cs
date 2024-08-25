using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ADO_Project.Data;
using ADO_Project.Data_Access_Layer;
using ADO_Project.Models;

namespace ADO_Project.Controllers
{
    public class ProductsWebController : ApiController       //Backend, Web API/REST
    {
        //private ADO_ProjectContext db = new ADO_ProjectContext();
        Product_DAL _productDAL = new Product_DAL();

        // GET: api/ProductsWeb
        public IQueryable<Product> GetProducts()
        {
            var productList = _productDAL.GetAllProducts().AsQueryable();

            if (productList.Count() == 0)
            {
                Console.WriteLine("No Product");
            }
            return productList;
        }

        // GET: api/ProductsWeb/18
        [ResponseType(typeof(Product))]
        public IHttpActionResult GetProduct(int id)
        {
            var productFind = _productDAL.GetProductByID(id);
    
            if (productFind == null)
            {
                return NotFound();
            }

            return Ok(productFind);
        }

        // PUT: api/ProductsWeb/5              //güncelleme için yani edit sayfası için PUT, Sıfırdan yaratmak için POST.
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProduct(int id, Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product.ProductID)
            {
                return BadRequest();
            }

            bool isUpdated = _productDAL.UpdateProduct(product);

            if (isUpdated)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                return BadRequest("Failed to update product");
            }

        }

        // POST: api/ProductsWeb
        [ResponseType(typeof(Product))]
        public IHttpActionResult PostProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // Ürün veritabanına kaydediliyor
                bool isAdded = _productDAL.InsertProduct(product);

                if (isAdded)
                {
                    // 201 Created yanıtı döndürülür ve yeni ürünle birlikte geri gönderilir
                    return CreatedAtRoute("DefaultApi", new { id = product.ProductID }, product);
                }
                else
                {
                    // Veritabanına ekleme başarısız olursa hata döndürülür
                    return BadRequest("Unable to add the product to the database.");
                }
            }
            catch (Exception ex)
            {
                // Hata durumunda, detaylı bilgiyle birlikte iç sunucu hatası döndürülür
                return InternalServerError(ex);
            }
        }

        // DELETE: api/ProductsWeb/5
        [HttpDelete]
        [ResponseType(typeof(string))]
        public IHttpActionResult DeleteProduct(int id)
        {
            try
            {
                string result = _productDAL.DeleteProduct(id);

                if (string.IsNullOrEmpty(result) || result.ToLower().Contains("failed"))
                {
                    return Content(HttpStatusCode.InternalServerError, "Failed to delete the product. Please check the provided ID.");
                }
                return Ok(result);
            }
            catch (Exception ex) 
            {
                return InternalServerError(ex);
            }

        }
        
    }
}