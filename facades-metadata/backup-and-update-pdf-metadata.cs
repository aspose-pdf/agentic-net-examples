using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Directory containing PDFs to process
        const string pdfDirectory = @"C:\PdfFiles";

        if (!Directory.Exists(pdfDirectory))
        {
            Console.Error.WriteLine($"Directory not found: {pdfDirectory}");
            return;
        }

        // Process each PDF file in the directory
        foreach (string pdfPath in Directory.GetFiles(pdfDirectory, "*.pdf"))
        {
            try
            {
                // Create a backup copy before any modification
                string backupPath = Path.Combine(
                    Path.GetDirectoryName(pdfPath),
                    Path.GetFileNameWithoutExtension(pdfPath) + "_backup.pdf");

                // Overwrite any existing backup to ensure the latest original is saved
                File.Copy(pdfPath, backupPath, true);
                Console.WriteLine($"Backup created: {backupPath}");

                // Modify metadata using PdfFileInfo (Facades API)
                PdfFileInfo fileInfo = new PdfFileInfo();
                fileInfo.BindPdf(pdfPath);               // Load the PDF for metadata editing
                fileInfo.Title = "Updated Title";        // Example metadata changes
                fileInfo.Author = "Updated Author";
                fileInfo.Subject = "Updated Subject";
                fileInfo.Keywords = "Aspose, PDF, Metadata";

                // Save changes back to the original file
                fileInfo.Save(pdfPath);
                Console.WriteLine($"Metadata updated for: {pdfPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Processing completed.");
    }
}