using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing PDF files to process
        string inputFolder = "pdfs";
        // Desired Creator value to set
        string creatorTool = "MyCreatorTool";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Folder not found: {inputFolder}");
            return;
        }

        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");
        foreach (string pdfPath in pdfFiles)
        {
            try
            {
                PdfFileInfo fileInfo = new PdfFileInfo();
                fileInfo.BindPdf(pdfPath);
                fileInfo.Creator = creatorTool;
                fileInfo.Save(pdfPath);
                Console.WriteLine($"Updated Creator for: {Path.GetFileName(pdfPath)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing {pdfPath}: {ex.Message}");
            }
        }

        Console.WriteLine("Processing completed.");
    }
}