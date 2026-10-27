using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing PDF files to concatenate
        const string sourceFolder = "PdfFolder";
        // Output file for the merged PDF
        const string outputPath = "merged.pdf";

        // Verify the source folder exists
        if (!Directory.Exists(sourceFolder))
        {
            Console.Error.WriteLine($"Source folder not found: {sourceFolder}");
            return;
        }

        // Collect all PDF files in the folder (non‑recursive)
        List<string> pdfFiles = new List<string>();
        foreach (string file in Directory.GetFiles(sourceFolder, "*.pdf"))
        {
            pdfFiles.Add(file);
        }

        // Ensure there is at least one PDF to process
        if (pdfFiles.Count == 0)
        {
            Console.Error.WriteLine("No PDF files found to concatenate.");
            return;
        }

        // Use PdfFileEditor (Facades API) to concatenate the PDFs
        // PdfFileEditor does NOT implement IDisposable, so no using block is required
        PdfFileEditor editor = new PdfFileEditor();

        // Concatenate all collected PDFs into a single output file
        // The Concatenate method accepts an array of source file paths
        editor.Concatenate(pdfFiles.ToArray(), outputPath);

        Console.WriteLine($"Successfully concatenated {pdfFiles.Count} PDFs into '{outputPath}'.");
    }
}