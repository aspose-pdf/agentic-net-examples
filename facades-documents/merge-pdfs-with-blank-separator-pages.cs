using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF files to be merged
        string[] inputFiles = { "first.pdf", "second.pdf", "third.pdf" };
        string outputFile = "merged_with_separators.pdf";

        // Verify that all input files exist
        foreach (var file in inputFiles)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"Input file not found: {file}");
                return;
            }
        }

        // Create a temporary PDF that contains a single blank page.
        // This file will be inserted between each pair of input PDFs.
        string blankPagePath = Path.Combine(Path.GetTempPath(), "blank_separator.pdf");
        CreateBlankPdf(blankPagePath);

        // Build the concatenation order: pdf, blank, pdf, blank, …, last pdf (no trailing blank)
        var filesInOrder = new List<string>();
        for (int i = 0; i < inputFiles.Length; i++)
        {
            filesInOrder.Add(inputFiles[i]);
            if (i < inputFiles.Length - 1)
                filesInOrder.Add(blankPagePath);
        }

        // Use PdfFileEditor (Aspose.Pdf.Facades) to concatenate the sequence into one document.
        PdfFileEditor editor = new PdfFileEditor();
        bool result = editor.Concatenate(filesInOrder.ToArray(), outputFile);

        if (result)
            Console.WriteLine($"Successfully created merged PDF: {outputFile}");
        else
            Console.Error.WriteLine("Failed to concatenate PDFs.");

        // Clean up the temporary blank page file.
        try { File.Delete(blankPagePath); } catch { }
    }

    // Helper method that creates a PDF containing a single blank page.
    static void CreateBlankPdf(string path)
    {
        using (Document doc = new Document())
        {
            // Add an empty page (default size is A4).
            doc.Pages.Add();
            doc.Save(path);
        }
    }
}