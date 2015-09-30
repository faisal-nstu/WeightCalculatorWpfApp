using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using WeightCalculatorWpfApp.Models;

namespace WeightCalculatorWpfApp.Viewmodels
{
    public class MainViewViewModel
    {
        private List<double> _heightsInch; 
        private List<double> _heightsCm;
        private List<double> _allAgesMinWeightKg;
        private List<double> _allAgesMinWeightLb;
        private List<double> _upto30MaxWeightKg;
        private List<double> _upto30MaxWeightLb;
        private List<double> _upto40MaxWeightKg;
        private List<double> _upto40MaxWeightLb;
        private List<double> _upto50MaxWeightKg;
        private List<double> _upto50MaxWeightLb;
        private List<double> _over50WeightKg;
        private List<double> _over50Weightlb;

        public PersonData CurrentPersonData { get; set; }
        public ObservableCollection<string> Dresses { get; set; }
        public ObservableCollection<string> WeightUnits { get; set; }
        public ObservableCollection<string> HeightUnits { get; set; }
        public ObservableCollection<string> AgeGroups { get; set; }
        public string SelectedDress { get; set; }
        public string SelectedAgeGroup { get; set; }
        public string SelectedHeightUnit { get; set; }
        public string SelectedWeightUnit { get; set; }

        public MainViewViewModel()
        {
            
        }

        public void InitForm()
        {
            CurrentPersonData = new PersonData();
            var dressList = new List<string>() { "Cbt Uniform", "Games Dress" };
            Dresses = new ObservableCollection<string>(dressList);
            var weightUnitList = new List<string>() { "kg", "lb" };
            WeightUnits = new ObservableCollection<string>(weightUnitList);
            SelectedWeightUnit = WeightUnits.FirstOrDefault();
            var heightUnitList = new List<string>() {"in","cm"};
            HeightUnits = new ObservableCollection<string>(heightUnitList);
            SelectedHeightUnit = HeightUnits.FirstOrDefault();
            //var ageGroups = new List<string>() { "Upto 30 yrs", "31 - 40 yrs", "41 - 50 yrs", "Over 50 yrs" };
            var ageGroups = new List<string>(){AgeGroup.Upto30Yrs,AgeGroup.Upto40Yrs,AgeGroup.Upto50Yrs,AgeGroup.Over50Yrs};
            AgeGroups = new ObservableCollection<string>(ageGroups);

            _heightsCm = new List<double>() { 157.5, 160.0, 162.5, 165.0, 167.5, 170.0, 172.5, 175.0, 177.5, 180.0, 182.5, 185.0, 187.5 };
            _heightsInch = new List<double>() { 62, 63, 64, 65, 66, 67, 68, 69, 70, 71, 72, 73, 74 };
            
            _allAgesMinWeightKg = new List<double>() { 47, 49, 50, 52, 53, 55, 57, 58, 60, 62, 64, 65, 67 };
            _allAgesMinWeightLb = new List<double>(){104,107,110,114,117,121,125,128,132,136,140,144,148};
            
            _upto30MaxWeightKg = new List<double>() {62,64,66,68,70,72,75,77,79,81,84,86,88};
            _upto30MaxWeightLb = new List<double>() {136,141,145,150,155,159,164,169,174,174,184,189,194};

            _upto40MaxWeightKg = new List<double>() {66,68,70,72,74,77,79,81,84,86,89,91,94};
            _upto40MaxWeightLb = new List<double>() {145,149,154,159,164,169,174,179,184,190,195,200,206};

            _upto50MaxWeightKg = new List<double>() {67,69,71,74,76,78,81,83,85,88,90,93,95};
            _upto50MaxWeightLb = new List<double>() {147,152,157,162,167,172,177,182,188,193,199,204,210};

            _over50WeightKg = new List<double>() {68,70,73,75,77,80,82,84,87,89,92,95,97};
            _over50Weightlb = new List<double>() {150,155,160,165,170,175,180,186,191,197,202,208,214};
        }

