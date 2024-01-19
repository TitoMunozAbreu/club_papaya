using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using club_papaya.Exceptions;
using club_papaya.Models;
using Microsoft.WindowsAppSDK.Runtime.Packages;
using Windows.Networking;

namespace club_papaya.Repository;
public class SocioDAO

{
    private static SocioDAO instance;
    private List<Socio> Socios;

    public SocioDAO()
    {
        Socios = new List<Socio>
        {
            new Socio
            (
                "25636584A",
                "Livvie",
                "Basler",
               "641256987",
                "lbasler0@club-nautico.com",
               "https://randomuser.me/api/portraits/women/75.jpg"
            ),
            new Socio
            (
                 "X1285479D",
                 "Nathanael",
                "Aers",
                "635789452",
                "iaers0@club-nautico.com",
                "https://randomuser.me/api/portraits/men/20.jpg"
            ),                  
            new Socio
            (
                 "Y7643930D",
                 "Silvester",
                "Tribe",
                "965874523",
                "asybbe2@club-nautico.com",
                "https://randomuser.me/api/portraits/men/15.jpg"
            ),                       
            new Socio
            (
                 "52852369A",
                 "Anitra",
                "Geary",
                "623536987",
                "ageary5@club-nautico.com",
                "https://randomuser.me/api/portraits/women/5.jpg"
            ),
            new Socio
            (
                 "35789526A",
                 "Levis",
                "Strauss",
                "956325968",
                "levis@club-nautico.com",
                "https://randomuser.me/api/portraits/men/5.jpg"
            )
        };
        foreach (Socio s in Socios)
        {
            if (s.Dni.Equals("Y7643930D"))
            {
                s.AgregarBarco(new Barco(
                    "Papayita",
                    "1",
                    "250.50"
                ));
                s.AgregarBarco(new Barco(
                   "Papayita II",
                   "2",
                   "400"
               ));
            }

            if (s.Dni.Equals("25636584A"))
            {
                s.AgregarBarco(new Barco(
                    "Mango",
                    "3",
                    "380"
                ));               
            }
        }

    }
    public static SocioDAO Instance
    {
        get
        {
            instance ??= new SocioDAO();
            return instance;
        }
    }

    public void IncluirSocio(Socio socio)
    {
        // comprobar si existe el socio segun Dni
        if (Socios.Any(s => s.Dni == socio.Dni))
        {
            throw new DuplicateResourceException("Socio con Dni: " + socio.Dni  + " se encuentra registrado.");
        }
        // agregar un socio a la lista
        Socios.Add(socio);
    }

    public List<Socio> ObtenerSocios()
    {
         // retorna la lista completa de socios
        return Socios;
    }

    public void ActualizarSocio(Socio socioActualizado)
    {
        // Buscar el índice del socio en la lista por su DNI
        int indice = Socios.FindIndex(s => s.Dni == socioActualizado.Dni);

        if (indice != -1)
        {
            // Actualizar los datos del socio existente
            Socios[indice].FirstName = socioActualizado.FirstName;
            Socios[indice].LastName = socioActualizado.LastName;
            Socios[indice].Email = socioActualizado.Email;
            Socios[indice].Mobile = socioActualizado.Mobile;
        }
    }

    public void EliminarSocio(Socio socio)
    {
        Socios.Remove(socio);
        
    }

    public bool ExisteDni(string Dni)
    {
        bool existeDni = false;
        // comprobar si existe el socio segun Dni
        if (Socios.Any(s => s.Dni == Dni))
        {
            return existeDni = true;
        }
        return existeDni;
    }

    public void RegistrarBarco(Socio socio, Barco barco)
    {
        Socio socioExistente = Socios.FirstOrDefault(s => s.Dni.Equals(socio.Dni));

        if (socioExistente != null)
        {
            socioExistente.AgregarBarco(barco);
        }
    }
}
