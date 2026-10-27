using System;
using System.IO;
using Aspose.Pdf;

class CreatorToolRefresher
{
    // Value to set for the Creator metadata field
    private const string NewCreatorTool = "MyCreatorTool";

    // Entry point – can be scheduled to run nightly (e.g., via Windows Task Scheduler)
    static void Main()
    {
        // Path to the repository containing PDF files
        const string repositoryPath = @"C:\PdfRepository";

        if (!Directory.Exists(repositoryPath))
        {
            Console.Error.WriteLine($"Repository folder not found: {repositoryPath}");
            return;
        }

        // Process each PDF file in the repository (including subfolders)
        foreach (string pdfFile in Directory.EnumerateFiles(repositoryPath, "*.pdf", SearchOption.AllDirectories))
        {
            try
            {
                // Load PDF document using the Document API (not Facades)
                using (Document doc = new Document(pdfFile))
                {
                    // Update the Creator metadata field
                    doc.Info.Creator = NewCreatorTool;

                    // Save changes back to the same file (overwrite)
                    doc.Save(pdfFile);
                }

                Console.WriteLine($"Updated Creator for: {pdfFile}");
            }
            catch (Exception ex)
            {
                // Log any errors but continue processing other files
                Console.Error.WriteLine($"Error processing '{pdfFile}': {ex.Message}");
            }
        }

        Console.WriteLine("CreatorTool refresh job completed.");
    }
}