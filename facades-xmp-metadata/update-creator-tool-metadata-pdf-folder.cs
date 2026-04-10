using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Expect at least the folder path argument.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: UpdateCreatorTool <folderPath> [creatorTool]");
            return;
        }

        string folderPath = args[0];
        string creatorTool = args.Length >= 2 ? args[1] : "MyCreatorTool";

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
                // Bind the existing PDF file using the PdfFileInfo facade.
                using (PdfFileInfo info = new PdfFileInfo(pdfPath))
                {
                    // Update the Creator property.
                    info.Creator = creatorTool;

                    // Save the updated metadata back to the same file.
                    // SaveNewInfo overwrites the original PDF with the new info.
                    info.SaveNewInfo(pdfPath);
                }

                Console.WriteLine($"Updated CreatorTool for: {Path.GetFileName(pdfPath)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to update '{pdfPath}': {ex.Message}");
            }
        }
    }
}