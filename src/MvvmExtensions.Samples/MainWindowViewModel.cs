using MvvmExtensions.Attributes;
using MvvmExtensions.PropertyChangedMonitoring;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MvvmExtensions.Samples
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private int width = 0;
        private int height = 0;

        public MainWindowViewModel()
        {
            DependencyWatcher.Register(this, NotifyPropertyChanged);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public int Width
        {
            get
            {
                return width;
            }
            set
            {
                if (width != value)
                {
                    width = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int Height
        {
            get
            {
                return height;
            }
            set
            {
                if (height != value)
                {
                    height = value;
                    NotifyPropertyChanged();
                }
            }
        }

        // Properties marked with DependsOn attribute will automatically invoke
        // NotifyPropertyChanged event and update values displayed in view
        [DependsOn(nameof(Width), nameof(Height))]
        public string RectangleArea
        {
            get
            {
                try
                {
                    return MathHelper.CalculateRectangleArea(Width, Height).ToString();
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
        }

        [DependsOn(nameof(Width), nameof(Height))]
        public string RectanglePerimeter
        {
            get
            {
                try
                {
                    return MathHelper.CalculateRectanglePerimeter(Width, Height).ToString();
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
        }

        public void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
