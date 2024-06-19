using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using TheIslandPostManagerWPF.Services;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace TheIslandPostManagerWPF.ViewModels;
public partial class BackupPageViewModel : ObservableObject
{
    private readonly IFileServices fileServices;
    private readonly ISnackbarService snackbarService;
    [ObservableProperty] private ObservableCollection<Folder> folders;
    [ObservableProperty] private int selectedIndex;
    [ObservableProperty] private int totalFiles;
    [ObservableProperty] private int currentDone;
    [ObservableProperty] private List<string> log;
    [ObservableProperty] private int progress;

    public BackupPageViewModel(IFileServices fileServices, ISnackbarService snackbarService)
    {
        this.fileServices = fileServices;
        this.snackbarService = snackbarService;
        GetDrives();
    }

    [RelayCommand]
    private void GetDrives()
    {
        try
        {
            Folders = new ObservableCollection<Folder>();

            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in drives)
            {
                if (drive.IsReady)
                {
                    //We only want drives with folders, "Fixed" is hard drives
                    if (drive.DriveFormat.Equals("exFat",StringComparison.OrdinalIgnoreCase) || drive.DriveType == DriveType.Removable )
                    {
                        Folder newFolder = new Folder();
                        newFolder.FullPath = drive.Name;
                        newFolder.Name = string.IsNullOrWhiteSpace(drive.VolumeLabel) ? drive.Name : drive.VolumeLabel;
                        Folders.Add(newFolder);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            snackbarService.Show(
                            "Error",
                            ex.Message,
                            ControlAppearance.Danger,
                            new SymbolIcon(SymbolRegular.ThumbDislike24),
                            TimeSpan.FromSeconds(5));
        }
    }

    [RelayCommand]
    private async Task Backup()
    {
        try
        {
            Log = new();

            TotalFiles = 0;
            CurrentDone = 0;

            if (SelectedIndex > -1)
            {
                var allJpeg = GetFilesRec(Folders.ElementAt(SelectedIndex).FullPath, "*.JPG");
                var allArw =  GetFilesRec(Folders.ElementAt(SelectedIndex).FullPath, "*.ARW");
                var allRaf =  GetFilesRec(Folders.ElementAt(SelectedIndex).FullPath, "*.RAF");
                var allCR2 = GetFilesRec(Folders.ElementAt(SelectedIndex).FullPath, "*.CR2");

                TotalFiles = allJpeg.Count + allArw.Count + allRaf.Count + allCR2.Count;


                await fileServices.CreateBackup(allJpeg,false,(prog, fileName) =>
                {
                    Progress = prog;
                    Log.Add($"Transfered file name: {fileName}");
                    CurrentDone++;
                });
                await fileServices.CreateBackup(allArw, false, (prog, fileName) =>
                {
                    Progress = prog;
                    CurrentDone++;
                    Log.Add($"Transfered file name: {fileName}");
                });
                await fileServices.CreateBackup(allRaf, false, (prog, fileName) =>
                {
                    Progress = prog;
                    Log.Add($"Transfered file name: {fileName}");
                    CurrentDone++;
                });
                await fileServices.CreateBackup(allCR2, false, (prog, fileName) =>
                {
                    Progress = prog;
                    Log.Add($"Transfered file name: {fileName}");
                    CurrentDone++;
                });
            }
        }
        catch (Exception ex)
        {
            snackbarService.Show(
                            "Error",
                            ex.Message,
                            ControlAppearance.Danger,
                            new SymbolIcon(SymbolRegular.ThumbDislike24),
                            TimeSpan.FromSeconds(5));
        }

    }

    private List<string> GetFilesRec(string drive,string ext)
    {
        string[] directories = Directory.GetDirectories(drive, "DCIM", SearchOption.TopDirectoryOnly);

        foreach (string directory in directories)
        {
            return Directory.GetFiles(directory, ext, SearchOption.AllDirectories).ToList();
        }

        return new List<string>();
    }
}
