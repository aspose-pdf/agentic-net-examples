using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing the source PDF files
        const string inputFolder = "InputArchive";
        // Folder where the processed PDFs will be saved
        const string outputFolder = "OutputArchive";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string pdfPath in pdfFiles)
        {
            string fileName   = Path.GetFileName(pdfPath);
            string outputPath = Path.Combine(outputFolder, fileName);

            // Facade for content manipulation (no IDisposable)
            PdfContentEditor editor = new PdfContentEditor();

            // Load the PDF
            editor.BindPdf(pdfPath);

            // Replace every occurrence of "Confidential" with "Public"
            editor.ReplaceText("Confidential", "Public");

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine("Batch text replacement completed.");
    }
}