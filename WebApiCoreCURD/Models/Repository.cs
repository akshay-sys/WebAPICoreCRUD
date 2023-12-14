using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiCoreCURD.Models
{
    public class Repository : IRepository
    {
        private InventDbContext dbcontext;
        public Repository(InventDbContext dbcontext)
        {

            this.dbcontext = dbcontext;
        }

        public Reservation this[int id] => dbcontext.Reservations.Find(id);

        public IEnumerable<Reservation> Reservations => dbcontext.Reservations.ToList();

        public Reservation AddReservation(Reservation reservation)
        {
            dbcontext.Reservations.Add(reservation);
            dbcontext.SaveChanges();
            return reservation;
        }
    }
}
