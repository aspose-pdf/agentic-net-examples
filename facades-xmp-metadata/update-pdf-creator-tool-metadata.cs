using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Expect at least the folder path; optional second argument is the new CreatorTool value.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: UpdateCreator <folderPath> [creatorTool]");
            return;
        }

        string folderPath = args[0];
        string newCreator = args.Length >= 2 ? args[1] : "Aspose.Pdf.Facades Utility";

        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder not found: {folderPath}");
            return;
        }

        // Get all PDF files in the specified folder (non‑recursive).
        string[] pdfFiles = Directory.GetFiles(folderPath, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string pdfPath in pdfFiles)
        {
            try
            {
                // Load the PDF metadata using the PdfFileInfo facade.
                using (PdfFileInfo info = new PdfFileInfo(pdfPath))
                {
                    // Update the Creator property.
                    info.Creator = newCreator;

                    // Save the updated metadata back to the same file.
                    // SaveNewInfo overwrites the original PDF with the modified info.
                    info.SaveNewInfo(pdfPath);
                }

                Console.WriteLine($"Updated Creator for: {Path.GetFileName(pdfPath)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Processing complete.");
    }
}