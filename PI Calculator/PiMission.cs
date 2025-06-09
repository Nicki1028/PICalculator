using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PI_Calculator
{
    public class PiMission
    {
        public readonly long SampleSize;

        public PiMission(long sampleSize)
        {
            this.SampleSize = sampleSize;
        }
        object lockkey = new object();
        public double Calculate()
        {
            long insideCircle = 0;
            Random random = new Random();
            ConcurrentBag<long> result = new ConcurrentBag<long>();
            Parallel.For(0, SampleSize, i =>
            {
                double x = random.NextDouble(); // 0 ~ 1
                double y = random.NextDouble(); // 0 ~ 1


                if (x * x + y * y <= 1)
                {

                    result.Add(insideCircle);

                }
            });


            return 4.0 * result.Count() / SampleSize;
        }
        public async Task<double> EstimatePiWithForEachAsync(long samplesize)
        {
            long insideCircle = 0;

            await Parallel.ForAsync(0, samplesize, async (batch, ct) =>
            {

                Random random = new(Guid.NewGuid().GetHashCode());
                double x = random.NextDouble();
                double y = random.NextDouble();

                if (x * x + y * y <= 1)
                {
                    Interlocked.Increment(ref insideCircle);    
                }

                await ValueTask.CompletedTask;
            });

            double result = 4.0 * insideCircle / samplesize;
            return await Task.FromResult(result);   
            //PIResponse response = new PIResponse { SampleSize = (int)samplesize, Value = result };
            //EventHandlers.Notify(response);
        }
        public async Task EstimatePiWithForEachAsync(long samplesize, Action<long, double> action)
        {

            long insideCircle = 0;
            object lockObj = new();

            await Parallel.ForAsync(0, samplesize, async (batch, ct) =>
            {
                
                Random random = new(Guid.NewGuid().GetHashCode());


                double x = random.NextDouble();
                double y = random.NextDouble();
                if (x * x + y * y <= 1)
                {
                    lock (lockObj)
                    {
                        insideCircle++;
                    } 
                }


                await ValueTask.CompletedTask;
            });

            double result = 4.0 * insideCircle / samplesize;
            //action.Invoke(samplesize, result);
        }
    }
}
