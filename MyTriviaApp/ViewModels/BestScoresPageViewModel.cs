using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyTriviaApp.Services;
using MyTriviaApp.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MyTriviaApp.ViewModels
{
    public class BestScoresPageViewModel : ViewModel
    {
        public ObservableCollection<Player> Players { get; set; }
        public ICommand LoadPlayersCommand { get; private set; }
        public ICommand RefreshCommand { get; private set; }
        private bool isRefreshing;
        public bool IsRefreshing { get => isRefreshing; set { isRefreshing = value; OnPropertyChanged(); } }
        private Service service;
        public BestScoresPageViewModel(Service service)
        {
            this.service = service;

            Players = new ObservableCollection<Player>();
            LoadPlayersCommand = new Command(async () => await LoadPlayers());
            RefreshCommand = new Command(async () => await Refresh());
        }

        private async Task LoadPlayers()
        {
            var fullList = await service.GetStudents();
            Players.Clear();
            foreach (var player in fullList)
            {
                Players.Add(player);
            }
        }
        private async Task Refresh()
        {
            IsRefreshing = true;
            await LoadPlayers();
            IsRefreshing = false;
        }
    }
}
