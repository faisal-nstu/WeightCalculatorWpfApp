using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using WeightCalculatorWpfApp.Models;

namespace WeightCalculatorWpfApp.Viewmodels
{
    public class MainViewViewModel
    {
        public PersonData CurrentPersonData { get; set; }
        public ObservableCollection<string> Dresses { get; set; }
        public string SelectedDress { get; set; }

        
        public MainViewViewModel()
        {
            CurrentPersonData = new PersonData();
            var dressList = new List<string>() { "Cbt Uniform", "Games Dress" };
            Dresses = new ObservableCollection<string>(dressList);
        }
    }
}
