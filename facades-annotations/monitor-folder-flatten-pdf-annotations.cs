using System;
using System.IO;
using System.Threading;
using Aspose.Pdf.Facades;

class Program
{
    // Folder to watch – change as needed
    private const string WatchFolder = @"C:\PdfWatchFolder";

    static void Main()
    {
        if (!Directory.Exists(WatchFolder))
        {
            Console.Error.WriteLine($"Folder does not exist: {WatchFolder}");
            return;
        }

        using (var watcher = new FileSystemWatcher(WatchFolder, "*.pdf"))
        {
            watcher.Created += OnCreated;
            watcher.EnableRaisingEvents = true;

            Console.WriteLine($"Monitoring folder: {WatchFolder}");
            Console.WriteLine("Press ENTER to exit.");
            Console.ReadLine(); // keep the process alive
        }
    }

    private static void OnCreated(object sender, FileSystemEventArgs e)
    {
        // Run processing on a separate thread to avoid blocking the watcher
        ThreadPool.QueueUserWorkItem(_ => ProcessPdf(e.FullPath));
    }

    private static void ProcessPdf(string inputPath)
    {
        // Wait until the file is fully written and accessible
        const int maxAttempts = 10;
        int attempt = 0;
        while (attempt < maxAttempts)
        {
            try
            {
                using (FileStream fs = File.Open(inputPath, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    // File is ready
                    break;
                }
            }
            catch (IOException)
            {
                attempt++;
                Thread.Sleep(500);
            }
        }

        string outputPath = Path.Combine(
            Path.GetDirectoryName(inputPath),
            Path.GetFileNameWithoutExtension(inputPath) + "_flattened.pdf");

        try
        {
            // Flatten annotations using PdfAnnotationEditor
            using (Aspose.Pdf.Facades.PdfAnnotationEditor editor = new Aspose.Pdf.Facades.PdfAnnotationEditor())
            {
                editor.BindPdf(inputPath);
                editor.DeleteAnnotations();               // optional: remove all annotations
                editor.FlatteningAnnotations();           // flatten remaining annotations
                editor.Save(outputPath);
            }

            Console.WriteLine($"Processed and saved: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
        }
    }
}