using NbaApi.ApiEntities.Teams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NbaApi.ViewModels
{
    public class TeamInfoViewModel:BaseViewModel
    {
        private Response team;

        public TeamInfoViewModel(Response team)
        {
            Team = team;
        }

        public Response Team
        {
            get { return team; }
            set { team = value; OnPropertyChanged(); }
        }

    }
}
