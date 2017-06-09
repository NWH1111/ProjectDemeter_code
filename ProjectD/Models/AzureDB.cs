using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ProjectD.Models
{
    public class AzureDB
    {
        [Key]
        public string 縣市 { get; set; }
        public string 市集名稱 { get; set; }
        public string 市集介紹 { get; set; }
        public string 營業時間 { get; set; }
        public string 電話 { get; set; }
        public string GPS { get; set; }
        public string 營業地址 { get; set; }
        public string 交通資訊 { get; set; }
    }
}
