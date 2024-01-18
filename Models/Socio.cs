using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace club_papaya.Models;
public class Socio
{
    public string Dni { get; set; }
    public string FirstName { get; set; }
    public string LastName{ get; set; }
    public string Mobile   { get; set; }
    public string Email { get; set; }
    public string PhotoUrl { get; set; } 


    public Socio(
        string dni, 
        string firstName,  
        string lastName,
        string mobile, 
        string email, 
        string photoUrl)
    {
        Dni = dni;
        FirstName = firstName;
        LastName = lastName;
        Mobile = mobile;
        Email = email;
        PhotoUrl = photoUrl;
    }
}