        public void Calculate()
        {
            double idealWeight;
            double dressDeductedWeightKg = SelectedDress == Dresses[0] ? CurrentPersonData.Weight - 3 : CurrentPersonData.Weight - 1;
            double dressDeductedWeightLb = SelectedDress == Dresses[0] ? CurrentPersonData.Weight - 7 : CurrentPersonData.Weight - 2;
            int rowIndex = 0;
            rowIndex = GetRowIndex();
            //MessageBox.Show(rowIndex.ToString());
            //return;
            if (CheckIfUnderweight(rowIndex, dressDeductedWeightKg,dressDeductedWeightLb)) return;
            idealWeight = GetWeightDifference(rowIndex);
            double differenceFromIdeal = SelectedWeightUnit == "kg"
                ? idealWeight - dressDeductedWeightKg
                : idealWeight - dressDeductedWeightLb;

            if (differenceFromIdeal > 0)
            {
                MessageBox.Show(differenceFromIdeal +" "+ SelectedWeightUnit + " UNDERWEIGHT");
            }
            else if (differenceFromIdeal < 0)
            {
                MessageBox.Show((differenceFromIdeal *(-1)) + " " + SelectedWeightUnit + " OVERWEIGHT");
            }
            else
            {
                MessageBox.Show("Perfect!!!");
            }
        }

        private double GetWeightDifference(int rowIndex)
        {
            double idealWeight = 0;
            if (CurrentPersonData.AgeGroup == AgeGroup.Upto30Yrs)
            {
                if (SelectedWeightUnit.ToLower() == "kg")
                {
                    idealWeight = _upto30MaxWeightKg[rowIndex];
                }
                else
                {
                    idealWeight = _upto30MaxWeightLb[rowIndex];
                }
            }
            if (CurrentPersonData.AgeGroup == AgeGroup.Upto40Yrs)
            {
                if (SelectedWeightUnit.ToLower() == "kg")
                {
                    idealWeight = _upto40MaxWeightKg[rowIndex];
                }
                else
                {
                    idealWeight = _upto40MaxWeightLb[rowIndex];
                }
            }
            if (CurrentPersonData.AgeGroup == AgeGroup.Upto50Yrs)
            {
                if (SelectedWeightUnit.ToLower() == "kg")
                {
                    idealWeight = _upto50MaxWeightKg[rowIndex];
                }
                else
                {
                    idealWeight = _upto50MaxWeightLb[rowIndex];
                }
            }
            if (CurrentPersonData.AgeGroup == AgeGroup.Over50Yrs)
            {
                if (SelectedWeightUnit.ToLower() == "kg")
                {
                    idealWeight = _over50WeightKg[rowIndex];
                }
                else
                {
                    idealWeight = _over50Weightlb[rowIndex];
                }
            }
            return idealWeight  ;
        }

        private bool CheckIfUnderweight(int rowIndex, double dressDeductedWeightKg, double dressDeductedWeightLb)
        {
            if (SelectedWeightUnit == "kg")
            {
                if (dressDeductedWeightKg <= _allAgesMinWeightKg[rowIndex])
                {
                    MessageBox.Show(_allAgesMinWeightKg[rowIndex] - dressDeductedWeightKg + " kg UNDERWEIGHT");
                    return true;
                }
            }
            else
            {
                if (dressDeductedWeightLb <= _allAgesMinWeightLb[rowIndex])
                {
                    MessageBox.Show(_allAgesMinWeightLb[rowIndex] - dressDeductedWeightLb + " lb UNDERWEIGHT");
                    return true;
                }
            }
            return false;
        }
        private int GetRowIndex()
        {
            int rowIndex = 0;
            if (SelectedHeightUnit == "in")
            {
                for (int i = 0; i < _heightsInch.Count; i++)
                {
                    if (i == _heightsInch.Count - 1)
                    {
                        if (CurrentPersonData.Height >= _heightsInch[i])
                        {
                            rowIndex = i;
                            break;
                        }
                    }
                    else 
                    {
                        if (CurrentPersonData.Height >= _heightsInch[i] && CurrentPersonData.Height < _heightsInch[i+1])
                        {
                            rowIndex = i;
                            break;
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < _heightsCm.Count; i++)
                {
                    if (i == _heightsCm.Count - 1)
                    {
                        if (CurrentPersonData.Height >= _heightsCm[i])
                        {
                            rowIndex = i;
                            break;
                        }
                    }
                    else
                    {
                        if (CurrentPersonData.Height >= _heightsCm[i] && CurrentPersonData.Height < _heightsCm[i + 1])
                        {
                            rowIndex = i;
                            break;
                        }
                    }
                } 
            }
            return rowIndex;
        }
    }
}
