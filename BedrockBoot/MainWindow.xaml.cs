using BedrockBoot.Pages;
using DevWinUI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BedrockBoot
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ExtendsContentIntoTitleBar = true; // �ñ�������չ����������
            AppTitleBar.IsBackButtonVisible = false; // ����ʾ���ذ�ť
            SetTitleBar(AppTitleBar); // �����Զ��������
            this.Closed += MainWindow_Closed;
            /* ��Ҫ���ô˴��룬��������ʹ�� DevWinUI-JSON �ĵ������񣬵���ʵ���Ҹ���ûд���������:)
            App.Current.NavService.Initialize(NavView, NavFrame, NavigationPageMappings.PageDictionary)
                                  .ConfigureDefaultPage(typeof(HomePage));
            App.Current.NavService.ConfigureSettingsPage(typeof(SettingsPage));
            App.Current.NavService.ConfigureJsonFile("Assets/NavViewMenu/AppData.json")
                                  .ConfigureTitleBar(AppTitleBar)
                                  .ConfigureBreadcrumbBar(BreadCrumbNav, BreadcrumbPageMappings.PageDictionary);
            byd ûд�û������� �����������ԣ��ҿ������Զ೤ ??????
            ������ʮ��
            */
        }
        private void MainWindow_Closed(object sender, WindowEventArgs args)
        {
            Environment.Exit(0);
        }
        private void NavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            /*if (NavFrame.CanGoBack)
            {
                BackButton.Visibility = Visibility.Visible;
            }
            else
            {
                BackButton.Visibility = Visibility.Collapsed;
            }*/
            // DM: Խ�ϣ�Խ����
            if (args.IsSettingsSelected) NavFrame.Navigate(typeof(SettingsPage));
            var selectedItem = (NavigationViewItem)args.SelectedItem;
            if ((string)selectedItem.Tag == "SettingPage") NavFrame.Navigate(typeof(SettingsPage));
            if ((string)selectedItem.Tag == "DownloadPage") NavFrame.Navigate(typeof(DownloadPage));
            if ((string)selectedItem.Tag == "HomePage") NavFrame.Navigate(typeof(HomePage));
            if ((string)selectedItem.Tag == "OOBE") NavFrame.Navigate(typeof(OOBEPage));
            if ((string)selectedItem.Tag == "TaskPage") NavFrame.Navigate(typeof(TaskPage));
            if ((string)selectedItem.Tag == "VersionPage") NavFrame.Navigate(typeof(VersionPage));
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (NavFrame.CanGoBack)
            {
                NavFrame.GoBack();
            }
        }
    }
}
