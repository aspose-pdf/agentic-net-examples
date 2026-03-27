using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        string sourceFolder = "source_pdfs";
        string outputFolder = "output_pdfs";

        if (!Directory.Exists(sourceFolder))
        {
            Console.Error.WriteLine("Source folder does not exist: " + sourceFolder);
            return;
        }

        Directory.CreateDirectory(outputFolder);
        // Set the current directory to the output folder so that the output file name can be simple
        Directory.SetCurrentDirectory(outputFolder);

        string[] pdfFiles = Directory.GetFiles(sourceFolder, "*.pdf");
        foreach (string inputPath in pdfFiles)
        {
            string fileName = Path.GetFileName(inputPath);
            PdfFileEditor editor = new PdfFileEditor();
            // Delete pages 3 and 4 (1‑based indexing)
            editor.Delete(inputPath, new int[] { 3, 4 }, fileName);
        }

        Console.WriteLine("Page deletion completed.");
    }
}