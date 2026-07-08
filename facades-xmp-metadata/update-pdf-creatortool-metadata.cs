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
            Console.Error.WriteLine("Usage: <exe> <folderPath> <creatorTool>");
            return;
        }

        string folderPath = args[0];
        string creatorTool = args[1];

        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder not found: {folderPath}");
            return;
        }

        // Get all PDF files in the specified folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(folderPath, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string pdfPath in pdfFiles)
        {
            try
            {
                // Use PdfFileInfo facade to modify metadata
                using (PdfFileInfo info = new PdfFileInfo())
                {
                    info.BindPdf(pdfPath);          // Load the PDF
                    info.Creator = creatorTool;    // Update CreatorTool property
                    info.SaveNewInfo(pdfPath);     // Overwrite the original file with updated info
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