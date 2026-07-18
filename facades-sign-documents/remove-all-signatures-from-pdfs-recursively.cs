using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Root folder to process – change as needed
        const string inputRoot = @"C:\PdfFolder";

        // Process all PDF files recursively
        foreach (string pdfPath in Directory.EnumerateFiles(inputRoot, "*.pdf", SearchOption.AllDirectories))
        {
            try
            {
                // Initialize the facade, bind the PDF, remove all signatures and save back
                using (PdfFileSignature signer = new PdfFileSignature())
                {
                    signer.BindPdf(pdfPath);          // Load the PDF
                    signer.RemoveSignatures();        // Remove every signature
                    signer.Save(pdfPath);             // Overwrite the original file
                }

                Console.WriteLine($"Signatures removed: {pdfPath}");
            }
            catch (Exception ex)
            {
                // Log any errors but continue processing other files
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}