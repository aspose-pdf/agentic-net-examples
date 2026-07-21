using System;
using System.IO;
using System.Threading;
using System.Drawing; // Needed for System.Drawing.Rectangle
using Aspose.Pdf;
using Aspose.Pdf.Facades;

public class PdfMonitorService
{
    // Made nullable because the watcher is created in Start() and can be null before that.
    private FileSystemWatcher? _watcher;
    private readonly string _inputFolder = @"C:\PdfInput";
    private readonly string _outputFolder = @"C:\PdfOutput";

    // Public methods that mimic ServiceBase behaviour
    public void Start(string[] args)
    {
        // Ensure output folder exists
        Directory.CreateDirectory(_outputFolder);

        // Set up watcher for new PDF files
        _watcher = new FileSystemWatcher(_inputFolder, "*.pdf")
        {
            NotifyFilter = NotifyFilters.FileName | NotifyFilters.CreationTime,
            EnableRaisingEvents = true,
            IncludeSubdirectories = false
        };
        _watcher.Created += OnCreated;
    }

    public void Stop()
    {
        if (_watcher != null)
        {
            _watcher.EnableRaisingEvents = false;
            _watcher.Created -= OnCreated;
            _watcher.Dispose();
            _watcher = null;
        }
    }

    private void OnCreated(object sender, FileSystemEventArgs e)
    {
        // Process the file on a separate thread to avoid blocking the watcher
        ThreadPool.QueueUserWorkItem(_ => ProcessPdf(e.FullPath));
    }

    private void ProcessPdf(string sourcePath)
    {
        // Wait until the file is fully written and accessible
        const int maxAttempts = 10;
        int attempt = 0;
        while (attempt < maxAttempts)
        {
            try
            {
                using (FileStream fs = new FileStream(sourcePath, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    // File is ready
                    break;
                }
            }
            catch (IOException)
            {
                Thread.Sleep(500);
                attempt++;
            }
        }

        // Define output path
        string fileName = Path.GetFileNameWithoutExtension(sourcePath);
        string outputPath = Path.Combine(_outputFolder, $"{fileName}_processed.pdf");

        // Edit PDF using Aspose.Pdf.Facades
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the source PDF
            editor.BindPdf(sourcePath);

            // Create a simple text annotation on the first page.
            // PdfContentEditor.CreateText expects a System.Drawing.Rectangle (x, y, width, height).
            // The original Aspose.Pdf.Rectangle used (llx, lly, urx, ury). Convert accordingly.
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 500, 200, 50);
            // Parameters: rectangle, text, font name, isBold, color (as string hex), page number (1‑based)
            editor.CreateText(rect, "Processed by Service", "Helvetica", false, "#FF0000", 1);

            // Save the edited PDF
            editor.Save(outputPath);
        }
    }

    // Main entry point for debugging or running as console app
    public static void Main(string[] args)
    {
        if (Environment.UserInteractive)
        {
            // Run as console application for testing
            var service = new PdfMonitorService();
            service.Start(args);
            Console.WriteLine("PdfMonitorService is running. Press Enter to stop...");
            Console.ReadLine();
            service.Stop();
        }
        else
        {
            // In a real Windows Service scenario you would inherit from ServiceBase.
            // This sample is kept ServiceBase‑free to compile without the System.ServiceProcess package.
            // Deploying as a Windows Service would require adding the System.ServiceProcess.ServiceController NuGet package
            // and inheriting from ServiceBase.
        }
    }
}
