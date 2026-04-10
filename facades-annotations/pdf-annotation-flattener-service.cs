using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

namespace PdfAnnotationFlattener
{
    // Service that watches a folder for new PDF files and flattens their annotations.
    public class PdfAnnotationFlattenerService
    {
        private readonly string _watchFolder;
        private readonly FileSystemWatcher _watcher;
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        public PdfAnnotationFlattenerService(string watchFolder)
        {
            _watchFolder = watchFolder;

            // Monitor only *.pdf files in the specified folder.
            _watcher = new FileSystemWatcher(_watchFolder, "*.pdf")
            {
                NotifyFilter = NotifyFilters.FileName | NotifyFilters.CreationTime
            };
            _watcher.Created += OnCreated;
        }

        // Start the background monitoring.
        public void Start()
        {
            _watcher.EnableRaisingEvents = true;
            Console.WriteLine($"Started watching folder: {_watchFolder}");
        }

        // Stop the monitoring and clean up resources.
        public void Stop()
        {
            _watcher.EnableRaisingEvents = false;
            _watcher.Created -= OnCreated;
            _watcher.Dispose();
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
            Console.WriteLine("Stopped watching.");
        }

        // Event handler invoked when a new PDF file appears.
        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            // Process each new file on a separate task to avoid blocking the watcher.
            Task.Run(() => ProcessPdfAsync(e.FullPath, _cancellationTokenSource.Token));
        }

        // Attempts to open the file, flatten annotations, and overwrite the original file.
        private async Task ProcessPdfAsync(string pdfPath, CancellationToken token)
        {
            // The file may still be being copied; retry a few times.
            const int maxRetries = 5;
            const int delayMs = 500;

            for (int attempt = 0; attempt < maxRetries && !token.IsCancellationRequested; attempt++)
            {
                try
                {
                    // Use PdfAnnotationEditor (facade) to work with annotations.
                    using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
                    {
                        // Bind the PDF file to the editor.
                        editor.BindPdf(pdfPath);

                        // Flatten all annotations so they become part of the page content.
                        editor.FlatteningAnnotations();

                        // Save back to the same file (overwrites the original).
                        editor.Save(pdfPath);
                    }

                    Console.WriteLine($"Flattened annotations in: {Path.GetFileName(pdfPath)}");
                    break; // Success – exit retry loop.
                }
                catch (IOException)
                {
                    // The file is likely still locked; wait and retry.
                    await Task.Delay(delayMs, token);
                }
                catch (Exception ex)
                {
                    // Log unexpected errors and abort processing for this file.
                    Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
                    break;
                }
            }
        }
    }

    // Simple console host to run the background service.
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Folder to monitor – adjust as needed.
            string folderToWatch = @"C:\WatchedPdfs";

            // Ensure the folder exists.
            if (!Directory.Exists(folderToWatch))
            {
                Console.Error.WriteLine($"Folder does not exist: {folderToWatch}");
                return;
            }

            var service = new PdfAnnotationFlattenerService(folderToWatch);
            service.Start();

            Console.WriteLine("Press ENTER to stop...");
            Console.ReadLine();

            service.Stop();
        }
    }
}