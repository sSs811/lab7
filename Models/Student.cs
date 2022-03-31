using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Table.Models
{
    [Serializable]
    public class Student : INotifyPropertyChanged
    {
        public string Name { set; get; }

        ObservableCollection<Balls> control;
        public ObservableCollection<Balls> Control
        {
            get
            {
                return control;
            }
            set
            {
                this.control = value;
                RaisePropertyChangedEvent("Control");
            }
        }

        float? average;
        [XmlIgnore]
        Avalonia.Media.SolidColorBrush averageBrush;
        [XmlIgnore]
        public Avalonia.Media.SolidColorBrush AverageBrush
        {
            get
            {
                return this.averageBrush;
            }
            private set
            {
                this.averageBrush = value;
                RaisePropertyChangedEvent("AverageBrush");
            }
        }
        [XmlIgnore]
        public bool isChecked { get; set; }
        [XmlIgnore]
        public float? Average
        {
            get
            {
                return this.average;
            }
            private set
            {
                if (value is not null)
                {
                    if (value < 1.5)
                    {
                        this.AverageBrush = new SolidColorBrush(Brushes.Yellow.Color);
                        this.average = value;
                    }
                    if (value < 1)
                    {
                        this.AverageBrush = new SolidColorBrush(Brushes.Red.Color);
                        this.average = value;
                    }
                    if (value >= 1.5)
                    {
                        this.AverageBrush = new SolidColorBrush(Brushes.LightGreen.Color);
                        this.average = value;
                    }
                }
                else
                {
                    this.average = null;
                    this.AverageBrush = new SolidColorBrush(Brushes.White.Color);
                }

                RaisePropertyChangedEvent("Average");
            }
        }

        public void CalculateAverage()
        {
            if (Control.Any(mark => mark.Mark is null))
            {
                this.Average = null;
            }
            else
            {
                float sum = 0;
                foreach (Balls mark in Control)
                {
                    sum += (float)mark.Mark;
                }
                this.Average = sum / 3;
            }

        }


        public Student(string name)
        {
            this.Name = name;
            this.Control = new ObservableCollection<Balls>();
            this.Control.CollectionChanged += MyItemsSource_CollectionChanged;
            this.Control.Clear();
            Control.Add(new Balls(0));
            Control.Add(new Balls(0));
            Control.Add(new Balls(0));
            this.isChecked = false;
            CalculateAverage();
        }

        public Student()
        {
            this.Name = "NULL";
            this.Control = new ObservableCollection<Balls>();
            this.Control.CollectionChanged += MyItemsSource_CollectionChanged;
            this.Control.Clear();
            Control.Add(new Balls(0));
            Control.Add(new Balls(0));
            Control.Add(new Balls(0));
            this.isChecked = false;
            CalculateAverage();
        }

        void MyItemsSource_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
                foreach (Balls item in e.NewItems)
                    item.PropertyChanged += MyType_PropertyChanged;

            if (e.OldItems != null)
                foreach (Balls item in e.OldItems)
                    item.PropertyChanged -= MyType_PropertyChanged;
        }

        void MyType_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            CalculateAverage();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChangedEvent(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChangedEventArgs e = new PropertyChangedEventArgs(propertyName);
                PropertyChanged(this, e);
            }
        }

    }
}