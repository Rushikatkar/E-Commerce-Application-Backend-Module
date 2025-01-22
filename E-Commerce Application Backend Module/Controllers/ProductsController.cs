using E_Commerce_Backend_System.Models.Data;
using E_Commerce_Backend_System.Models.Entityes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;
using Stripe.Climate;
using System.Linq;

namespace E_Commerce_Backend_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
       AppDbContext context;
         readonly IWebHostEnvironment _env;

        public ProductsController(AppDbContext context , IWebHostEnvironment env)
        {
            this.context = context;
            _env = env;
        }
        [HttpGet("GetProduct")]
        public IActionResult GetProduct()
        {
            var res = this.context.Products.ToList();
            if (res == null)
                return NotFound();

            return Ok(res);
        }

        [HttpPost("AddProduct")]
        public IActionResult Createproduct(Models.Entityes.Product rec)
        {
            if (rec == null)
                return BadRequest();

            this.context.Products.Add(rec);
            this.context.SaveChanges();
            return Ok("Product Saved!");
        }

        [HttpPut("UpdateProduct")]
        public IActionResult UpdateProduct(Models.Entityes.Product rec)
        {
            if (rec == null)
                return BadRequest();
            this.context.Entry(rec).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            this.context.SaveChanges();

            return Ok("Product Updated!");
        }

        [HttpDelete("DeleteProduct/{id}")]
        public IActionResult DeleteProduct(int id)
        {
            if (id == 0)
                return BadRequest();

            var rec = this.context.Products.Find(id);
            if (rec == null)
                return NotFound();

            this.context.Products.Remove(rec);
            this.context.SaveChanges();

            return Ok("Product Deleted!");

        }




        //[HttpPost("Create")]
        //public IActionResult Create([FromForm] Product rec)
        //{
        //    if (rec.ProductPhoto != null && rec.ProductPhoto.Length > 0)
        //    {
        //        // Generate the folder path
        //        string folderPath = Path.Combine(_env.WebRootPath, "photo");

        //        // Ensure the directory exists
        //        if (!Directory.Exists(folderPath))
        //        {
        //            Directory.CreateDirectory(folderPath);
        //        }

        //        // Generate the full file path
        //        string fileName = rec.ProductPhoto.FileName;
        //        string actualFilePath = Path.Combine(folderPath, fileName);

        //        // Save the file to the server
        //        using (FileStream fs = new FileStream(actualFilePath, FileMode.Create))
        //        {
        //            rec.ProductPhoto.CopyTo(fs);
        //        }

        //        // Set the relative path to the file
        //        rec.ProductPhotoPath = Path.Combine("photo", fileName).Replace("\\", "/");

                
        //        // Return a success response
        //        return Ok(new { message = "Photo uploaded successfully.", photoPath = rec.ProductPhotoPath });
        //    }

        //    // Return a bad request if no file is uploaded
        //    return BadRequest(new { message = "No photo uploaded." });


        //}

        }
}
