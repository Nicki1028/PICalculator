using PI_Calculator.Interface;
using PI_Calculator.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PI_Calculator
{
    public class PIService : IPIService
    {
        ConcurrentQueue<PIModel> queue = new ConcurrentQueue<PIModel>();
        ConcurrentDictionary<long, PIModel> keyValuePairs = new ConcurrentDictionary<long, PIModel>();
        ConcurrentBag<PIModel> cache = new ConcurrentBag<PIModel>();
        
        
        public List<PIModel> GetResult()
        {
            List<PIModel> result = cache.ToList();
            cache.Clear();
           
            return result;
            
        }
      
        public PIModel PiMissionRequest(long samplesize)
        {
            //Step1:
            //前端會輸入sampleSize請求丟給後端Presenter
            //後端Presenter會呼叫Service的PiMissionRequest(傳入SampleSize) 回傳 CancellationTokenSource (憑證)
            //service會將這個請求(sample & token )放入佇列中

            //Step2:
            //前端會在呼叫完Prsenter的PiMissionRequest後得到 CancellationTokenSource
            //前端會將該次請求存放到ViewModel (包含token)
            //ViewModel 根據添加的資料渲染dataGrid 顯示資料(創建button & 綁定button&token 關係)
            //前端當buttonClick被點擊的時候，將token狀態改為Cancel 就能自動連動service去取消任務

            CancellationTokenSource taskcts = new CancellationTokenSource();
            PIModel pIModel = new PIModel(samplesize, DateTime.Now, 0, "Pending", taskcts);
            if (!keyValuePairs.ContainsKey(samplesize))
            {             
                queue.Enqueue(pIModel);
            }
            return pIModel;
        }

        public async Task Start(CancellationToken token)
        { 
            
            await Task.Run(() =>
            {

                while (true)
                {
                    if (token.IsCancellationRequested)
                    {                       
                        break;
                    }
                    if (queue.Count > 0 && queue.TryDequeue(out PIModel? piModel))
                    {
                        if (piModel == null)
                            continue;

                        Task.Run(async () =>
                        {
                            PiMission piMission = new PiMission(piModel.SampleSize);
                            await piMission.EstimatePiWithForEachAsync(piModel);
                            keyValuePairs[piModel.SampleSize] = piModel;
                            cache.Add(piModel);
                        });
                    }

                }
            });
            
        }

       
    }
}
