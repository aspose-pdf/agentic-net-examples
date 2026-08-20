using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // UNC path to the network share containing PDFs
        const string networkPath = @"\\server\share\pdfs";

        if (!Directory.Exists(networkPath))
        {
            Console.Error.WriteLine($"Directory not found: {networkPath}");
            return;
        }

        // Get all PDF files in the share (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(networkPath, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string pdfPath in pdfFiles)
        {
            try
            {
                // Overwrite the original file after flattening annotations
                using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
                {
                    editor.BindPdf(pdfPath);               // Load the PDF
                    editor.FlatteningAnnotations();        // Flatten all annotations
                    editor.Save(pdfPath);                  // Save back to the same file
                }

                Console.WriteLine($"Flattened annotations in: {pdfPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}