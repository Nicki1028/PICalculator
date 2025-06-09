using PI_Calculator.Interface;
using PI_Calculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PI_Calculator
{
    public class CalculatorPresenter : ICalculatorPresenter
    {
        private readonly ICalculatorView _view;
        private readonly IPIService _service;
        CancellationTokenSource cts = new CancellationTokenSource();
        public CalculatorPresenter(ICalculatorView view, IPIService service)
        {
            _view = view;
            _service = service;
            _service.Start(cts.Token);
          
        }

        public void AddMission(long sampleSize)
        {
            _service.PiMissionRequest(sampleSize);
        }
       

        public void FetchCompletedPiResults()
        {
            _view.UpdatePIResult(_service.GetResult());
        }




        // 幫我根據這個函數的行為重新命名這個函數名稱
        public void ActivateServiceRunning(bool activate)
        {
            if (activate)
            {
                cts = new CancellationTokenSource();
                _service.Start(cts.Token);
            }
            else
            {
                cts.Cancel();
            }
        }
        

    }
}
