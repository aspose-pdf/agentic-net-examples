using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Directory containing the PDF files to process
        string directoryPath = @"C:\PdfFolder";

        if (!Directory.Exists(directoryPath))
        {
            Console.Error.WriteLine($"Directory not found: {directoryPath}");
            return;
        }

        // Instantiate a Facades class as required (no IDisposable, so no using)
        PdfFileEditor editor = new PdfFileEditor();

        // Get all PDF files in the directory
        string[] pdfFiles = Directory.GetFiles(directoryPath, "*.pdf");

        foreach (string pdfPath in pdfFiles)
        {
            try
            {
                // Load each PDF, modify viewer preference, and save
                using (Document doc = new Document(pdfPath))
                {
                    // Set the PDF to open in full‑screen mode
                    doc.PageMode = PageMode.FullScreen;

                    // Overwrite the original file with the updated version
                    doc.Save(pdfPath);
                }

                Console.WriteLine($"Updated: {Path.GetFileName(pdfPath)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}