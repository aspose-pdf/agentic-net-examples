using System;
using System.IO;
using Aspose.Pdf;               // Core API for Document handling
using Aspose.Pdf.Facades;      // Included as required by the task

class Program
{
    // Entry point – can be invoked by a scheduler (e.g., Windows Task Scheduler) to run nightly
    static void Main()
    {
        // Path to the folder that contains all PDF files to be processed
        const string repositoryPath = @"C:\PdfRepository";

        // The value that should be written to the Creator metadata field
        const string creatorToolValue = "MyCreatorTool v2.0";

        if (!Directory.Exists(repositoryPath))
        {
            Console.Error.WriteLine($"Repository folder not found: {repositoryPath}");
            return;
        }

        // Process every PDF file in the repository (including subfolders)
        foreach (string pdfFilePath in Directory.GetFiles(repositoryPath, "*.pdf", SearchOption.AllDirectories))
        {
            try
            {
                // Load the PDF document – using block ensures proper disposal
                using (Document pdfDoc = new Document(pdfFilePath))
                {
                    // Refresh the Creator metadata (often referred to as "CreatorTool")
                    pdfDoc.Info.Creator = creatorToolValue;

                    // Save the changes back to the original file
                    // Document.Save(string) writes a PDF regardless of the extension
                    pdfDoc.Save(pdfFilePath);
                }

                Console.WriteLine($"Successfully updated: {pdfFilePath}");
            }
            catch (Exception ex)
            {
                // Log any errors but continue processing remaining files
                Console.Error.WriteLine($"Error processing '{pdfFilePath}': {ex.Message}");
            }
        }
    }
}