using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    // Entry point – can be invoked by a scheduler (e.g., Windows Task Scheduler) to run nightly
    static void Main()
    {
        // Path to the folder that contains all PDF files to be processed
        const string repositoryPath = @"C:\PdfRepository";

        // Refresh CreatorTool metadata for every PDF in the repository
        RefreshCreatorToolValues(repositoryPath);
    }

    // Refreshes the Creator metadata (and optionally other fields) for all PDFs in the given folder
    static void RefreshCreatorToolValues(string folderPath)
    {
        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder not found: {folderPath}");
            return;
        }

        // Enumerate all PDF files (non‑recursive; adjust SearchOption if subfolders are needed)
        string[] pdfFiles = Directory.GetFiles(folderPath, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string pdfPath in pdfFiles)
        {
            try
            {
                // PdfFileInfo is a Facade class that allows reading/writing document metadata.
                // It implements IDisposable, so we wrap it in a using block for deterministic cleanup.
                using (PdfFileInfo fileInfo = new PdfFileInfo(pdfPath))
                {
                    // Update the Creator field – this represents the "CreatorTool" value.
                    fileInfo.Creator = "CreatorTool v1.0";

                    // Optionally update the modification date to the current time.
                    // PdfFileInfo.ModDate expects a PDF‑date formatted string, not a DateTime.
                    fileInfo.ModDate = DateTime.Now.ToString("yyyyMMddHHmmss");

                    // Save the updated metadata back to the same file.
                    // SaveNewInfo overwrites the original file with the new metadata.
                    fileInfo.SaveNewInfo(pdfPath);
                }

                Console.WriteLine($"Updated CreatorTool for: {Path.GetFileName(pdfPath)}");
            }
            catch (Exception ex)
            {
                // Log the error but continue processing remaining files.
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}
