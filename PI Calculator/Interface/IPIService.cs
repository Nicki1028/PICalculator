using PI_Calculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PI_Calculator.Interface
{
    public interface IPIService
    {
        public void Start(CancellationToken token);
        public List<PIModel> GetResult();
        public void PiMissionRequest(long samplesize);
    }
}
