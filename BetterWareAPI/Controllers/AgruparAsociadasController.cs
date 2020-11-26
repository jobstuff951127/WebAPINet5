using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BetterWareAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AgruparAsociadasController : ControllerBase
    {
      
        [HttpPost]
        public Dictionary<string,string[]> Get(Dictionary<string,string> Items)
        {
            var dic = new  Dictionary<string,string[]>();

            foreach(var kvp in Items)
            {
                if (dic.ContainsKey(kvp.Value))
                {
                    var lst = dic[kvp.Value].ToList();
                    lst.Insert(lst.Count(),kvp.Key);
                    var myArray = lst.ToArray<string>();
                    dic[kvp.Value] = myArray;
                } 
                else
                {
                    dic.Add(kvp.Value,new string[] {kvp.Key});
                }  
               
            }
            return dic;
        }
    }
}
