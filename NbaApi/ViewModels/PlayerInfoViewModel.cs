using NbaApi.Models;
using NbaApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NbaApi.ViewModels
{
    public class PlayerInfoViewModel : BaseViewModel
    {
        private string logo;

        public string Logo
        {
            get { return logo; }
            set { logo = value; OnPropertyChanged(); }
        }


        private Player player;

        public PlayerInfoViewModel(Player player)
        {
            Player = player;
            LoadLogo();
        }

        public async void LoadLogo()
        {
            Logo = await ImageSearchService.GetImageUrl(player.firstname + player.lastname);
        }


        public Player Player
        {
            get { return player; }
            set { player = value; OnPropertyChanged(); }
        }


    }
}
