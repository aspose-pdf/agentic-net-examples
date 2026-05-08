using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Expect folder path as first argument, optional creator tool name as second argument.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: UpdateCreator <folderPath> [creatorTool]");
            return;
        }

        string folderPath = args[0];
        string creatorTool = args.Length > 1 ? args[1] : "MyCreatorTool";

        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder not found: {folderPath}");
            return;
        }

        // Process each PDF file in the folder (non‑recursive).
        string[] pdfFiles = Directory.GetFiles(folderPath, "*.pdf", SearchOption.TopDirectoryOnly);
        foreach (string pdfPath in pdfFiles)
        {
            try
            {
                // Load the PDF file info using the Facades API.
                using (PdfFileInfo fileInfo = new PdfFileInfo(pdfPath))
                {
                    // Update the Creator property.
                    fileInfo.Creator = creatorTool;

                    // Save the updated information back to the same file.
                    // SaveNewInfo overwrites the original PDF with the modified metadata.
                    fileInfo.SaveNewInfo(pdfPath);
                }

                Console.WriteLine($"Updated Creator for: {Path.GetFileName(pdfPath)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}