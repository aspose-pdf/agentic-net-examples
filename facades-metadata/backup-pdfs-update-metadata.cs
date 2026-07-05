using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Directory containing PDFs to process
        const string sourceDir = "InputPdfs";
        // Directory where backup copies will be stored
        const string backupDir = "BackupPdfs";

        if (!Directory.Exists(sourceDir))
        {
            Console.Error.WriteLine($"Source directory not found: {sourceDir}");
            return;
        }

        // Ensure backup directory exists
        Directory.CreateDirectory(backupDir);

        // Process each PDF file in the source directory
        foreach (string pdfPath in Directory.GetFiles(sourceDir, "*.pdf"))
        {
            try
            {
                // Create backup copy
                string fileName = Path.GetFileName(pdfPath);
                string backupPath = Path.Combine(backupDir, fileName);
                File.Copy(pdfPath, backupPath, overwrite: true);

                // Modify metadata using PdfFileInfo
                using (PdfFileInfo info = new PdfFileInfo(pdfPath))
                {
                    // Example metadata changes
                    info.Title = "Updated Title";
                    info.Author = "Updated Author";
                    info.Subject = "Updated Subject";
                    info.Keywords = "updated, keywords";

                    // Save changes back to the original file
                    bool saved = info.SaveNewInfo(pdfPath);
                    if (!saved)
                    {
                        Console.Error.WriteLine($"Failed to save metadata for: {pdfPath}");
                    }
                }

                Console.WriteLine($"Processed: {fileName}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}