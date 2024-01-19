using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace club_papaya.Models
{
    public class Barco
    {
        public string Name { get; set; }
        public string MooringNumber { get; set; }
        public string PaymentFee { get; set; }

        public Barco(string name, string mooringNumber, string paymentFee)
        {
            Name = name;
            MooringNumber = mooringNumber;
            PaymentFee = paymentFee;
        }

    }
}
