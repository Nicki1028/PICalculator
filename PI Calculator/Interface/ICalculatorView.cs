﻿using PI_Calculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PI_Calculator.Interface
{
    public interface ICalculatorView
    {
        void UpdatePIResult(List<PIModel> pIModels);
    }
}
