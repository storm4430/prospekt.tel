using System;
using System.Linq;
using System.Web.Http;
using prospekt.tel.Models;
using System.IO;
using System.Web;

namespace prospekt.tel.Controllers.Api
{
    [Authorize]
    public class PersonPhotosController : ApiController
    {
        private psEnt db = new psEnt();

        public string Get(int id)
        {
            var imagePath = db.usp_GetPersonPhoto(id).FirstOrDefault();
            if (imagePath == "")
            {
                return "data:image/jpeg;base64,/9j/4AAQSkZJRgABAQAAAQABAAD/2wCEAAkGBxMHBhAQEBQQEhAVEg4QDRQVDQ8NDxYPFREWFxUUExYYHTQsGBolGxUVITEhJSkrLi4uFyAzODMsNygtLysBCgoKBQUFDgUFDisZExkrKysrKysrKysrKysrKysrKysrKysrKysrKysrKysrKysrKysrKysrKysrKysrKysrK//AABEIAKwBJAMBIgACEQEDEQH/xAAaAAEBAAMBAQAAAAAAAAAAAAAAAQQFBgMC/8QAMxABAAIABAMGBAUEAwAAAAAAAAECAwQFERIhMUFRYXGBkRMiscEyNKHR4WKC8PFCUnL/xAAUAQEAAAAAAAAAAAAAAAAAAAAA/8QAFBEBAAAAAAAAAAAAAAAAAAAAAP/aAAwDAQACEQMRAD8A74AAAAAAAAAAAAAAHxjY1cDD4rTtH+dAfY1OJrPP5a+8/aGPiariXnltWPCN/qDfDn66piV7YnzrDMyurxe214iPGOnqDaCVnijeOcdigAAAAAAAAAAAAAAAAAAAAAAAAAAAAAATPDG89O1zWczE5nGmZ6f8Y7obvU7cGRv5RHvLnQAAAAbLRszNcb4c9J328JbpylLcF4mOsTvDqcO3HSJ74iQfQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAMDWvyf90b/AKtE6TUKfEyV48N49ObmwAAAAHT5T8rT/wA1+jmHVYUxbCjbnG0bA+gAAAAAAAAAAAAAAAAAAAAAAAAAAAAAYer/AJGfOu/u590WpxNsjfbuj6w50AAAAB0uRrwZOkf0x+7nMPCtiz8sTPZyjd1GFHDhViesRET57A+gAAAAAAAAAAAAAAAAAAAAAAAAAAAAAOsNJrOBGDak1iIiYtHKNucf7btj5/AjHy1onrETNfOAc2AAADdaFXbAtPfbl6Q2TxydYrlaRHThrPrMby9gAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAHlmsWMDAta3Tp48+T1aXV85GLtSs7xE72ns37Nv1BrQAAAbzSc38bD4J2iaxG3jVsHLYOLOBixavWG8ympVx+U/Lbx6T5SDNAAAAAAAAAAAAAAABQAAAAAAAAJnaN/cB82tFI3mYiPGdmizGp3xbcp4Y7Ijr6yw7Wm87zMzPjO4Oivn8OnW8em9vowsxrHZSPWf2akB64+Zvj/AIrTPh0j2eKgIKAgoCGygPTCzFsH8Npj15ezLw9WvXrw29Np/RgANvTWY251n0tEvumr0tPOLR7S0oDp8HMVx/w2ifr7PVycTtLodLx5x8rEzzmJmsgywAAAAAAAAABQEFAQUBBQEY+o24MlefDb3nb7slr9axOHKRHfaPaOf7A0QoCCgIKAgoCCgIKAgoCCgI2+hX+S9fGJ+32aln6Lfhzcx31n3jn+4N4KAgoCCgIKAgoCgAAAAAAANLrtt8akd0TPvP8ADdNDrU753yrX7gwAAAAAAAAAAAAAAAAGRkL/AA85Sf6oj35MdY5SDrB55bE+Nl6274jfz7XoAAAAAAAACgAAAAAAAOZz+N8fN2tHTfaPKOTeali/BydpjrPyx6uc2BBQEFAQUBBQEFAQUBBQEFAQUBu9DxeLL2r3Ty8p/ndsmh0XE4M5t/2iY9Y5x92+AAAAAAABQAAAAAAAAajXsT8FPO0/SPu1DN1e2+ft4RWI8tmEAAAAAAAAAAAAAAAAAAD0y+J8LMVt3WifTfm6pyLqcrbiytJnrNazPsD1AAAAAAAB/9k=";
            }
            else
            {
                byte[] fileData = File.ReadAllBytes(imagePath);
                var base64 = Convert.ToBase64String(fileData);

                return "data:image/jpeg;base64," + base64;
            }
        }

        [HttpPut]
        public IHttpActionResult Put(int id)
        {
            var person = db.usp_GetPersonById(id).FirstOrDefault();
            var filePartPath = System.Configuration.ConfigurationManager.AppSettings["ScansPath"].ToString();
            var httpRequest = HttpContext.Current.Request;
            var uniqueKey = person.fam.ToString().Substring(0, 1) +
                            person.im.ToString().Substring(0, 1) +
                            person.ot.ToString().Substring(0, 1) +
                            person.passport_num.ToString() + @"\";
            var postedFile = httpRequest.Files["photo"];
            if (!Directory.Exists(filePartPath + @"photos\" + uniqueKey))
            {
                Directory.CreateDirectory(filePartPath + @"photos\" + uniqueKey);
            }
            var photoFilePath = filePartPath + @"photos\" + uniqueKey + postedFile.FileName;
            postedFile.SaveAs(photoFilePath);
            try
            {
                var r = db.usp_Photos_Update(id, photoFilePath);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
            return Ok();
        }
    }
}
