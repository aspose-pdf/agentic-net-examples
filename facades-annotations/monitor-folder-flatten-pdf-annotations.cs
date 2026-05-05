using System;
using System.IO;
using System.Threading;
using Aspose.Pdf.Facades;

class PdfAnnotationFlattenService
{
    // Folder to monitor for new PDF files
    private const string WatchFolder = @"C:\WatchedFolder";

    // Suffix added to flattened PDFs
    private const string OutputSuffix = "_flattened.pdf";

    static void Main()
    {
        // Ensure the watch folder exists
        if (!Directory.Exists(WatchFolder))
        {
            Console.Error.WriteLine($"Folder does not exist: {WatchFolder}");
            return;
        }

        // Set up a FileSystemWatcher to monitor PDF creation
        using (FileSystemWatcher watcher = new FileSystemWatcher())
        {
            watcher.Path = WatchFolder;
            watcher.Filter = "*.pdf";
            watcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.CreationTime;
            watcher.Created += OnPdfCreated;
            watcher.EnableRaisingEvents = true;

            Console.WriteLine($"Monitoring folder: {WatchFolder}");
            Console.WriteLine("Press ENTER to exit.");

            // Keep the service running until user stops it
            Console.ReadLine();
        }
    }

    // Event handler invoked when a new PDF file appears
    private static void OnPdfCreated(object sender, FileSystemEventArgs e)
    {
        // Run processing on a separate thread to avoid blocking the watcher
        ThreadPool.QueueUserWorkItem(_ => ProcessPdf(e.FullPath));
    }

    // Attempts to flatten annotations of the specified PDF file
    private static void ProcessPdf(string inputPath)
    {
        if (string.IsNullOrWhiteSpace(inputPath))
        {
            Console.Error.WriteLine("Input path is null or empty.");
            return;
        }

        // Wait until the file is ready for reading (in case it is still being copied)
        const int maxAttempts = 10;
        int attempt = 0;
        while (attempt < maxAttempts)
        {
            try
            {
                using (FileStream fs = File.Open(inputPath, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    // If we can open the file exclusively, it is ready
                    break;
                }
            }
            catch (IOException)
            {
                // File is still locked; wait a moment and retry
                Thread.Sleep(500);
                attempt++;
            }
        }

        // Determine output file name safely
        string directory = Path.GetDirectoryName(inputPath) ?? string.Empty;
        string fileNameWithoutExt = Path.GetFileNameWithoutExtension(inputPath) ?? string.Empty;
        string outputPath = Path.Combine(directory, fileNameWithoutExt + OutputSuffix);

        try
        {
            // Initialize the annotation editor facade
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                // Bind the source PDF
                editor.BindPdf(inputPath);

                // Flatten all annotations in the document (correct method name)
                editor.FlatteningAnnotations();

                // Save the flattened PDF
                editor.Save(outputPath);
            }

            Console.WriteLine($"Flattened PDF saved: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
        }
    }
}
