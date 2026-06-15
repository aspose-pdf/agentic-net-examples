using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Directory containing PDFs to process
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
                // Create backup copy (same file name in backup folder)
                string backupPath = Path.Combine(backupFolder, Path.GetFileName(pdfPath));
                File.Copy(pdfPath, backupPath, overwrite: true);
                Console.WriteLine($"Backup created: {backupPath}");

                // Modify metadata using PdfFileInfo facade
                using (PdfFileInfo info = new PdfFileInfo(pdfPath))
                {
                    // Example metadata changes
                    info.Title = "Updated Title";
                    info.Author = "Updated Author";
                    info.Subject = "Updated Subject";
                    info.Keywords = "updated,metadata";

                    // Save the updated information back to the original file
                    bool saved = info.SaveNewInfo(pdfPath);
                    if (saved)
                        Console.WriteLine($"Metadata updated for: {pdfPath}");
                    else
                        Console.Error.WriteLine($"Failed to save metadata for: {pdfPath}");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}