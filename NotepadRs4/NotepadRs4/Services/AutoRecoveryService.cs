﻿using NotepadRs4.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace NotepadRs4.Services
{
    public class AutoRecoveryService
    {
        // Properties
        readonly StorageFolder tempFolder = ApplicationData.Current.TemporaryFolder;



        // Check for temp files





        // Save to Temp File
        /// <summary>
        /// Saves the data in an AutoRecovery File in case the app crashes
        /// </summary>
        /// <param name="data">TextDataModel containing all the info</param>
        /// <returns>Returns the StorageFile with the location for later reference</returns>
        public async Task<StorageFile> SaveAutoRecoveryFile(TextDataModel data)
        {
            StorageFile file = await tempFolder.CreateFileAsync("AutoRecoveryFile.txt", CreationCollisionOption.GenerateUniqueName);

            if (file != null)
            {
                bool success = await FileDataService.Save(data, file);
                Debug.WriteLine("FileDataService - SaveAutoRecoveryFile - Success = " + success);
                return file;
            }
            else
            {
                Debug.WriteLine("FileDataService - SaveAutoRecoveryFile - Something has gone horribly, horribly wrong");
                return null;
            }
        }


        // Load Temp file
        public async void LoadAutoRecoveryFiles()
        {
            // Get a list of AutoRecoveryFiles
            var autoRecoveryFiles = await tempFolder.GetFilesAsync();
            foreach (var item in autoRecoveryFiles)
            {
                var uri = new Uri("notepadarc:" + item.Path);
                var success = await Windows.System.Launcher.LaunchUriAsync(uri);
                // Put a pause here?
            }

            // #TODO: Do something if there are more than 10 items in the list to prevent bufferoverflows

        }
        
        


        // Set Crash Bool to true


        // Set Crash bool to false


        // Clear current temp file
        public async void DeleteAutoRecoveryFile(StorageFile file)
        {
            await file.DeleteAsync();
        }

        // Clear the Temporary Folder
        public async void ClearTempFolder()
        {
            await tempFolder.DeleteAsync();
           
        }
    }
}
