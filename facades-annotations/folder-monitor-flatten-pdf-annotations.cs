using System;
using System.IO;
using Aspose.Pdf.Facades;

namespace PdfAnnotationFlattenerService
{
    class Program
    {
        private const string WatchFolder = "input";
        private const string Filter = "*.pdf";

        static void Main()
        {
            if (!Directory.Exists(WatchFolder))
            {
                Console.Error.WriteLine($"Watch folder does not exist: {WatchFolder}");
                return;
            }

            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = WatchFolder;
            watcher.Filter = Filter;
            watcher.Created += OnCreated;
            watcher.EnableRaisingEvents = true;

            Console.WriteLine($"Monitoring folder '{WatchFolder}' for new PDF files. Press Enter to exit.");
            Console.ReadLine();
        }

        private static void OnCreated(object sender, FileSystemEventArgs e)
        {
            // Small delay to ensure the file is fully written
            System.Threading.Thread.Sleep(500);
            try
            {
                ProcessPdf(e.FullPath);
                Console.WriteLine($"Processed and flattened: {e.Name}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{e.Name}': {ex.Message}");
            }
        }

        private static void ProcessPdf(string inputPath)
        {
            // Ensure the file exists and has .pdf extension
            if (!File.Exists(inputPath))
            {
                throw new FileNotFoundException("Input PDF not found.", inputPath);
            }

            string outputFileName = Path.GetFileNameWithoutExtension(inputPath) + "_flattened.pdf";

            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(inputPath);
                editor.FlatteningAnnotations();
                editor.Save(outputFileName);
            }
        }
    }
}
