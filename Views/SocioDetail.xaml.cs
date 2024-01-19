using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using club_papaya.Models;
using club_papaya.Repository;
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
/// Display Socio details data 
/// </summary>
public sealed partial class SocioDetail : Page
{
    public Socio Socio { get; set; }
    private SocioDAO SocioDAO { get; set; }

    public SocioDetail()
    {
        InitializeComponent();
        GoBackButton.Loaded += GoBackButton_Loaded;
        SocioDAO = SocioDAO.Instance;
    

    }

    private void GoBackButton_Loaded(object sender, RoutedEventArgs e)
    {
        // When we land in page, put focus on the back button
        GoBackButton.Focus(FocusState.Programmatic);
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);

        // Store the item to be used in binding to UI
        Socio = e.Parameter as Socio;

        if (Socio.Barcos.LongCount() > 0)
        {
            lstBarcos.ItemsSource = Socio.Barcos;
        }
        else
        {
            tituloBarco.Text = "No barcos registrados";
            tituloBarco.Style = (Style)Application.Current.Resources["BodyStrongTextBlockStyle"];
        }


        ConnectedAnimation imageAnimation = ConnectedAnimationService.GetForCurrentView().GetAnimation("ForwardConnectedAnimation");
        if (imageAnimation != null)
        {
            // Connected animation + coordinated animation
            imageAnimation.TryStart(detailedImage, new UIElement[] { coordinatedPanel });

        }
    }


    private void BackButton_Click(object sender, RoutedEventArgs e)
    {
        Frame.GoBack();
    }

    private void btn_EliminarSocio(object sender, RoutedEventArgs e)
    {
        //Eliminar socio
        SocioDAO.EliminarSocio(Socio);
        //volver al listado de socios
        Frame.GoBack();
    }

    private void btn_EditarSocio(object sender, RoutedEventArgs e)
    {
        Frame.Navigate(typeof(SocioForm), Socio, new SuppressNavigationTransitionInfo());
    }


}
