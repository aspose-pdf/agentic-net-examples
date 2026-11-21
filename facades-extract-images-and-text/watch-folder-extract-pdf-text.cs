using System;
using System.IO;
using System.Threading;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    // Folder to watch – change as needed
    private const string WatchFolder = @"C:\WatchedPdfs";

    static void Main()
    {
        // Ensure the folder exists
        if (!Directory.Exists(WatchFolder))
        {
            Console.Error.WriteLine($"Folder does not exist: {WatchFolder}");
            return;
        }

        // Set up a FileSystemWatcher to monitor new PDF files
        using (FileSystemWatcher watcher = new FileSystemWatcher())
        {
            watcher.Path = WatchFolder;
            watcher.Filter = "*.pdf";
            watcher.Created += OnCreated;
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

    // Event handler called when a new PDF file appears
    private static void OnCreated(object sender, FileSystemEventArgs e)
    {
        // Give the OS a moment to finish writing the file
        for (int i = 0; i < 10; i++)
        {
            try
            {
                using (FileStream stream = File.Open(e.FullPath, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    // If we can open the file exclusively, it is ready
                    break;
                }
            }
            catch (IOException)
            {
                Thread.Sleep(500);
            }
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(e.FullPath))
            {
                // Extract all text using TextAbsorber
                TextAbsorber absorber = new TextAbsorber();
                doc.Pages.Accept(absorber);
                string extractedText = absorber.Text ?? string.Empty;

                // Write the extracted text to a .txt file beside the PDF
                string txtPath = Path.ChangeExtension(e.FullPath, ".txt");
                File.WriteAllText(txtPath, extractedText);

                Console.WriteLine($"Extracted text from '{Path.GetFileName(e.FullPath)}' to '{Path.GetFileName(txtPath)}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to process '{e.Name}': {ex.Message}");
        }
    }
}