using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using prospekt.tel.Models;
using Microsoft.Ajax.Utilities;
using System.IO;
using System.Web;

namespace prospekt.tel.Controllers.Api
{
    [Authorize]
    public class PersonsController : ApiController
    {
        private psEnt db = new psEnt();

        public IHttpActionResult Get()
        {
            try
            {
                var result = db.usp_GetPersonFIOLetters().ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
                throw;
            }
        }

        public IHttpActionResult Get(int id)
        {
            try
            {
                var result = db.usp_GetPersonById(id).FirstOrDefault();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
                throw;
            }
        }

        public IHttpActionResult Get(string substr)
        {
            try
            {
                var t = ParseFioQuery(substr);
                var result = db.usp_GetPerson(t.Fam, t.Im, t.Ot).ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
                throw;
            }
        }

        [HttpPost]
        public IHttpActionResult Create()
        {
            var filePartPath = System.Configuration.ConfigurationManager.AppSettings["ScansPath"].ToString();
            var httpRequest = HttpContext.Current.Request;
            var uniqueKey = httpRequest.Form["fam"].ToString().Substring(0, 1) +
                            httpRequest.Form["im"].ToString().Substring(0, 1) +
                            httpRequest.Form["ot"].ToString().Substring(0, 1) +
                            httpRequest.Form["doc_num"].ToString() + @"\";
            var postedFile = httpRequest.Files["photo"];
            var photoFilePath = "";
            if (postedFile != null)
            {
                if (!Directory.Exists(filePartPath + @"photos\" + uniqueKey))
                {
                    Directory.CreateDirectory(filePartPath + @"photos\" + uniqueKey);
                }
                photoFilePath = filePartPath + @"photos\" + uniqueKey + postedFile.FileName;
                postedFile.SaveAs(photoFilePath);
            }

            var postedScanFile = httpRequest.Files["scan"];
            var scanFilePath = "";
            if (postedScanFile != null)
            {
                if (!Directory.Exists(filePartPath + @"scans\" + uniqueKey))
                {
                    Directory.CreateDirectory(filePartPath + @"scans\" + uniqueKey);
                }
                scanFilePath = filePartPath + @"scans\" + uniqueKey + postedScanFile.FileName;
                postedScanFile.SaveAs(scanFilePath);
            }

            usp_GetPerson_Result obj = new usp_GetPerson_Result()
            {
                fam = httpRequest.Form["fam"].ToString(),
                im = httpRequest.Form["im"].ToString(),
                ot = httpRequest.Form["ot"].ToString(),
                sex = Convert.ToInt32(httpRequest.Form["sex"].ToString()),
                dr = DateTime.Parse(httpRequest.Form["dr"].ToString()),
                passport_serie = httpRequest.Form["doc_ser"].ToString(),
                passport_num = httpRequest.Form["doc_num"].ToString(),
                pers_photo = photoFilePath,
                pass_scan = scanFilePath,
                cellPhone = httpRequest.Form["cellPhone"].ToString(),
                person_comment = httpRequest.Form["comment"].ToString()
            };

            try
            {
                var result = db.usp_Persons_IU(null, obj.fam, obj.im, obj.ot, obj.sex, obj.dr, obj.person_comment, obj.pers_photo, obj.pass_scan, obj.passport_serie, obj.passport_num, obj.cellPhone);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        [HttpPut]
        public IHttpActionResult Put(usp_GetPersonById_Result obj)
        {
            try
            {
                var result = db.usp_Persons_IU(obj.id, obj.fam, obj.im, obj.ot, obj.sex, obj.dr, obj.person_comment, obj.pers_photo, obj.pass_scan, obj.passport_serie, obj.passport_num, obj.cellPhone);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }


        private string LatToRusKeyboard(string query)
        {
            int code0 = (int)query[0];
            char ch0 = query[0];

            string result = query;

            if (((code0 >= 65) && (code0 <= 90)) || //A-Z
                ((code0 >= 97) && (code0 <= 122)) || //a-z
                (ch0 == ',' || ch0 == '<' //бБ
                || ch0 == '.' || ch0 == '>' //юЮ
                || ch0 == ';' || ch0 == ':' //жЖ
                || ch0 == '\'' || ch0 == '"' //эЭ
                || ch0 == '[' || ch0 == '{' //хХ
                || ch0 == ']' || ch0 == '}' //ъЪ
                || ch0 == '`' || ch0 == '~')) //ёЁ
            {
                Dictionary<char, char> arr = new Dictionary<char, char>();

                //uppers
                arr.Add('Q', 'Й');
                arr.Add('W', 'Ц');
                arr.Add('E', 'У');
                arr.Add('R', 'К');
                arr.Add('T', 'Е');
                arr.Add('Y', 'Н');
                arr.Add('U', 'Г');
                arr.Add('I', 'Ш');
                arr.Add('O', 'Щ');
                arr.Add('P', 'З');
                arr.Add('{', 'Х');
                arr.Add('}', 'Ъ');
                arr.Add('A', 'Ф');
                arr.Add('S', 'Ы');
                arr.Add('D', 'В');
                arr.Add('F', 'А');
                arr.Add('G', 'П');
                arr.Add('H', 'Р');
                arr.Add('J', 'О');
                arr.Add('K', 'Л');
                arr.Add('L', 'Д');
                arr.Add(':', 'Ж');
                arr.Add('"', 'Э');
                arr.Add('Z', 'Я');
                arr.Add('X', 'Ч');
                arr.Add('C', 'С');
                arr.Add('V', 'М');
                arr.Add('B', 'И');
                arr.Add('N', 'Т');
                arr.Add('M', 'Ь');
                arr.Add('<', 'Б');
                arr.Add('>', 'Ю');
                arr.Add('~', 'Ё');

                //lowers
                arr.Add('q', 'й');
                arr.Add('w', 'ц');
                arr.Add('e', 'у');
                arr.Add('r', 'к');
                arr.Add('t', 'е');
                arr.Add('y', 'н');
                arr.Add('u', 'г');
                arr.Add('i', 'ш');
                arr.Add('o', 'щ');
                arr.Add('p', 'з');
                arr.Add('[', 'х');
                arr.Add(']', 'ъ');
                arr.Add('a', 'ф');
                arr.Add('s', 'ы');
                arr.Add('d', 'в');
                arr.Add('f', 'а');
                arr.Add('g', 'п');
                arr.Add('h', 'р');
                arr.Add('j', 'о');
                arr.Add('k', 'л');
                arr.Add('l', 'д');
                arr.Add(';', 'ж');
                arr.Add('\'', 'э');
                arr.Add('z', 'я');
                arr.Add('x', 'ч');
                arr.Add('c', 'с');
                arr.Add('v', 'м');
                arr.Add('b', 'и');
                arr.Add('n', 'т');
                arr.Add('m', 'ь');
                arr.Add(',', 'б');
                arr.Add('.', 'ю');
                arr.Add('`', 'ё');

                result = "";

                char newCh = new char();

                foreach (char ch in query)
                {
                    arr.TryGetValue(ch, out newCh);

                    if (newCh != '\0')
                        result += arr[ch];
                    else
                        result += ch;
                }
            }


            return result;
        }

        /// <summary>
        /// Парсит входящую строку query и разбивает на поля имя,отч,фам,др
        /// понимает 3 символа, фио01012000, фам им отч
        /// предварительно преобразовывает qwerty в йцукен
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        private SearchTemplate ParseFioQuery(string query)
        {
            query = LatToRusKeyboard(query);

            SearchTemplate result = new SearchTemplate()
            {
                Fam = "",
                Im = "",
                Ot = "",
                Dr = null
            };

            if (query.Length == 3)
            {
                result.Fam = query[0].ToString();
                result.Im = query[1].ToString();
                result.Ot = query[2].ToString();
                return result;
            }

            if (query.Length == 11)
            {
                string manDateR = query.Substring(3, 8);

                int res;
                bool isInt = Int32.TryParse(manDateR, out res);

                if (isInt)
                {
                    result.Fam = query[0].ToString();
                    result.Im = query[1].ToString();
                    result.Ot = query[2].ToString();
                    result.Dr = new DateTime(Convert.ToInt32(manDateR.Substring(4, 4)), Convert.ToInt32(manDateR.Substring(2, 2)), Convert.ToInt32(manDateR.Substring(0, 2)));
                    return result;
                }
            }

            if (query.IndexOf(' ') != -1)
            {
                string[] arr = query.Split(' ');

                result.Fam = arr[0];

                if (arr.Length > 1)
                    result.Im = arr[1];

                if (arr.Length > 2)
                    result.Ot = arr[2];

                return result;
            }

            result.Fam = query;

            return result;
        }

    }

    public class SearchTemplate
    {
        public string Fam { get; set; }
        public string Im { get; set; }
        public string Ot { get; set; }
        public DateTime? Dr { get; set; }
    }
}
