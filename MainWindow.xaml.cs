using club_papaya.Views;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using Windows.ApplicationModel.Core;
using Windows.UI.ViewManagement;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace club_papaya;
/// <summary>
/// An empty window that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class MainWindow : Window
{
    public MainWindow()
    {
        this.InitializeComponent();
    }

    private void OnNavigationItemSelected(NavigationView sender, NavigationViewItemInvokedEventArgs args)
    {
        // Obtener el tag del ítem seleccionado
        string selectedPageTag = args.InvokedItemContainer?.Tag?.ToString();

        // Seleccionar la página correspondiente según el tag
        switch (selectedPageTag)
        {
            case "listadoSocio":
                ContentFrame.Navigate(typeof(SociosView),null, new DrillInNavigationTransitionInfo());
                break; 
            case "registrarSocio":
                ContentFrame.Navigate(typeof(SocioForm), null, new DrillInNavigationTransitionInfo());
                break;         
            case "registrarBarco":
                ContentFrame.Navigate(typeof(BarcoForm), null, new DrillInNavigationTransitionInfo());
                break;
        }
    }

}
