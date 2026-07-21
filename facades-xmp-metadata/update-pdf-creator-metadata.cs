using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Expect at least the folder path; optional second argument is the new Creator value.
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

        string[] pdfFiles = Directory.GetFiles(folderPath, "*.pdf", SearchOption.TopDirectoryOnly);
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found in the specified folder.");
            return;
        }

        foreach (string pdfPath in pdfFiles)
        {
            try
            {
                // Bind the PDF file using the PdfFileInfo facade.
                using (PdfFileInfo info = new PdfFileInfo(pdfPath))
                {
                    // Update the Creator property.
                    info.Creator = newCreator;

                    // Save the updated information back to the same file.
                    // SaveNewInfo overwrites the existing PDF with the modified metadata.
                    info.SaveNewInfo(pdfPath);
                }

                Console.WriteLine($"Updated Creator for: {Path.GetFileName(pdfPath)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to update '{pdfPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Processing completed.");
    }
}