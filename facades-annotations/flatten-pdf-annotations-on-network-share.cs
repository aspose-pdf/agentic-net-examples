using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    // Entry point – can be scheduled to run nightly (e.g., via Windows Task Scheduler)
    static void Main()
    {
        // UNC path to the network share containing PDF files
        const string networkSharePath = @"\\ServerName\SharedFolder\PDFs";

        // Verify the directory exists
        if (!Directory.Exists(networkSharePath))
        {
            Console.Error.WriteLine($"Directory not found: {networkSharePath}");
            return;
        }

        // Get all PDF files in the directory (non‑recursive; add SearchOption.AllDirectories if needed)
        string[] pdfFiles = Directory.GetFiles(networkSharePath, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string pdfPath in pdfFiles)
        {
            try
            {
                // Use PdfAnnotationEditor to flatten annotations.
                // The editor does not implement IDisposable, but we still dispose the underlying Document via using.
                using (Document doc = new Document(pdfPath))
                {
                    // Initialize the facade with the loaded document.
                    PdfAnnotationEditor editor = new PdfAnnotationEditor(doc);

                    // Flatten all annotations in the document.
                    editor.FlatteningAnnotations();

                    // Overwrite the original file with the flattened version.
                    editor.Save(pdfPath);
                }

                Console.WriteLine($"Flattened annotations in: {pdfPath}");
            }
            catch (Exception ex)
            {
                // Log any errors but continue processing remaining files.
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Annotation flattening task completed.");
    }
}