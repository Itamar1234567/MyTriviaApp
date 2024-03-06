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
        private bool isOrdered;
        private Service service;
        private Player selectedPlayer;
        public Player SelectedPlayer { get => selectedPlayer;set { selectedPlayer = value; OnPropertyChanged(); } }
        public BestScoresPageViewModel(Service service)
        {
            this.service = service;

            Players = new ObservableCollection<Player>();
            LoadPlayersCommand = new Command(async () => await LoadPlayers());
            RefreshCommand = new Command(async () => await Refresh());
        }

        private async Task LoadPlayers()
        {
            IsRefreshing = false;
            var fullList = await service.GetStudentsDeccending();
            Players.Clear();
            foreach (var player in fullList)
            {
                Players.Add(player);
            }
            isOrdered = true;
        }
        private async Task ChangeOrder()
        {
            IsRefreshing = true;
            var fullList = await service.GetStudentsAccending();
            Players.Clear();
            foreach (var player in fullList)
            {
                Players.Add(player);
            }
            isOrdered = false;
        }
        private async Task Refresh()
        {
            IsRefreshing = true;
            if(!isOrdered)
                await LoadPlayers();
            else 
                await ChangeOrder();
            IsRefreshing = false;
        }
    }
}
