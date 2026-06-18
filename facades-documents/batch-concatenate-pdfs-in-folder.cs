using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing PDF files to concatenate
        const string inputFolder = @"C:\PdfFolder";
        // Output concatenated PDF file
        const string outputFile = @"C:\PdfFolder\merged.pdf";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Folder not found: {inputFolder}");
            return;
        }

        // Get all PDF files in the folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found to concatenate.");
            return;
        }

        try
        {
            // PdfFileEditor does NOT implement IDisposable; instantiate directly
            PdfFileEditor editor = new PdfFileEditor();

            // Concatenate the array of PDF file paths into a single output file
            bool success = editor.Concatenate(pdfFiles, outputFile);

            if (success)
                Console.WriteLine($"Successfully concatenated {pdfFiles.Length} files to '{outputFile}'.");
            else
                Console.Error.WriteLine("Concatenation failed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during concatenation: {ex.Message}");
        }
    }
}