using System;
using System.IO;
using System.Threading;
using Aspose.Pdf;

namespace PdfSanitizerConsole
{
    class Program
    {
        // Folder paths (relative to the executable directory)
        private static readonly string watchFolder = "WatchFolder";
        private static readonly string archiveFolder = "ArchiveFolder";

        static void Main(string[] args)
        {
            // Ensure the required directories exist
            Directory.CreateDirectory(watchFolder);
            Directory.CreateDirectory(archiveFolder);

            // Create a self‑contained sample PDF in the watch folder
            string samplePdfPath = Path.Combine(watchFolder, "sample.pdf");
            using (Document sampleDoc = new Document())
            {
                // Evaluation mode limits collections to four elements – one page is safe
                sampleDoc.Pages.Add();
                sampleDoc.Save(samplePdfPath);
            }

            // Process all PDF files currently present in the watch folder
            string[] pdfFiles = Directory.GetFiles(watchFolder, "*.pdf");
            foreach (string sourcePath in pdfFiles)
            {
                // Small pause in case the file is still being written (simulates watcher behaviour)
                Thread.Sleep(200);
                ProcessPdf(sourcePath);
            }

            Console.WriteLine("PDF sanitization completed.");
        }

        private static void ProcessPdf(string sourcePath)
        {
            string tempPath = Path.Combine(watchFolder, "temp_" + Path.GetFileName(sourcePath));
            try
            {
                using (Document doc = new Document(sourcePath))
                {
                    // Sanitize the PDF – these methods are part of the core Aspose.Pdf API
                    doc.RemoveMetadata();
                    doc.Flatten();
                    doc.OptimizeResources();
                    doc.Save(tempPath);
                }

                string archivePath = Path.Combine(archiveFolder, Path.GetFileName(sourcePath));
                // Move the sanitized file to the archive folder and delete the original
                File.Move(tempPath, archivePath);
                File.Delete(sourcePath);
            }
            catch (Exception ex)
            {
                // In a real‑world scenario you would log the exception; here we just write to console
                Console.WriteLine($"Error processing '{sourcePath}': {ex.Message}");
            }
        }
    }
}
