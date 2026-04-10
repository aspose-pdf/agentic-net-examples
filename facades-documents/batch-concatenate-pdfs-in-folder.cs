using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing the PDF files to concatenate
        const string inputFolder = @"C:\PdfFolder";
        // Output file that will contain the concatenated result
        const string outputFile = @"C:\PdfFolder\Combined.pdf";

        // Verify that the input folder exists
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Folder not found: {inputFolder}");
            return;
        }

        // Retrieve all PDF files in the folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");

        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found to concatenate.");
            return;
        }

        // Create the PdfFileEditor facade (it does NOT implement IDisposable)
        PdfFileEditor editor = new PdfFileEditor();

        // Concatenate the files into the specified output file
        // The Concatenate(string[] inputFiles, string outputFile) overload handles the batch operation.
        bool success = editor.Concatenate(pdfFiles, outputFile);

        if (success)
        {
            Console.WriteLine($"Successfully concatenated {pdfFiles.Length} files into '{outputFile}'.");
        }
        else
        {
            Console.Error.WriteLine("Concatenation failed.");
        }
    }
}