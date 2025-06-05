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

        public CalculatorPresenter(ICalculatorView view, IPIService service)
        {
            _view = view;
            _service = service;
            _service.Start();
        }

        public void AddMission(long sampleSize)
        {
            _service.PiMissionRequest(sampleSize);
        }
       

        public void FetchCompletedPiResults()
        {
            _view.UpdatePIResult(_service.GetResult());
        }

    }
}
