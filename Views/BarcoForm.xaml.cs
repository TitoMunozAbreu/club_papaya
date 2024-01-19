using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using club_papaya.Repository;
using club_papaya.Models;
using CommunityToolkit.Common;
using Microsoft.UI.Xaml.Media.Animation;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace club_papaya.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BarcoForm : Page
    {
        private SocioDAO SocioDAO { get; set; }
        public List<Socio> Socios { get; set; }
        public Socio Socio { get; set; }
        public bool IsNameValid { get;set; }
        public bool IsMNumberValid { get;set; }
        public bool IsPaymentFeeValid { get;set; }


        public BarcoForm()
        {
            this.InitializeComponent();
            //Obtener instancia del SocioDAO
            SocioDAO = SocioDAO.Instance;
            //listado de socio
            Socios = SocioDAO.ObtenerSocios();
            //asignar Socios al ComboBox
            cmbSocios.ItemsSource = Socios;
            //asignar validaciones
            IsNameValid = false;
            IsMNumberValid = false;
            IsPaymentFeeValid = false;  
        }

        private void btn_RegistrarBarco(object sender, RoutedEventArgs e)
        {
            Barco barco = new Barco(
                    txtName.Text,
                    txtMooringNumber.Text,
                    txtPaymentFee.Text
                );

            //registrar barco en el socio
            SocioDAO.RegistrarBarco(Socio, barco);
            // msj de exito
            alert.IsOpen = true;
            alert.Title = "Enhorabuena!";
            alert.Severity = InfoBarSeverity.Success;
            alert.Message = "Barco " + barco.Name + " registrado con exito!";      
            Frame.Navigate(typeof(SocioDetail), Socio, new SuppressNavigationTransitionInfo());

        }

        private void GfName(object sender, RoutedEventArgs e)
        {
            ValidatorName();
        }
        private void TcName(object sender, TextChangedEventArgs e)
        {
            ValidatorName();
        }
   
        private void GfMooringNumber(object sender, RoutedEventArgs e)
        {
            ValidatorMNumber();
        }
        private void TcMooringNumber(object sender, TextChangedEventArgs e)
        {
            ValidatorMNumber();
        }

        private void GfPaymentFee(object sender, RoutedEventArgs e)
        {
            ValidatorPaymentFee();
        }
        private void TcPaymentFee(object sender, TextChangedEventArgs e)
        {
            ValidatorPaymentFee();
        }


        private void ValidatorName()
        {
            var Name = txtName.Text;

            if (string.IsNullOrEmpty(Name) || string.IsNullOrWhiteSpace(Name))
            {
                errorName.Visibility = Visibility.Visible;
                errorName.Text = "Ingresar un nombre";
                IsNameValid = false;
            }
            else if (Name.IsNumeric())
            {
                errorName.Visibility = Visibility.Visible;
                errorName.Text = "Ingresar un nombre valido";
                IsNameValid = false;
            }
            else
            {
                errorName.Visibility = Visibility.Collapsed;
                IsNameValid = true;
                IsNewBarcoDataValid();
            }
        }

        private void ValidatorMNumber()
        {
            var MNumber = txtMooringNumber.Text;

            if (string.IsNullOrEmpty(MNumber) || string.IsNullOrWhiteSpace(MNumber))
            {
                errorMooringNumber.Visibility = Visibility.Visible;
                errorMooringNumber.Text = "Ingresar un numero amarre";
                IsMNumberValid = false;
            }
            else if (!MNumber.IsNumeric())
            {
                errorMooringNumber.Visibility = Visibility.Visible;
                errorMooringNumber.Text = "Ingresar un numero amarre valido";
                IsMNumberValid = false;
            }
            else
            {
                errorMooringNumber.Visibility = Visibility.Collapsed;
                IsMNumberValid = true;
                IsNewBarcoDataValid();
            }
        }

        private void ValidatorPaymentFee()
        {
            var PaymentFee = txtPaymentFee.Text;

            if (string.IsNullOrEmpty(PaymentFee) || string.IsNullOrWhiteSpace(PaymentFee))
            {
                errorPaymentFee.Visibility = Visibility.Visible;
                errorPaymentFee.Text = "Ingresar un numero amarre";
                IsPaymentFeeValid = false;
            }
            else if (!PaymentFee.IsDecimal())
            {
                errorPaymentFee.Visibility = Visibility.Visible;
                errorPaymentFee.Text = "Ingresar un numero amarre valido";
                IsPaymentFeeValid = false;
            }
            else
            {
                errorPaymentFee.Visibility = Visibility.Collapsed;
                IsPaymentFeeValid = true;
                IsNewBarcoDataValid();
            }
        }

        private void CmbSocios_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Socio = cmbSocios.SelectedItem as Socio;
        }

        private void IsNewBarcoDataValid()
        {
            //comprobar que todos los campos sean validos
            if (IsNameValid && IsMNumberValid && IsPaymentFeeValid)
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
                alert.Message = "Datos del barco invalidos";
            }
        }


    }
}
