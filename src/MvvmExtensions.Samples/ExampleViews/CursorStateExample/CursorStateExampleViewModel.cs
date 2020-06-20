using MvvmExtensions.Attributes;
using MvvmExtensions.Commands;
using MvvmExtensions.PropertyChangedMonitoring;
using MvvmExtensions.Utilities;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MvvmExtensions.Samples.ExampleViews
{
    class CursorStateExampleViewModel : PropertyChangedImplementation
    {
        private bool runningAutomaticOp = false;
        private bool runningManualOp = false;
        private CursorState manualCursorState = null;

        [DependsOn(nameof(RunningAutomaticOp), nameof(RunningManualOp))]
        public ICommand RunAutomaticOperationCommand
        {
            get
            {
                return new DelegateCommand
                {
                    CanExecuteFunc = () => !RunningAutomaticOp && !RunningManualOp,
                    CommandAction = (x) => RunAutomaticOp()
                };
            }
        }

        [DependsOn(nameof(RunningAutomaticOp), nameof(RunningManualOp))]
        public ICommand StartOperationCommand
        {
            get
            {
                return new DelegateCommand
                {
                    CanExecuteFunc = () => !RunningManualOp && !RunningAutomaticOp,
                    CommandAction = (x) => StartManualOp()
                };
            }
        }

        [DependsOn(nameof(RunningAutomaticOp), nameof(RunningManualOp))]
        public ICommand StopOperationCommand
        {
            get
            {
                return new DelegateCommand
                {
                    CanExecuteFunc = () => RunningManualOp,
                    CommandAction = (x) => StopManualOp(),
                };
            }
        }

        public bool RunningAutomaticOp
        {
            get
            {
                return runningAutomaticOp;
            }
            set
            {
                if (runningAutomaticOp != value)
                {
                    runningAutomaticOp = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool RunningManualOp
        {
            get
            {
                return runningManualOp;
            }
            set
            {
                if (runningManualOp != value)
                {
                    runningManualOp = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private void RunAutomaticOp()
        {
            // 'CursorState' must be created on STA thread
            Application.Current.Dispatcher.Invoke(async () =>
            {
                RunningAutomaticOp = true;

                // 'using' is crusial to revert cursor back to the default state when we leave this block
                using (new CursorState(Cursors.Wait))
                {
                    await Task.Delay(2000);
                }

                RunningAutomaticOp = false;
            });
        }

        private void StartManualOp()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                RunningManualOp = true;
                manualCursorState = new CursorState(Cursors.Wait);
            });
        }

        private void StopManualOp()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                manualCursorState?.Dispose();
                RunningManualOp = false;
            });
        }
    }
}
