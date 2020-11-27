using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BetterWareAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BetterWareController : ControllerBase
    {
        //Entregue un zip con un Webservice en C# y protocolo, REST el cual tenga dos endpoints:

    
        /* 
            ● “agruparAsociadas” que obteniendo json con un conjunto asociativo de asociadas y
            distribuidoras devuelva la relación de las distribuidoras con sus asociadas
            por ejemplo:
            Entrada:
            {"Asoc1":"Dist1","Asoc2":"Dist2","Asoc3":"Dist1","Asoc4":"Dist2"}
            Salida:
            {"Dist1":["Asoc1","Asoc3"],"Dist2":["Asoc2","Asoc4"]}
           
        */
        
        [HttpPost]
        public async Task<Dictionary<string, string[]>> AgruparAsociadas(Dictionary<string, string> Items)
        {
            var dic = new Dictionary<string, string[]>();

            await Task.Run(() =>
            {
                foreach (var kvp in Items)
                {
                    if (dic.ContainsKey(kvp.Value))
                    {
                        var lst = dic[kvp.Value].ToList();
                        lst.Insert(lst.Count(), kvp.Key);
                        var myArray = lst.ToArray<string>();
                        dic[kvp.Value] = myArray;
                    }
                    else
                        dic.Add(kvp.Value, new string[] { kvp.Key });
                }

            });
            return dic;
        }

        /*         
         ● “inegi” que implemente una función que al recibir como parámetro un arreglo de arreglos, con
            el año de nacimiento y muerte para un conjunto grande de información nos devuelve el primer
            año en el que más personas estaban con vida
            por ejemplo:
            Entrada:
            [[1951, 2018], [1981, 2000], [1983, 1984]]
            Salida:
            [1983]
            Entrada:
            [[1951, 2018], [1981, 2000], [1980, 1982], [1983, 1984]]
            Salida:
            [1981]
            Entrada:
            [[1951, 2018], [1981, 2000], [1980, 1982], [1983, 1984], [2000, 2018], [2003, 2016], [2005,
            2015]]
            Salida:
            [2005]
        */
        
        [HttpPost]
        public async Task<int> Inegi(int[][] Items)
        {
            var dict = new Dictionary<int, int>();
            int count = 0;

            await Task.Run(() =>
            {
                for (int n = 0; n < Items.Length; n++)
                {
                    for (int k = 0; k < Items.Length; k++)
                    {
                        if (Items[n][0] > Items[k][0] && Items[n][0] < Items[k][1])
                        {
                            count++;
                        }
                    }
                    if(!dict.ContainsKey(count))
                      dict.Add(count, Items[n][0]);

                    count = 0;
                }
                count = dict.OrderByDescending(x => x.Key).First().Value;
            });
            return count;
        }
    }
}
