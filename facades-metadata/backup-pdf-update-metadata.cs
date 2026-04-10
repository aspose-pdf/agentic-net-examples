using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "document.pdf";
        const string backupPath = "document_backup.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the original PDF and create a backup copy
            using (Document doc = new Document(inputPath))
            {
                doc.Save(backupPath); // backup before any changes
            }

            // Modify metadata using PdfFileInfo
            using (PdfFileInfo info = new PdfFileInfo(inputPath))
            {
                // Example metadata updates
                info.Title = "Updated Title";
                info.Author = "Updated Author";
                info.Subject = "Updated Subject";
                info.Keywords = "Aspose, PDF, Metadata";

                // Save the updated metadata back to the original file
                bool success = info.SaveNewInfo(inputPath);
                if (!success)
                {
                    Console.Error.WriteLine("Failed to save updated metadata.");
                }
            }

            Console.WriteLine($"Backup created at '{backupPath}'. Metadata updated in '{inputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}