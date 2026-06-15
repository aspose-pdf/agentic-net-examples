using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Root directory containing PDFs – adjust as needed
        string rootDirectory = @"C:\PdfFolder";

        if (!Directory.Exists(rootDirectory))
        {
            Console.Error.WriteLine($"Directory not found: {rootDirectory}");
            return;
        }

        // Retrieve all PDF files recursively
        string[] pdfFiles = Directory.GetFiles(rootDirectory, "*.pdf", SearchOption.AllDirectories);

        foreach (string inputPath in pdfFiles)
        {
            try
            {
                // Initialize the facade, bind the PDF, remove signatures, and save
                PdfFileSignature pdfSignature = new PdfFileSignature();
                pdfSignature.BindPdf(inputPath);          // Load the PDF
                pdfSignature.RemoveSignatures();          // Remove all signatures

                // Save to a new file (original file is left untouched)
                string outputPath = Path.Combine(
                    Path.GetDirectoryName(inputPath),
                    Path.GetFileNameWithoutExtension(inputPath) + "_unsigned.pdf");

                pdfSignature.Save(outputPath);            // Persist changes

                Console.WriteLine($"Signatures removed: {inputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to process '{inputPath}': {ex.Message}");
            }
        }
    }
}