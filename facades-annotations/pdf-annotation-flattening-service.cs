using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

class PdfAnnotationFlatteningService
{
    // Folder to monitor – change as needed
    private const string WatchFolder = @"C:\WatchedPdfFolder";

    // Entry point
    static void Main()
    {
        // Ensure the folder exists
        if (!Directory.Exists(WatchFolder))
        {
            Console.Error.WriteLine($"Folder does not exist: {WatchFolder}");
            return;
        }

        // Set up a FileSystemWatcher to monitor PDF files
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
            Console.WriteLine("Press Enter to exit.");

            // Keep the application running until the user presses Enter
            Console.ReadLine();
        }
    }

    // Event handler invoked when a new PDF file appears in the folder
    private static void OnPdfCreated(object sender, FileSystemEventArgs e)
    {
        // Run the processing on a separate task to avoid blocking the watcher
        Task.Run(() => ProcessPdf(e.FullPath));
    }

    // Attempts to open the PDF, flatten its annotations, and overwrite the file
    private static void ProcessPdf(string pdfPath)
    {
        // Wait until the file is fully written and accessible
        const int maxAttempts = 10;
        const int delayMs = 500;
        int attempts = 0;
        while (attempts < maxAttempts)
        {
            try
            {
                // Try opening the file for exclusive read/write access
                using (FileStream fs = new FileStream(pdfPath, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                {
                    // If we reach here, the file is ready; close the stream immediately
                    break;
                }
            }
            catch (IOException)
            {
                // File is still being written – wait and retry
                Thread.Sleep(delayMs);
                attempts++;
            }
        }

        // If the file is still locked after retries, skip processing
        if (attempts == maxAttempts)
        {
            Console.Error.WriteLine($"Failed to access file (still locked): {pdfPath}");
            return;
        }

        try
        {
            // Use PdfAnnotationEditor to flatten annotations
            using (Aspose.Pdf.Facades.PdfAnnotationEditor editor = new Aspose.Pdf.Facades.PdfAnnotationEditor())
            {
                // Bind the PDF file
                editor.BindPdf(pdfPath);

                // Flatten all annotations in the document
                editor.FlatteningAnnotations();

                // Save the changes back to the same file (overwrites original)
                editor.Save(pdfPath);

                // Close the editor (optional, as using will dispose)
                editor.Close();
            }

            Console.WriteLine($"Flattened annotations for: {Path.GetFileName(pdfPath)}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
        }
    }
}