using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

class Program
{
    // Entry point – optionally receives the repository path as a command‑line argument.
    static async Task Main(string[] args)
    {
        string repositoryPath = args.Length > 0 ? args[0] : @"C:\PdfRepository";
        await RefreshCreatorToolValuesAsync(repositoryPath);
    }

    // Refreshes CreatorTool‑related metadata for every PDF in the specified folder.
    static async Task RefreshCreatorToolValuesAsync(string folderPath)
    {
        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder not found: {folderPath}");
            return;
        }

        // Find all PDFs recursively.
        string[] pdfFiles = Directory.GetFiles(folderPath, "*.pdf", SearchOption.AllDirectories);

        foreach (string pdfFile in pdfFiles)
        {
            try
            {
                // PdfFileInfo is a Facade class that allows editing document metadata.
                // It implements IDisposable, so we wrap it in a using block (document‑disposal rule).
                using (PdfFileInfo info = new PdfFileInfo(pdfFile))
                {
                    // Update the metadata fields that represent the CreatorTool values.
                    info.Creator = "MyApp CreatorTool";          // CreatorTool identifier
                    info.Author  = "Automated Process";          // Example author
                    info.Title   = Path.GetFileNameWithoutExtension(pdfFile); // Use file name as title

                    // Save the updated metadata back to the same file.
                    // SaveNewInfo overwrites the original PDF with the new metadata.
                    info.SaveNewInfo(pdfFile);
                }

                Console.WriteLine($"Metadata refreshed: {pdfFile}");
            }
            catch (Exception ex)
            {
                // Log any errors but continue processing other files.
                Console.Error.WriteLine($"Error processing '{pdfFile}': {ex.Message}");
            }
        }

        await Task.CompletedTask; // Placeholder for async compatibility.
    }
}