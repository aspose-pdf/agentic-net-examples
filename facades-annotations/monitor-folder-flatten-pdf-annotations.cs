using System;
using System.IO;
using System.Threading;
using Aspose.Pdf.Facades;

class PdfAnnotationFlattenerService
{
    // Folder to watch for new PDF files
    private static readonly string WatchFolder = @"C:\PdfWatchFolder";

    // Entry point
    static void Main()
    {
        // Ensure the watch folder exists
        if (!Directory.Exists(WatchFolder))
        {
            Console.Error.WriteLine($"Watch folder does not exist: {WatchFolder}");
            return;
        }

        // Set up a FileSystemWatcher to monitor *.pdf files
        using (FileSystemWatcher watcher = new FileSystemWatcher())
        {
            watcher.Path = WatchFolder;
            watcher.Filter = "*.pdf";
            watcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.CreationTime;

            // Subscribe to the Created event
            watcher.Created += OnPdfCreated;

            // Begin watching
            watcher.EnableRaisingEvents = true;

            Console.WriteLine($"Monitoring folder: {WatchFolder}");
            Console.WriteLine("Press Ctrl+C to exit.");

            // Keep the application running
            ManualResetEvent quitEvent = new ManualResetEvent(false);
            Console.CancelKeyPress += (sender, e) =>
            {
                e.Cancel = true;
                quitEvent.Set();
            };
            quitEvent.WaitOne();
        }
    }

    // Event handler invoked when a new PDF file appears
    private static void OnPdfCreated(object sender, FileSystemEventArgs e)
    {
        // Run processing on a separate thread to avoid blocking the watcher
        ThreadPool.QueueUserWorkItem(_ => ProcessPdf(e.FullPath));
    }

    // Flattens annotations of the specified PDF and saves the result
    private static void ProcessPdf(string inputPath)
    {
        // Wait until the file is fully written (simple retry logic)
        const int maxAttempts = 10;
        const int delayMs = 500;
        int attempts = 0;
        while (attempts < maxAttempts)
        {
            try
            {
                using (FileStream testStream = File.Open(inputPath, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    // File can be opened exclusively – it is ready
                    break;
                }
            }
            catch (IOException)
            {
                attempts++;
                Thread.Sleep(delayMs);
            }
        }

        if (attempts == maxAttempts)
        {
            Console.Error.WriteLine($"Failed to access file after multiple attempts: {inputPath}");
            return;
        }

        string directory = Path.GetDirectoryName(inputPath);
        string fileNameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
        string outputPath = Path.Combine(directory, $"{fileNameWithoutExt}_flattened.pdf");

        try
        {
            // Use PdfAnnotationEditor to flatten annotations
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(inputPath);          // Load the PDF
                editor.DeleteAnnotations();         // Optionally remove annotation objects
                editor.FlatteningAnnotations();    // Flatten remaining annotations onto the page content
                editor.Save(outputPath);            // Save the flattened PDF
            }

            Console.WriteLine($"Flattened PDF saved: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
        }
    }
}