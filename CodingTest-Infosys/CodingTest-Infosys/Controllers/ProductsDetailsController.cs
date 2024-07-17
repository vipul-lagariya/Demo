using CodingTest_Infosys.Models;
using CodingTest_Infosys.Repository;
using CodingTest_Infosys.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CodingTest_Infosys.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsDetailsController : ControllerBase
    {
        private readonly IOptions<MySettingsModel> appSettings;

        public ProductsDetailsController(IOptions<MySettingsModel> app)
        {
            appSettings = app;
        }

        [HttpGet]
        [Route("GetAllDetails")]
        public IActionResult GetAllDetails()
        {
            var data = DbClient<DBClientCall>.Instance.GetAllDetails(appSettings.Value.DbConn);
            return Ok(data);
        }

        [HttpPost]
        [Route("SaveDetails")]
        public IActionResult SaveDetails([FromBody] ProductDetails model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var msg = new Message<ProductDetails>();
                    var data = DbClient<DBClientCall>.Instance.SaveDetails(model, appSettings.Value.DbConn);
                    if (data == "C200")
                    {
                        msg.IsSuccess = true;
                        if (model.Id == 0)
                            msg.ReturnMessage = "Details saved successfully";
                        else
                            msg.ReturnMessage = "Details updated successfully";
                    }
                    return Ok(msg);
                }
                else
                {
                    return BadRequest(ModelState);
                }
                
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("DeleteDetails")]
        public IActionResult DeleteDetails([FromBody] ProductDetails model)
        {
            var msg = new Message<ProductDetails>();
            var data = DbClient<DBClientCall>.Instance.DeleteDetails(model.Id, appSettings.Value.DbConn);
            if (data == "C200")
            {
                msg.IsSuccess = true;
                msg.ReturnMessage = "Details Deleted";
            }
            else if (data == "C203")
            {
                msg.IsSuccess = false;
                msg.ReturnMessage = "Invalid record";
            }
            return Ok(msg);
        }
    }
}
