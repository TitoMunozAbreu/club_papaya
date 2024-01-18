using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Windows.Foundation.Metadata;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace club_papaya.Views;
/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class SociosView : Page
{
    public ObservableCollection<Socio> Socios { get; set; }
    private SocioDAO SocioDAO { get; set; }

    public SociosView()
    {
        this.InitializeComponent();
        SocioDAO = SocioDAO.Instance;
        Socios = new ObservableCollection<Socio>(SocioDAO.ObtenerSocios());
        collection.ItemsSource = Socios;
    }

    private async void collection_Loaded(object sender, RoutedEventArgs e)
    {
        if (Socios != null)
        {
            // If the connected item appears outside the viewport, scroll it into view.
            collection.ScrollIntoView(Socios, ScrollIntoViewAlignment.Default);
            collection.UpdateLayout();

            // Play the second connected animation.
            ConnectedAnimation animation = ConnectedAnimationService.GetForCurrentView().GetAnimation("BackConnectedAnimation");
            if (animation != null)
            {
                // Setup the "back" configuration if the API is present.
                if (ApiInformation.IsApiContractPresent("Windows.Foundation.UniversalApiContract", 7))
                {
                    animation.Configuration = new DirectConnectedAnimationConfiguration();
                }

                await collection.TryStartConnectedAnimationAsync(animation, Socios, "connectedElement");
            }

            // Set focus on the list
            collection.Focus(FocusState.Programmatic);
        }
    }

    private void collection_ItemClick(object sender, ItemClickEventArgs e)
    {
        Socio socioSeleccionado = null;
        // Get the collection item corresponding to the clicked item.
        if (collection.ContainerFromItem(e.ClickedItem) is ListViewItem container)
        {
            // Stash the clicked item for use later. We'll need it when we connect back from the detailpage.
            socioSeleccionado = container.Content as Socio;

            // Prepare the connected animation.
            // Notice that the stored item is passed in, as well as the name of the connected element.
            // The animation will actually start on the Detailed info page.
            var animation = collection.PrepareConnectedAnimation("ForwardConnectedAnimation", socioSeleccionado, "connectedElement");

        }

        // Navigate to the DetailedInfoPage.
        // Note that we suppress the default animation.
        Frame.Navigate(typeof(SocioDetail), socioSeleccionado, new SuppressNavigationTransitionInfo());
    }

}
