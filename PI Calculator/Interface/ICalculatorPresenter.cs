﻿using PI_Calculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PI_Calculator.Interface
{
    public interface ICalculatorPresenter
    {
        void FetchCompletedPiResults();

        PIModel AddMission(long samplesize);

        void ActivateServiceRunning(bool activate);


    }
}
