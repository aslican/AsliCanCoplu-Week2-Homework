using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Owner.API.Model;

namespace Owner.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        List<OwnerModel> ownerList = new List<OwnerModel>()
        {
            new OwnerModel
            {
                Id = 1,
                Name = "Aslı Can",
                Surname = "Çöplü",
                Date = DateTime.Now,
                Statement ="Student of Bootcamp",
                Type = "Student",

            },
            new OwnerModel
            {
                Id = 2,
                Name = "Doğuhan",
                Surname = "Altın",
                Date = DateTime.Now,
                Statement ="!(Student of Bootcamp)",
                Type = "Student",

            },
            new OwnerModel
            {
                Id = 3,
                Name = "gökhan",
                Surname = "Civelek",
                Date = DateTime.Now,
                Statement ="!(Student of Bootcamp)",
                Type = "part-time student",

            },
           
        }; 
        [HttpGet]
        [Route("GetAllOwners")]
        public IActionResult GetAllOwners()
        {
            return Ok(ownerList);
        }

        [HttpPost]
        [Route("AddOwner")]
        public IActionResult AddOwner([FromBody]OwnerModel newOwner)
        {
            
            if (newOwner.Statement.Contains("hack"))
            {
                throw new Exception();
            }
            ownerList.Add(newOwner);
            return Ok(ownerList);   
        }
        [HttpDelete("{id}")]
        
        public IActionResult DeleteOwner(int id)
        {
            var deletedOwner = ownerList.FirstOrDefault(x => x.Id == id);
            if (deletedOwner == null)
            {
                throw new Exception($"{id} owner is not found");
            }

            ownerList.Remove(deletedOwner);
            return Ok(ownerList);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateOwners([FromBody] OwnerModel updatedOwner,int id)
        {
            if(id != updatedOwner.Id)
            {
                throw new Exception("owner id cannot be changed");

            }
            var changeOwner = ownerList.FirstOrDefault(x => x.Id == id);
            if(changeOwner == null)
            {
                throw new Exception($"{id} owner is not found");
            }
            changeOwner.Name = updatedOwner.Name;
            changeOwner.Surname = updatedOwner.Surname; 
            changeOwner.Type = updatedOwner.Type;   
            changeOwner.Statement = updatedOwner.Statement; 
            return Ok(ownerList);

        }
    }
}
