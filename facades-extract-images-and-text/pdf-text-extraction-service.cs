using System;
using System.IO;
using System.Threading;
using Aspose.Pdf.Facades;

namespace PdfTextExtractionService
{
    // Windows‑service‑like class that watches a folder and extracts text from new PDF files.
    // It no longer inherits from System.ServiceProcess.ServiceBase to avoid the missing assembly reference.
    public class ExtractionService
    {
        private FileSystemWatcher _watcher;
        private readonly string _watchFolder = @"C:\WatchedFolder"; // folder to monitor
        private readonly string _outputFolder = @"C:\ExtractedText"; // folder for txt files

        // Starts the watcher – equivalent to ServiceBase.OnStart.
        public void Start(string[] args)
        {
            // Ensure output directory exists.
            Directory.CreateDirectory(_outputFolder);

            // Set up the file system watcher.
            _watcher = new FileSystemWatcher
            {
                Path = _watchFolder,
                Filter = "*.pdf",
                NotifyFilter = NotifyFilters.FileName | NotifyFilters.CreationTime,
                EnableRaisingEvents = true,
                IncludeSubdirectories = false
            };

            // Subscribe to the Created event.
            _watcher.Created += OnPdfCreated;
        }

        // Stops the watcher – equivalent to ServiceBase.OnStop.
        public void Stop()
        {
            if (_watcher != null)
            {
                _watcher.EnableRaisingEvents = false;
                _watcher.Created -= OnPdfCreated;
                _watcher.Dispose();
                _watcher = null;
            }
        }

        // Event handler called when a new PDF file appears.
        private void OnPdfCreated(object sender, FileSystemEventArgs e)
        {
            try
            {
                // Simple retry logic – wait until the file is no longer locked.
                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        using (FileStream fs = File.Open(e.FullPath, FileMode.Open, FileAccess.Read, FileShare.None))
                        {
                            // File is ready.
                            break;
                        }
                    }
                    catch (IOException)
                    {
                        Thread.Sleep(500);
                    }
                }

                ExtractTextFromPdf(e.FullPath);
            }
            catch (Exception ex)
            {
                // Log the error – for simplicity we write to the console.
                Console.Error.WriteLine($"Error processing PDF '{e.FullPath}': {ex.Message}");
            }
        }

        // Uses Aspose.Pdf.Facades.PdfExtractor to extract all text from the PDF.
        private void ExtractTextFromPdf(string pdfPath)
        {
            // Determine output text file path.
            string fileName = Path.GetFileNameWithoutExtension(pdfPath);
            string txtPath = Path.Combine(_outputFolder, fileName + ".txt");

            // Overwrite any existing output file.
            if (File.Exists(txtPath))
                File.Delete(txtPath);

            // Perform extraction.
            using (PdfExtractor extractor = new PdfExtractor())
            {
                extractor.BindPdf(pdfPath);
                extractor.ExtractText();
                extractor.GetText(txtPath);
            }
        }

        // Main entry point. Allows running as a console app for debugging or as a service.
        public static void Main(string[] args)
        {
            if (Environment.UserInteractive)
            {
                // Run as a console application for testing.
                var service = new ExtractionService();
                service.Start(args);
                Console.WriteLine("Service started. Press any key to stop...");
                Console.ReadKey();
                service.Stop();
                Console.WriteLine("Service stopped.");
            }
            else
            {
                // Running as a true Windows Service would require a reference to
                // System.ServiceProcess.ServiceController, which is not available in the
                // current project configuration. If you need real service support, add the
                // NuGet package "System.ServiceProcess.ServiceController" and inherit from
                // ServiceBase again.
                Console.Error.WriteLine("Running as a Windows Service is not supported in this build configuration.");
            }
        }
    }
}
