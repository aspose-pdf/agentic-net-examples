using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing PDF files to concatenate
        const string inputFolder = @"C:\PdfFolder";
        // Output file path
        const string outputFile = @"C:\PdfFolder\merged.pdf";

        // Validate input folder
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Folder not found: {inputFolder}");
            return;
        }

        // Collect all PDF file paths in the folder
        List<string> pdfFiles = new List<string>();
        foreach (string filePath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            pdfFiles.Add(filePath);
        }

        // Ensure there is at least one PDF to concatenate
        if (pdfFiles.Count == 0)
        {
            Console.WriteLine("No PDF files found to concatenate.");
            return;
        }

        // Use PdfFileEditor (does not implement IDisposable) to concatenate files
        PdfFileEditor editor = new PdfFileEditor();

        // Concatenate the collected PDF files into a single output file
        bool success = editor.Concatenate(pdfFiles.ToArray(), outputFile);

        if (success)
        {
            Console.WriteLine($"Successfully concatenated {pdfFiles.Count} files into '{outputFile}'.");
        }
        else
        {
            Console.Error.WriteLine("Concatenation failed.");
        }
    }
}