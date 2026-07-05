using System;
using System.IO;
using Aspose.Pdf.Facades; // PdfFileEditor resides here

class Program
{
    static void Main()
    {
        // Source folder containing PDFs
        const string sourceFolder = @"C:\SourcePdfs";
        // Destination folder for processed PDFs
        const string outputFolder = @"C:\ProcessedPdfs";

        // Verify that the source folder exists before trying to enumerate files
        if (!Directory.Exists(sourceFolder))
        {
            Console.WriteLine($"Source folder '{sourceFolder}' does not exist. Operation aborted.");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the source folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(sourceFolder, "*.pdf", SearchOption.TopDirectoryOnly);

        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found in the source folder.");
            return;
        }

        // Process each file
        foreach (string inputPath in pdfFiles)
        {
            // Build the output file path preserving the original file name
            string fileName = Path.GetFileName(inputPath);
            string outputPath = Path.Combine(outputFolder, fileName);

            // Delete pages 3 and 4 using the PdfFileEditor facade
            // The Delete method safely ignores page numbers that are out of range
            PdfFileEditor editor = new PdfFileEditor();
            editor.Delete(inputPath, new int[] { 3, 4 }, outputPath);
        }

        Console.WriteLine("Page deletion completed.");
    }
}