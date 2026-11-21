using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing the PDF files to merge
        const string folderPath = "pdfs";
        // Path for the merged output PDF
        const string outputPath = "merged.pdf";

        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder not found: {folderPath}");
            return;
        }

        // Collect all PDF files in the folder using a foreach loop
        List<string> pdfFiles = new List<string>();
        foreach (string file in Directory.GetFiles(folderPath, "*.pdf"))
        {
            if (File.Exists(file))
                pdfFiles.Add(file);
        }

        if (pdfFiles.Count == 0)
        {
            Console.WriteLine("No PDF files found to concatenate.");
            return;
        }

        // PdfFileEditor does NOT implement IDisposable, so do NOT use a using block
        PdfFileEditor editor = new PdfFileEditor();

        // Concatenate the collected PDFs into a single document
        // Concatenate expects an array of input file paths and the output file path
        editor.Concatenate(pdfFiles.ToArray(), outputPath);

        Console.WriteLine($"Merged {pdfFiles.Count} PDFs into '{outputPath}'.");
    }
}