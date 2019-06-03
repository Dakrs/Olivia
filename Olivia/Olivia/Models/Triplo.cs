using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Threading;

namespace Olivia.Models
{
    public class Triplo
    {
        public Triplo() { }
        public Triplo(int created, int rated, int points)
        {
            this.Created = created;
            this.Rated = rated;
            this.Points = points;
        }

        public int Created { get; set; }
        public int Rated { get; set; }
        public int Points { get; set; }

    }

   
}
