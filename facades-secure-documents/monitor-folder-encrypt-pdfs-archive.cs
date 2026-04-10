using System;
using System.IO;
using System.Threading;
using Aspose.Pdf.Facades;          // PdfFileSecurity, KeySize, Algorithm
using Aspose.Pdf;                 // DocumentPrivilege

namespace PdfArchiveService
{
    // Service that watches a folder, encrypts new PDFs and moves them to an archive folder.
    public class DirectoryMonitorService : IDisposable
    {
        private readonly string _watchPath;
        private readonly string _archivePath;
        private readonly FileSystemWatcher _watcher;
        private readonly string _userPassword = "user123";   // example user password
        private readonly string _ownerPassword = "owner123"; // example owner password

        public DirectoryMonitorService(string watchPath, string archivePath)
        {
            _watchPath = watchPath;
            _archivePath = archivePath;

            // Ensure the archive directory exists.
            Directory.CreateDirectory(_archivePath);

            // Set up the file system watcher.
            _watcher = new FileSystemWatcher(_watchPath, "*.pdf")
            {
                NotifyFilter = NotifyFilters.FileName | NotifyFilters.CreationTime,
                EnableRaisingEvents = false,
                IncludeSubdirectories = false
            };
            _watcher.Created += OnCreated;
        }

        // Start monitoring.
        public void Start()
        {
            _watcher.EnableRaisingEvents = true;
            Console.WriteLine($"Monitoring folder: {_watchPath}");
        }

        // Stop monitoring.
        public void Stop()
        {
            _watcher.EnableRaisingEvents = false;
            Console.WriteLine("Monitoring stopped.");
        }

        // Event handler for newly created PDF files.
        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            // Run the processing on a separate thread to avoid blocking the watcher.
            ThreadPool.QueueUserWorkItem(_ => ProcessNewPdf(e.FullPath));
        }

        // Wait until the file is ready for reading, then encrypt and move it.
        private void ProcessNewPdf(string sourceFilePath)
        {
            try
            {
                // Wait for the file to be fully written.
                const int maxAttempts = 10;
                int attempts = 0;
                while (attempts < maxAttempts)
                {
                    try
                    {
                        using (FileStream fs = new FileStream(sourceFilePath, FileMode.Open, FileAccess.Read, FileShare.None))
                        {
                            // If we can open the file exclusively, it's ready.
                            break;
                        }
                    }
                    catch (IOException)
                    {
                        // File is still being written; wait and retry.
                        Thread.Sleep(500);
                        attempts++;
                    }
                }

                if (attempts == maxAttempts)
                {
                    Console.Error.WriteLine($"Timeout waiting for file: {sourceFilePath}");
                    return;
                }

                // Determine the destination (encrypted) file path inside the archive folder.
                string fileName = Path.GetFileName(sourceFilePath);
                string encryptedFilePath = Path.Combine(_archivePath, fileName);

                // Encrypt the PDF using PdfFileSecurity.
                // The constructor takes the input PDF path and the output (encrypted) path.
                using (PdfFileSecurity security = new PdfFileSecurity(sourceFilePath, encryptedFilePath))
                {
                    // Encrypt with user/owner passwords, allow printing, and use 256‑bit AES.
                    security.EncryptFile(
                        _userPassword,
                        _ownerPassword,
                        DocumentPrivilege.Print,
                        KeySize.x256,
                        Algorithm.AES);
                }

                // Optionally delete the original unencrypted file.
                File.Delete(sourceFilePath);
                Console.WriteLine($"Encrypted and archived: {encryptedFilePath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing file '{sourceFilePath}': {ex.Message}");
            }
        }

        // Dispose pattern to clean up the watcher.
        public void Dispose()
        {
            _watcher?.Dispose();
        }
    }

    // Simple console host.
    class Program
    {
        static void Main(string[] args)
        {
            // Example usage: first argument = folder to watch, second argument = archive folder.
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: PdfArchiveService <watchFolder> <archiveFolder>");
                return;
            }

            string watchFolder = args[0];
            string archiveFolder = args[1];

            if (!Directory.Exists(watchFolder))
            {
                Console.Error.WriteLine($"Watch folder does not exist: {watchFolder}");
                return;
            }

            using (var service = new DirectoryMonitorService(watchFolder, archiveFolder))
            {
                service.Start();

                Console.WriteLine("Press ENTER to stop...");
                Console.ReadLine();

                service.Stop();
            }
        }
    }
}