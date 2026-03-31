using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        string inputPath = "input.pdf";
        string backupPath = "input_backup.pdf";
        string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input file not found: " + inputPath);
            return;
        }

        // Create a backup copy of the original PDF
        try
        {
            File.Copy(inputPath, backupPath, true);
            Console.WriteLine("Backup created: " + backupPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine("Backup failed: " + ex.Message);
            return;
        }

        // Modify metadata using PdfFileInfo and save to a new file
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
        {
            pdfInfo.Title = "Updated Title";
            pdfInfo.Author = "Updated Author";
            pdfInfo.Subject = "Updated Subject";

            bool saved = pdfInfo.SaveNewInfo(outputPath);
            Console.WriteLine(saved ? "Metadata updated and saved to " + outputPath : "Failed to save updated PDF.");
        }
    }
}
