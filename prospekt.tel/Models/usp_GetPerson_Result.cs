//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace prospekt.tel.Models
{
    using System;
    
    public partial class usp_GetPerson_Result
    {
        public int id { get; set; }
        public string fam { get; set; }
        public string im { get; set; }
        public string ot { get; set; }
        public int sex { get; set; }
        public System.DateTime birthday { get; set; }
        public System.DateTime created { get; set; }
        public System.DateTime updated { get; set; }
        public string person_comment { get; set; }
        public int person_photo { get; set; }
        public int person_passport { get; set; }
        public string passport_serie { get; set; }
        public string passport_num { get; set; }
        public bool sysStatus { get; set; }
        public string cellPhone { get; set; }
        public string pers_photo { get; set; }
        public string pass_scan { get; set; }
    }
}