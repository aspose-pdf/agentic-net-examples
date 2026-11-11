using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing PDFs to process
        const string inputFolder = "InputPdfs";
        // Folder where backup copies will be stored
        const string backupFolder = "BackupPdfs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure backup folder exists
        Directory.CreateDirectory(backupFolder);

        // Process each PDF file in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            try
            {
                string fileName = Path.GetFileName(pdfPath);
                string backupPath = Path.Combine(backupFolder, fileName);

                // Create a backup copy before any modification
                File.Copy(pdfPath, backupPath, true);
                Console.WriteLine($"Backup created: {backupPath}");

                // Load PDF metadata using PdfFileInfo (Facades API)
                PdfFileInfo info = new PdfFileInfo(pdfPath);

                // Modify desired metadata fields
                info.Title = "Updated Title";
                info.Author = "Updated Author";
                info.Subject = "Updated Subject";

                // Save changes back to the original PDF
                info.Save(pdfPath);
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