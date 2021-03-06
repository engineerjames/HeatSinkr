﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HeatSinkr.UI.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public List<string> ModelOutputs { get; set; } = new List<string>();

        public event PropertyChangedEventHandler PropertyChanged;
                
        protected void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
            {
                SyncModelOutputs();
                return false;
            }

            field = value;
            OnPropertyChanged(propertyName);
            SyncModelOutputs();
            return true;
        }

        private void SyncModelOutputs()
        {
            foreach (string Property in ModelOutputs)
                OnPropertyChanged(Property);
        }
    }
}
