using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder that contains the PDF files to be merged
        const string folderPath = "pdfs";
        // Path for the resulting merged PDF
        const string outputPath = "merged.pdf";

        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder not found: {folderPath}");
            return;
        }

        // Gather all PDF files in the folder using a foreach loop
        List<string> pdfFiles = new List<string>();
        foreach (string file in Directory.GetFiles(folderPath, "*.pdf"))
        {
            pdfFiles.Add(file);
        }

        if (pdfFiles.Count == 0)
        {
            Console.WriteLine("No PDF files found to concatenate.");
            return;
        }

        // Concatenate the collected PDF files into a single document
        PdfFileEditor editor = new PdfFileEditor();
        bool success = editor.Concatenate(pdfFiles.ToArray(), outputPath);

        if (success)
            Console.WriteLine($"Successfully concatenated {pdfFiles.Count} files into '{outputPath}'.");
        else
            Console.Error.WriteLine("Concatenation failed.");
    }
}