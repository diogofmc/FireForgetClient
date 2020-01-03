using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using FireForgert.Responses;
using FireForgert.TaskMonitor;
using Xamarin.Forms;

namespace FireForgetClient
{
    public class MainPageViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private bool _isBusy;
        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }
            set
            {
                _isBusy = value;
                OnPropertyChanged(nameof(IsBusy));
            }
        }


        private string _taskStatus;
        public string TaskStatus
        {
            get
            {
                return _taskStatus;
            }
            set
            {
                _taskStatus = value;
                OnPropertyChanged(nameof(TaskStatus));
            }
        }

        string username = "teste";
        string password = "teste";

        public ICommand FireForgetTaskCommand { get; set; }
        public ICommand FireForgetTaskWithResultCommand { get; set; }
        public ICommand FireForgetTaskWithErrorCommand { get; set; }

        public MainPageViewModel()
        {
            FireForgetTaskCommand = new Command(FireForgetTask);
            FireForgetTaskWithResultCommand = new Command(FireForgetTaskWithResult);
            FireForgetTaskWithErrorCommand = new Command(FireForgetTaskWithError);
        }

        private void FireForgetTaskWithError(object obj)
        {
            Console.WriteLine("------------- FireForgetTaskWithError ---------------");
            TaskMonitor.Build()
                .ExecuteTaskAsync((username),
                OtherAuthenticateWithError, OtherOnAuthenticationSuccess, OtherOnAuthenticationFail);
        }

        private void FireForgetTaskWithResult(object obj)
        {
            Console.WriteLine("------------- FireForgetTaskWithResult ---------------");
            Console.WriteLine("------------- Starting ---------------");

            TaskMonitor.Build()
                .WhenStarted(()=> IsBusy = true)
                .WhenFinished(() => IsBusy = false)
                .ExecuteTaskAsync((username, password),
                Authenticate, OnAuthenticationSuccess, OnAuthenticationFail);
        }

        private void FireForgetTask(object obj)
        {
            TaskMonitor.Build()
                .RequestServiceAsync(FireForgetAuthenticate());
        }


        #region Services/Tasks

        private async Task<Response<string>> OtherAuthenticateWithError(string auth)
        {
            await Task.Delay(4000);
            return Response<string>.Failed;
        }

        private async Task<Response<bool>> Authenticate((string Username, string Password) credentials)
        {
            await Task.Delay(4000);
            return Response<bool>.SuccessedWith(true);
        }


        private async Task FireForgetAuthenticate()
        {
            await Task.Delay(4000);
            TaskStatus = "FIRE FORGET FINISHED";
        }


        #endregion

        #region Responses

        private void OnAuthenticationSuccess(bool obj)
        {
            Console.WriteLine("--------- LOGIN TASK COMPLETED WITH SUCESS------------------- " + obj);
            TaskStatus = "LOGIN TASK COMPLETED WITH SUCESS";
        }

        private void OtherOnAuthenticationSuccess(string obj)
        {
            Console.WriteLine("--------- LOGIN TASK COMPLETED WITH SUCESS------------------- " + obj);
        }

        private void OtherOnAuthenticationFail(Response<string> obj)
        {
            Console.WriteLine("--------- LOGIN TASK FAILED ------------------- " + obj.Error.Message);
            TaskStatus = "LOGIN TASK FAILED";
        }

        private void OnAuthenticationFail(Response<bool> obj)
        {
            Console.WriteLine("--------- LOGIN TASK FAILED ------------------- " + obj);
            TaskStatus = "LOGIN TASK FAILED";
        }

        #endregion
    }
}
