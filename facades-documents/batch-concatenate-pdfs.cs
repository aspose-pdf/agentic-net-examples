using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder that contains the PDF files to be merged
        string inputFolder = "pdfs";
        // Output file name (must be a simple file name)
        string outputFile = "merged.pdf";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Folder not found: {inputFolder}");
            return;
        }

        // Collect all PDF file paths in the folder
        List<string> pdfFiles = new List<string>();
        string[] allFiles = Directory.GetFiles(inputFolder, "*.pdf");
        foreach (string filePath in allFiles)
        {
            pdfFiles.Add(filePath);
        }

        if (pdfFiles.Count == 0)
        {
            Console.WriteLine("No PDF files found to concatenate.");
            return;
        }

        // Concatenate the collected PDFs into a single document
        PdfFileEditor fileEditor = new PdfFileEditor();
        bool result = fileEditor.Concatenate(pdfFiles.ToArray(), outputFile);
        if (result)
        {
            Console.WriteLine($"Successfully concatenated {pdfFiles.Count} files into '{outputFile}'.");
        }
        else
        {
            Console.Error.WriteLine("Concatenation failed.");
        }
    }
}