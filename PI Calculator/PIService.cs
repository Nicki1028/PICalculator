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
    public class PIService:IPIService
    {
        ConcurrentQueue<long> queue = new ConcurrentQueue<long>();
        ConcurrentDictionary<long, PIModel> keyValuePairs = new ConcurrentDictionary<long, PIModel>();
        ConcurrentBag<PIModel> cache = new ConcurrentBag<PIModel>();
     
        public List<PIModel> GetResult()
        {
            List<PIModel> result = cache.ToList();
            cache.Clear();
           
            return result;
            
        }
      
        public void PiMissionRequest(long samplesize)
        {
            if (!keyValuePairs.ContainsKey(samplesize))
            {
                queue.Enqueue(samplesize);
            }

        }

        public async void Start()
        {
            
            _ = Task.Run(() =>
            {

                while (true)
                {
                    if (queue.Count > 0)
                    {
                        queue.TryDequeue(out long sampleSize);
                        PiMission piMission = new PiMission(sampleSize);
                        double result = piMission.Calculate();
                        PIModel pIModel = new PIModel(sampleSize, DateTime.Now, result);
                        keyValuePairs[sampleSize] = pIModel;
                        cache.Add(pIModel);
                        
                    }

                }
            });
        }

        public void Stop()
        {
            
        }
    }
}
