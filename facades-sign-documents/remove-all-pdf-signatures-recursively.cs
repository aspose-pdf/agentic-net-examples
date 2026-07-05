using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Use the first argument as the target directory, or default to the current directory.
        string targetDirectory = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();

        // Recursively enumerate all PDF files.
        foreach (string pdfFilePath in Directory.EnumerateFiles(targetDirectory, "*.pdf", SearchOption.AllDirectories))
        {
            try
            {
                // Bind the PDF, remove all signatures, and save back to the same file.
                using (PdfFileSignature pdfSignature = new PdfFileSignature())
                {
                    pdfSignature.BindPdf(pdfFilePath);   // Load the PDF.
                    pdfSignature.RemoveSignatures();    // Remove every signature.
                    pdfSignature.Save(pdfFilePath);     // Overwrite the original file.
                }

                Console.WriteLine($"Processed: {pdfFilePath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfFilePath}': {ex.Message}");
            }
        }
    }
}