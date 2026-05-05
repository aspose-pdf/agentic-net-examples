using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    // Network share path containing PDF files (e.g. @"\\Server\Shared\PdfDocuments")
    private const string PdfFolderPath = @"\\Server\Shared\PdfDocuments";

    static void Main()
    {
        try
        {
            // Verify that the directory exists
            if (!Directory.Exists(PdfFolderPath))
            {
                Console.Error.WriteLine($"Directory not found: {PdfFolderPath}");
                return;
            }

            // Get all PDF files in the directory (non‑recursive; add SearchOption.AllDirectories if needed)
            string[] pdfFiles = Directory.GetFiles(PdfFolderPath, "*.pdf", SearchOption.TopDirectoryOnly);

            foreach (string pdfPath in pdfFiles)
            {
                try
                {
                    // Use the facade to open, flatten annotations, and save the same file
                    using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
                    {
                        editor.BindPdf(pdfPath);                 // Load the PDF
                        editor.FlatteningAnnotations();          // Flatten all annotations
                        editor.Save(pdfPath);                    // Overwrite the original file
                    }

                    Console.WriteLine($"Flattened annotations in: {pdfPath}");
                }
                catch (Exception ex)
                {
                    // Log but continue processing other files
                    Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
                }
            }

            Console.WriteLine("Nightly annotation flattening completed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Fatal error: {ex.Message}");
        }
    }
}