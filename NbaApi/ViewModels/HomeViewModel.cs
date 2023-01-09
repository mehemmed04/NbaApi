using NbaApi.ApiEntities.Teams;
using NbaApi.Commands;
using NbaApi.Models;
using NbaApi.Services;
using NbaApi.Services.NBAApiService;
using NbaApi.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace NbaApi.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {

        private ObservableCollection<Player> allPlayers;

        public ObservableCollection<Player> AllPlayers
        {
            get { return allPlayers; }
            set { allPlayers = value; OnPropertyChanged(); }
        }


        private Response selectedTeam;

        public Response SelectedTeam
        {
            get { return selectedTeam; }
            set { selectedTeam = value; OnPropertyChanged(); }
        }

        private Player selectedPlayer;

        public Player SelectedPlayer
        {
            get { return selectedPlayer; }
            set { selectedPlayer = value;OnPropertyChanged(); }
        }



        private ObservableCollection<Response> allTeams;

        public ObservableCollection<Response> AllTeams
        {
            get { return allTeams; }
            set { allTeams = value; OnPropertyChanged(); }
        }

        private ObservableCollection<PageNo> allPages;

        public ObservableCollection<PageNo> AllPages
        {
            get { return allPages; }
            set { allPages = value; OnPropertyChanged(); }
        }


        private ObservableCollection<PageNo> allPlayerPages;

        public ObservableCollection<PageNo> AllPlayerPages
        {
            get { return allPlayerPages; }
            set { allPlayerPages = value; OnPropertyChanged(); }
        }

        private PageNo selectedPageNo;

        public PageNo SelectedPageNo
        {
            get { return selectedPageNo; }
            set
            {
                selectedPageNo = value; OnPropertyChanged();
                var no = SelectedPageNo.No;
                if (result != null)
                    AllTeams = new ObservableCollection<Response>(result.Skip((no - 1) * 10).Take(10));
            }
        }


        private PageNo selectedPlayerPageNo;

        public PageNo SelectedPlayerPageNo
        {
            get { return selectedPlayerPageNo; }
            set
            {
                selectedPlayerPageNo = value; OnPropertyChanged();
                var no = selectedPlayerPageNo.No;
                if (playerResult!= null)
                    AllPlayers = new ObservableCollection<Player>(playerResult.Skip((no - 1) * 10).Take(10));
            }
        }

        List<Response> result = null;
        List<Player> playerResult = null;
        public HomeViewModel()
        {
            LoadData();
        }
        public RelayCommand SelectPageCommand { get; set; }
        public RelayCommand SelectPlayerPageCommand { get; set; }
        public RelayCommand SelectedTeamChangedCommand { get; set; }
        public RelayCommand SelectedPlayerChangedCommand { get; set; }
        public async void LoadData()
        {
            SelectedPageNo = new PageNo
            {
                No = 1
            };

            var service = new NbaApiService();

            result = await service.GetTeamsAsync();
            playerResult = await service.GetPlayersByTeamIdAsync(1);

            allPages = new ObservableCollection<PageNo>();
            var pageCount = decimal.Parse(result.Count.ToString()) / 10;
            int count = (int)Math.Ceiling(pageCount);

            for (int i = 0; i < count; i++)
            {
                allPages.Add(new PageNo
                {
                    No = i + 1
                });
            }


            allPlayerPages = new ObservableCollection<PageNo>();
            pageCount = decimal.Parse(playerResult.Count.ToString()) / 10;
            count = (int)Math.Ceiling(pageCount);

            for (int i = 0; i < count; i++)
            {
                AllPlayerPages.Add(new PageNo
                {
                    No = i + 1
                });
            }


            AllTeams = new ObservableCollection<Response>(result.Skip(0).Take(10));
            AllPlayers = new ObservableCollection<Player>(playerResult.Skip(0).Take(10));

            SelectPageCommand = new RelayCommand((o) =>
            {
                var no = SelectedPageNo.No;
                AllTeams = new ObservableCollection<Response>(result.Skip((no - 1) * 10).Take(10));
            });

            SelectPlayerPageCommand = new RelayCommand((o) =>
            {
                var no = selectedPlayerPageNo.No;
                AllPlayers = new ObservableCollection<Player>(playerResult.Skip((no - 1) * 10).Take(10));
            });

            SelectedTeamChangedCommand = new RelayCommand((o) =>
            {
                if(SelectedTeam != null)
                {
                    TeamInfoViewModel tm = new TeamInfoViewModel(SelectedTeam);
                    TeamInfoWindow tw = new TeamInfoWindow();
                    tw.DataContext = tm;
                    tw.Show();
                }
            });
            SelectedPlayerChangedCommand = new RelayCommand((o) =>
            {
                if (SelectedPlayer != null)
                {
                    PlayerInfoViewModel pm = new PlayerInfoViewModel(SelectedPlayer);
                    PlayerInfoWindow pw = new PlayerInfoWindow();
                    pw.DataContext = pm;
                    pw.Show();
                }
            });

        }




    }
}
