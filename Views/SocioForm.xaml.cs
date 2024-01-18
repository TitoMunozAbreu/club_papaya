using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using club_papaya.Models;
using club_papaya.Repository;
using CommunityToolkit.Common;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace club_papaya.Views;
/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class SocioForm : Page
{
    private SocioDAO SocioDAO { get; set; }
    private bool IsEditSocio { get; set; }
    private bool IsFirstNameValid { get; set; }
    private bool IsLastNameValid { get; set; }
    private bool IsEmailValid { get; set; }
    private bool IsMobileValid { get; set; }
    private bool IsDniValid { get; set; }

public SocioForm()
    {
        this.InitializeComponent();
        SocioDAO = SocioDAO.Instance;
        IsFirstNameValid = false;
        IsLastNameValid = false;
        IsEmailValid = false;
        IsMobileValid = false;   
        IsDniValid = false;
        IsEditSocio = false;
        GoBackButton.Visibility = Visibility.Collapsed; 
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);
        if (e.Parameter is Socio socio) 
        {
            CargarDatosForm(socio);
        }
    }

    private void CargarDatosForm(Socio socio)
    {
        IsEditSocio = true;
        //desabilitar edicion del Dni
        txtDni.IsEnabled = false;
        IsDniValid = true;
        //Actualizar formulario a tipo edicion
        titulo.Text = "Actualizar Socio";
        btnGuardar.Content = "Actualizar";
        btnGuardar.Background = new SolidColorBrush(Colors.Blue);
        //cargas datos del socio
        txtDni.Text = socio.Dni;
        txtFirstName.Text = socio.FirstName;
        txtLastName.Text = socio.LastName;
        txtMobile.Text = socio.Mobile;
        txtEmail.Text = socio.Email;
        GoBackButton.Visibility = Visibility.Visible;
    }

    private void btn_RegistrarSocio(object sender, RoutedEventArgs e)
    {
        Random random = new Random();
        int numeroRandom = random.Next(1, 100);

        Socio socio = new Socio(
                txtDni.Text,
                txtFirstName.Text,
                txtLastName.Text,
                txtMobile.Text,
                txtEmail.Text,
                "https://randomuser.me/api/portraits/women/" + numeroRandom + ".jpg"
            );
        if (IsEditSocio)
        {
            //actualizar socio en SocioDAO
            SocioDAO.ActualizarSocio(socio);
            Frame.Navigate(typeof(SociosView),null, new SuppressNavigationTransitionInfo());

        }
        else
        {


            //registrar socio en SocioDAO
            SocioDAO.IncluirSocio(socio);
            // msj de exito
            alert.IsOpen = true;
            alert.Title = "Enhorabuena!";
            alert.Severity = InfoBarSeverity.Success;
            alert.Message = "Socio " + socio.FirstName + " registrado con exito!";
        }
       
        // Limpiar campos después de guardar
        LimpiarCampos();
        // Resetear el estado de las validaciones
        ResetearValidaciones();


    }

    private void GfDni(object sender, RoutedEventArgs e)
    {
        ValidatorDni();
    }
    private void TcDni(object sender, TextChangedEventArgs e)
    {
        ValidatorDni();
    }

    private void GfFirstname(object sender, RoutedEventArgs e)
    {
        ValidatorFirstName();
    }
    private void TcFirstname(object sender, TextChangedEventArgs e)
    {
        ValidatorFirstName();
    }

    private void GfLastName(object sender, RoutedEventArgs e)
    {
        ValidatorLastName();
    }
    private void TcLastName(object sender, TextChangedEventArgs e)
    {
        ValidatorLastName();
    }

    private void GfEmail(object sender, RoutedEventArgs e)
    {
        ValidatorEmail();
    }
    private void TcEmail(object sender, TextChangedEventArgs e)
    {
        ValidatorEmail();
    }

    private void GfMobile(object sender, RoutedEventArgs e)
    {
        ValidatorMobile();
    }
    private void TcMobile(object sender, TextChangedEventArgs e)
    {
        ValidatorMobile();
    }

    private void ValidatorFirstName()
    {
        var FirstName = txtFirstName.Text;

        if (string.IsNullOrEmpty(FirstName) || string.IsNullOrWhiteSpace(FirstName))
        {
            errorFirstName.Visibility = Visibility.Visible;
            errorFirstName.Text = "Ingresar un nombre";
            IsFirstNameValid = false;
        } 
        else if (FirstName.IsNumeric())
        {
            errorFirstName.Visibility = Visibility.Visible;
            errorFirstName.Text = "Ingresar un nombre valido";
            IsFirstNameValid = false;
        }
        else
        {
            errorFirstName.Visibility = Visibility.Collapsed;
            IsFirstNameValid = true;
            IsNewSocioDataValid();
        }
    }

    private void ValidatorLastName()
    {
        var LastName = txtLastName.Text;

        if (string.IsNullOrEmpty(LastName) || string.IsNullOrWhiteSpace(LastName))
        {
            errorLastName.Visibility = Visibility.Visible;
            errorLastName.Text = "Ingresar un apellido";
            IsLastNameValid = false;
        }
        else if (LastName.IsNumeric())
        {
            errorLastName.Visibility = Visibility.Visible;
            errorLastName.Text = "Ingresar un apellido valido";
            IsLastNameValid = false;
        }
        else
        {
            errorLastName.Visibility = Visibility.Collapsed;
            IsLastNameValid = true;
            IsNewSocioDataValid();
        }
    }

    private void ValidatorDni()
    {
        var Dni = txtDni.Text;
        var DniRegex = "^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]{8,9}[A-Za-z]$";

        if (string.IsNullOrEmpty(Dni) || string.IsNullOrWhiteSpace(Dni))
        {
            errorDni.Visibility = Visibility.Visible;
            errorDni.Text = "Ingresar un Dni / Nie";
            IsDniValid = false;
        }
        else if (!Regex.IsMatch(Dni, DniRegex))
        {
            errorDni.Visibility = Visibility.Visible;
            errorDni.Text = "Dni/Nie valido, ejemplo: 12345678A | X1234567D";
            IsDniValid = false;
        }
        else if (SocioDAO.ExisteDni(Dni))
        {
            errorDni.Visibility = Visibility.Visible;
            errorDni.Text = "Dni/Nie ya se encuentra registrado";
            IsDniValid = false;
        }
        else
        {
            errorDni.Visibility = Visibility.Collapsed;
            IsDniValid = true;
            IsNewSocioDataValid();
        }
        //TODO: comprobar si el dni se encuentra registrado en los socios
    }

    private void ValidatorEmail()
    {
        var Email = txtEmail.Text;
        var EmailRegex = ".*@club-nautico\\.com";

        if (string.IsNullOrEmpty(Email) || string.IsNullOrWhiteSpace(Email))
        {
            errorEmail.Visibility = Visibility.Visible;
            errorEmail.Text = "Ingresar un email";
            IsEmailValid = false;
        }
        else if (!Email.IsEmail())
        {
            errorEmail.Visibility = Visibility.Visible;
            errorEmail.Text = "Ingresar un email valido";
            IsEmailValid = false;
        }
        else if (!Regex.IsMatch(Email, EmailRegex))
        {
            errorEmail.Visibility = Visibility.Visible;
            errorEmail.Text = "Ingresar dominio: ejemplo@club-nautico.com ";
            IsEmailValid = false;
        }
        else
        {
            errorEmail.Visibility = Visibility.Collapsed;
            IsEmailValid = true;
            IsNewSocioDataValid();
        }
    }

    private void ValidatorMobile()
    {
        var Mobile = txtMobile.Text;

        if (string.IsNullOrEmpty(Mobile) || string.IsNullOrWhiteSpace(Mobile))
        {
            errorMobile.Visibility = Visibility.Visible;
            errorMobile.Text = "Ingresar un móvil";
            IsMobileValid = false;
        }
        else if (!Mobile.IsNumeric())
        {
            errorMobile.Visibility = Visibility.Visible;
            errorMobile.Text = "Ingresar un móvil valido";
            IsMobileValid = false;
        }
        else if (Mobile.Length != 9)
        {
            errorMobile.Visibility = Visibility.Visible;
            errorMobile.Text = "Ingresar móvil de 9 digitos";
            IsMobileValid = false;
        }
        else
        {
            errorMobile.Visibility = Visibility.Collapsed;
            IsMobileValid = true;
            IsNewSocioDataValid();
        }
    }

    private void IsNewSocioDataValid()
    {   
        //comprobar que todos los campos sean validos
        if (IsFirstNameValid && IsLastNameValid && IsDniValid && IsEmailValid && IsMobileValid)
        {   
            //habilitar boton
            btnGuardar.IsEnabled = true;
            alert.IsOpen = false;
        }
        else 
        {
            //desabilitar boton
            btnGuardar.IsEnabled = false;
            // msj de error
            alert.IsOpen = true;
            alert.Title = "Error";
            alert.Severity = InfoBarSeverity.Error;
            alert.Message = "Datos del socio invalidos";
        }
    }

    private void LimpiarCampos()
    {
        txtDni.Text = string.Empty;
        txtFirstName.Text = string.Empty;
        txtLastName.Text = string.Empty;
        txtMobile.Text = string.Empty;
        txtEmail.Text = string.Empty;
    }

    private void ResetearValidaciones()
    {
        // Restablecer la visibilidad de los mensajes de error
        errorDni.Visibility = Visibility.Collapsed;
        errorFirstName.Visibility = Visibility.Collapsed;
        errorLastName.Visibility = Visibility.Collapsed;
        errorMobile.Visibility = Visibility.Collapsed;
        errorEmail.Visibility = Visibility.Collapsed;
        // Resetear las variables de validación
        IsFirstNameValid = false;
        IsLastNameValid = false;
        IsEmailValid = false;
        IsMobileValid = false;
        IsDniValid = false;
    }

    private void BackButton_Click(object sender, RoutedEventArgs e)
    {
        Frame.GoBack();
    }


}
