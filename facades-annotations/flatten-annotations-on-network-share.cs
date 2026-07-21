using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    // Network share path containing PDFs (UNC format)
    private const string NetworkSharePath = @"\\SERVER\SharedFolder\PDFs";

    static void Main()
    {
        // Verify the network share exists
        if (!Directory.Exists(NetworkSharePath))
        {
            Console.Error.WriteLine($"Network share not found: {NetworkSharePath}");
            return;
        }

        // Get all PDF files recursively
        string[] pdfFiles = Directory.GetFiles(NetworkSharePath, "*.pdf", SearchOption.AllDirectories);

        foreach (string pdfPath in pdfFiles)
        {
            try
            {
                // Use PdfAnnotationEditor to flatten annotations
                using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
                {
                    // Bind the PDF file
                    editor.BindPdf(pdfPath);

                    // Flatten all annotations in the document
                    editor.FlatteningAnnotations();

                    // Overwrite the original file with the flattened version
                    editor.Save(pdfPath);

                    // Close the editor (Dispose is called automatically by using)
                }

                Console.WriteLine($"Flattened annotations in: {pdfPath}");
            }
            catch (Exception ex)
            {
                // Log any errors but continue processing other files
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }

        // This console application can be scheduled as a nightly task (e.g., Windows Task Scheduler)
    }
}