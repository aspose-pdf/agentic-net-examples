using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Directory containing the original PDFs
        const string sourceDir = "pdfs";

        // Directory where backup copies will be stored
        const string backupDir = "pdfs_backup";

        // Ensure the backup directory exists
        Directory.CreateDirectory(backupDir);

        // Verify that the source directory exists; if not, inform the user and exit gracefully
        if (!Directory.Exists(sourceDir))
        {
            Console.Error.WriteLine($"Source directory '{sourceDir}' does not exist. No PDFs to process.");
            return;
        }

        // Retrieve all PDF files from the source directory
        string[] pdfFiles = Directory.GetFiles(sourceDir, "*.pdf");

        if (pdfFiles.Length == 0)
        {
            Console.WriteLine($"No PDF files found in '{sourceDir}'.");
            return;
        }

        foreach (string pdfPath in pdfFiles)
        {
            string fileName = Path.GetFileName(pdfPath);
            string backupPath = Path.Combine(backupDir, fileName);

            try
            {
                // Create a backup copy of the original PDF (overwrite if it already exists)
                File.Copy(pdfPath, backupPath, overwrite: true);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create backup for '{pdfPath}': {ex.Message}");
                continue; // Skip metadata update for this file
            }

            // Modify metadata using the PdfFileInfo facade
            try
            {
                using (PdfFileInfo pdfInfo = new PdfFileInfo())
                {
                    // Load the original PDF into the facade
                    pdfInfo.BindPdf(pdfPath);

                    // Set new metadata values
                    pdfInfo.Title   = $"Modified {fileName}";
                    pdfInfo.Author  = "Aspose.Pdf Example";
                    pdfInfo.Subject = "Metadata update demonstration";

                    // Save the updated PDF back to the original location
                    bool saved = pdfInfo.SaveNewInfo(pdfPath);
                    if (!saved)
                    {
                        Console.Error.WriteLine($"Failed to save updated metadata for '{pdfPath}'.");
                    }
                }

                Console.WriteLine($"Processed '{fileName}': backup created and metadata updated.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}
