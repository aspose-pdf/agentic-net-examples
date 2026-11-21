using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Facades namespace imported as requested

class Program
{
    // Entry point – can be invoked by a scheduler (e.g., Windows Task Scheduler) nightly
    static void Main(string[] args)
    {
        // Repository folder containing PDFs
        string repositoryPath = @"C:\PdfRepository";

        // Value to set for the Creator metadata field
        string creatorValue = "MyApplication v1.0";

        RefreshCreatorMetadata(repositoryPath, creatorValue);
    }

    // Processes all PDF files in the specified folder (including subfolders)
    static void RefreshCreatorMetadata(string folderPath, string creator)
    {
        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder not found: {folderPath}");
            return;
        }

        // Get all PDF files recursively
        string[] pdfFiles = Directory.GetFiles(folderPath, "*.pdf", SearchOption.AllDirectories);

        foreach (string pdfPath in pdfFiles)
        {
            try
            {
                // Open the PDF document inside a using block for deterministic disposal
                using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(pdfPath))
                {
                    // Set the Creator metadata (DocumentInfo.CreatorTool does not exist)
                    doc.Info.Creator = creator;

                    // Update the modification date to the current UTC time
                    doc.Info.ModDate = DateTime.UtcNow;

                    // Save changes back to the same file
                    doc.Save(pdfPath);
                }

                Console.WriteLine($"Updated Creator for: {pdfPath}");
            }
            catch (Exception ex)
            {
                // Log any errors but continue processing remaining files
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}