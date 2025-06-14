using PI_Calculator.Interface;
using PI_Calculator.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PI_Calculator.ViewModels
{
    class CalcuatorViewModel
    {
        public ObservableCollection<PIModel> collections { get; } = new ObservableCollection<PIModel>();
        public List<PIModel> datas
        {
            set
            {
                foreach (var item in value)
                {
                    var model = collections.FirstOrDefault(x => x.SampleSize == item.SampleSize);
                    if ( model != null)
                    {
                        model.Value = item.Value;
                        model.Time = item.Time; 
                        model.Status = item.Status;
                    }
                }
            }
        }
        public void Add(PIModel model)
        {
            collections.Add(model);
        }
        
    }
}
