using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Resolve folders relative to the executable location
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;
        string sourceFolder = Path.Combine(baseDir, "InputPdfs");
        string backupFolder = Path.Combine(baseDir, "BackupPdfs");

        // Ensure both folders exist – creates them if they are missing
        Directory.CreateDirectory(sourceFolder);
        Directory.CreateDirectory(backupFolder);

        // Process each PDF file in the source folder
        foreach (string pdfPath in Directory.GetFiles(sourceFolder, "*.pdf"))
        {
            try
            {
                // Create a backup copy before any changes
                string fileName = Path.GetFileName(pdfPath);
                string backupPath = Path.Combine(backupFolder, fileName);
                File.Copy(pdfPath, backupPath, overwrite: true);

                // Modify metadata using PdfFileInfo facade (in‑place modification)
                using (PdfFileInfo info = new PdfFileInfo())
                {
                    info.BindPdf(pdfPath);
                    info.Title = "Updated Title";
                    info.Author = "Updated Author";
                    info.Subject = "Updated Subject";
                    info.Keywords = "updated,metadata";

                    bool success = info.SaveNewInfo(pdfPath);
                    if (!success)
                    {
                        Console.Error.WriteLine($"Failed to save metadata for '{pdfPath}'.");
                    }
                }

                Console.WriteLine($"Processed '{fileName}' (backup created).");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}
