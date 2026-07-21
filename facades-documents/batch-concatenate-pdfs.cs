using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing the PDF files to concatenate
        const string inputFolder = @"C:\PdfFolder";
        // Output file path for the concatenated PDF
        const string outputFile = @"C:\PdfFolder\Combined.pdf";

        // Verify that the input folder exists
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Folder not found: {inputFolder}");
            return;
        }

        // Collect all PDF files in the folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");

        if (pdfFiles.Length == 0)
        {
            Console.Error.WriteLine("No PDF files found to concatenate.");
            return;
        }

        // Optional: sort the files alphabetically to define concatenation order
        Array.Sort(pdfFiles);

        // Use PdfFileEditor from Aspose.Pdf.Facades to concatenate the files
        PdfFileEditor fileEditor = new PdfFileEditor();

        // Concatenate the array of input files into a single output file
        bool success = fileEditor.Concatenate(pdfFiles, outputFile);

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