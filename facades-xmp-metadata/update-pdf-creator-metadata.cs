using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: folder path and the new CreatorTool value
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: UpdateCreator <folderPath> <creatorTool>");
            return;
        }

        string folderPath = args[0];
        string newCreator = args[1];

        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder not found: {folderPath}");
            return;
        }

        // Get all PDF files in the specified folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(folderPath, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string pdfFile in pdfFiles)
        {
            try
            {
                // Bind the PDF file using PdfFileInfo facade
                using (PdfFileInfo fileInfo = new PdfFileInfo())
                {
                    fileInfo.BindPdf(pdfFile);

                    // Update the Creator metadata property
                    fileInfo.Creator = newCreator;

                    // Save the updated metadata back to the same file
                    fileInfo.SaveNewInfo(pdfFile);
                }

                Console.WriteLine($"Updated Creator for: {Path.GetFileName(pdfFile)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfFile}': {ex.Message}");
            }
        }
    }
}