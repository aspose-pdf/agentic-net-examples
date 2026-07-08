using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing the PDF files to concatenate
        const string inputFolder = "pdfs";
        // Path for the resulting merged PDF
        const string outputFile = "merged.pdf";

        // Verify the input folder exists
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Folder not found: {inputFolder}");
            return;
        }

        // Gather all PDF files in the folder using a foreach loop
        List<string> pdfFiles = new List<string>();
        foreach (string filePath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            pdfFiles.Add(filePath);
        }

        // Ensure there is at least one PDF to process
        if (pdfFiles.Count == 0)
        {
            Console.WriteLine("No PDF files found to concatenate.");
            return;
        }

        // Concatenate the collected PDFs using Aspose.Pdf.Facades.PdfFileEditor
        PdfFileEditor editor = new PdfFileEditor();
        bool result = editor.Concatenate(pdfFiles.ToArray(), outputFile);

        // Report the outcome
        if (result)
        {
            Console.WriteLine($"Successfully concatenated {pdfFiles.Count} files into '{outputFile}'.");
        }
        else
        {
            Console.Error.WriteLine("PDF concatenation failed.");
        }
    }
}