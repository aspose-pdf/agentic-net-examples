#nullable enable
using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF files (must exist in the working directory)
        string[] inputFiles = new string[] { "doc1.pdf", "doc2.pdf", "doc3.pdf" };
        const string blankPdfPath = "blank.pdf";
        const string outputPdfPath = "merged.pdf";

        // Create a single‑page blank PDF to be inserted between documents
        CreateBlankPdf(blankPdfPath);

        // Build the list of files to concatenate: doc, blank, doc, blank, ...
        List<string> filesToConcat = new List<string>();
        for (int i = 0; i < inputFiles.Length; i++)
        {
            string input = inputFiles[i];
            if (File.Exists(input))
            {
                filesToConcat.Add(input);
                // Add a blank page after each document except the last one
                if (i < inputFiles.Length - 1)
                {
                    filesToConcat.Add(blankPdfPath);
                }
            }
            else
            {
                Console.Error.WriteLine($"Input file not found: {input}");
                return;
            }
        }

        // Concatenate using PdfFileEditor
        PdfFileEditor editor = new PdfFileEditor();
        bool success = editor.Concatenate(filesToConcat.ToArray(), outputPdfPath);
        if (success)
        {
            Console.WriteLine($"PDFs concatenated successfully into '{outputPdfPath}'.");
        }
        else
        {
            Console.Error.WriteLine("Failed to concatenate PDFs.");
        }
    }

    // Helper method to create a one‑page blank PDF
    private static void CreateBlankPdf(string path)
    {
        using (Document blankDoc = new Document())
        {
            // Add an empty page
            blankDoc.Pages.Add();
            blankDoc.Save(path);
        }
    }
}