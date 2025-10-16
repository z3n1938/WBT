using System;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace WBT
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Event bağlantıları
            BtnWingetUpgradeAll.Click += BtnWingetUpgradeAll_Click;
            BtnWindowsUpdate.Click += BtnWindowsUpdate_Click;
            BtnTempClean.Click += BtnTempClean_Click;
            BtnFlushDns.Click += BtnFlushDns_Click;
            BtnBackgroundApps.Click += BtnBackgroundApps_Click;
            BtnSfcScan.Click += BtnSfcScan_Click;
            BtnDism.Click += BtnDism_Click;

            BtnCleanMgr.Click += BtnCleanMgr_Click;
            BtnEventViewer.Click += BtnEventViewer_Click;
            BtnTaskManager.Click += BtnTaskManager_Click;
            BtnResourceMonitor.Click += BtnResourceMonitor_Click;
            BtnSystemInfo.Click += BtnSystemInfo_Click;
            BtnDefrag.Click += BtnDefrag_Click;
            BtnRegedit.Click += BtnRegedit_Click;

            BtnCmd.Click += BtnCmd_Click;
            BtnPowerShell.Click += BtnPowerShell_Click;
            BtnPowerShellISE.Click += BtnPowerShellISE_Click;
        }

        // 🔹 YARDIMCI METOT
        private void RunCommand(string fileName, string arguments = "")
        {
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = fileName,
                    Arguments = arguments,
                    UseShellExecute = true,
                    Verb = "runas"
                };
                Process.Start(psi);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // 🔹 TEMİZLEME METODU
        private void CleanFolder(string folderPath, bool deleteDirectories = true)
        {
            if (!Directory.Exists(folderPath)) return;

            foreach (string file in Directory.GetFiles(folderPath))
            {
                try { File.Delete(file); } catch { }
            }

            if (deleteDirectories)
            {
                foreach (string dir in Directory.GetDirectories(folderPath))
                {
                    try { Directory.Delete(dir, true); } catch { }
                }
            }
        }

        // 🔹 BUTON OLAYLARI

        private void BtnWingetUpgradeAll_Click(object sender, RoutedEventArgs e)
        {
            RunCommand("cmd.exe", "/c winget upgrade --all");
        }

        private void BtnWindowsUpdate_Click(object sender, RoutedEventArgs e)
        {
            RunCommand("ms-settings:windowsupdate");
        }

        private void BtnTempClean_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string tempPath = Path.GetTempPath();
                CleanFolder(tempPath);

                string windowsTemp = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "Temp");
                CleanFolder(windowsTemp);

                string prefetchPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "Prefetch");
                CleanFolder(prefetchPath, deleteDirectories: false);

                MessageBox.Show("Geçici dosyalar başarıyla temizlendi!", "Başarılı", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnFlushDns_Click(object sender, RoutedEventArgs e)
        {
            RunCommand("cmd.exe", "/c ipconfig /flushdns");
        }

        private void BtnBackgroundApps_Click(object sender, RoutedEventArgs e)
        {
            RunCommand("ms-settings:privacy-backgroundapps");
        }

        private void BtnSfcScan_Click(object sender, RoutedEventArgs e)
        {
            RunCommand("cmd.exe", "/k sfc /scannow");
        }

        private void BtnDism_Click(object sender, RoutedEventArgs e)
        {
            RunCommand("cmd.exe", "/k Dism /Online /Cleanup-Image /StartComponentCleanup");
        }

        private void BtnCleanMgr_Click(object sender, RoutedEventArgs e)
        {
            RunCommand("cleanmgr.exe");
        }

        private void BtnEventViewer_Click(object sender, RoutedEventArgs e)
        {
            RunCommand("eventvwr.msc");
        }

        private void BtnTaskManager_Click(object sender, RoutedEventArgs e)
        {
            RunCommand("taskmgr.exe");
        }

        private void BtnResourceMonitor_Click(object sender, RoutedEventArgs e)
        {
            RunCommand("resmon.exe");
        }

        private void BtnSystemInfo_Click(object sender, RoutedEventArgs e)
        {
            RunCommand("msinfo32.exe");
        }

        private void BtnDefrag_Click(object sender, RoutedEventArgs e)
        {
            RunCommand("dfrgui.exe");
        }

        private void BtnRegedit_Click(object sender, RoutedEventArgs e)
        {
            RunCommand("regedit.exe");
        }

        private void BtnCmd_Click(object sender, RoutedEventArgs e)
        {
            RunCommand("cmd.exe");
        }

        private void BtnPowerShell_Click(object sender, RoutedEventArgs e)
        {
            RunCommand("powershell.exe");
        }

        private void BtnPowerShellISE_Click(object sender, RoutedEventArgs e)
        {
            RunCommand("powershell_ise.exe");
        }
    }
}
