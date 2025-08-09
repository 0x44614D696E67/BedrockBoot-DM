using BedrockBoot.Versions;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using Windows.Storage;
using Windows.System;

namespace BedrockBoot.Pages
{
    public sealed partial class ModManagerPage : Page
    {
        private NowVersions _selectedVersion;
        public  ModManager Manager = ModManager.Instance;

        public string mods_dir => Path.Combine(_selectedVersion.Version_Path, "mods");
        // ��ӹ��캯�����հ汾����
        public ModManagerPage(NowVersions version)
        {
            this.InitializeComponent();
            _selectedVersion = version;
            BreadcrumbBar.Text=$"Mods����{_selectedVersion.DisPlayName}";
            if (!Directory.Exists(mods_dir))
            {
                Directory.CreateDirectory(mods_dir);
            }

           LoadMods();
        }

        public void LoadMods()
        {
            Manager.ModsList.Clear();
            TaskContainer.Children.Clear();
            var dllFileInfos = globalTools.GetDllFiles(mods_dir);

            Button newButton(DllFileInfo info, SettingsExpander data)
            {
                var button = new Button();
                button.Content = "ɾ��mod";
                button.Click += (s, e) =>
                {
                    var removeMod = Manager.RemoveMod(info);
                    if (removeMod != true)
                    {
                        globalTools.ShowInfo("ɾ��ʧ��");
                    }
                    else
                    {
                        globalTools.ShowInfo("ɾ���ɹ�");
                        TaskContainer.Children.Remove(data);
                    }
                };
                return button;
            }
            foreach (var dllFileInfo in dllFileInfos)
            {
                var settingsExpander = new SettingsExpander()
                {
                    Margin = new Thickness(20),
                    Description = dllFileInfo.FullPath,
                    Header = dllFileInfo.FileName,
                    IsExpanded = false,
                    HeaderIcon = new FontIcon() { Glyph = "&#xEA37;" },
                };
                settingsExpander.Items = new List<object>()
                {
                    new SettingsCard()
                    {
                        Header = "ɾ��mod",
                        Content = newButton(dllFileInfo, settingsExpander)
                    }
                };
                TaskContainer.Children.Add(settingsExpander);
                Manager.ModsList.Add(dllFileInfo);
            }
        }
        // �޲ι��캯���������Ҫ��
        public ModManagerPage()
        {
            this.InitializeComponent();
        }

        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var storageFolder = await  StorageFolder.GetFolderFromPathAsync(mods_dir); 
           await Launcher.LaunchFolderAsync(storageFolder);
        }

        private void ButtonBaseLeft_OnClick(object sender, RoutedEventArgs e)
        {
           LoadMods();
        }
    }
}