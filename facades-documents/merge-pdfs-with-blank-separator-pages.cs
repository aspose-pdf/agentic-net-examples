using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF files to be concatenated
        string[] inputPdfs = { "doc1.pdf", "doc2.pdf", "doc3.pdf" };
        // Output PDF file with separator pages
        const string outputPdf = "merged_with_separators.pdf";
        // Temporary blank page PDF (will be deleted after use)
        const string blankPdf = "blank_page.pdf";

        // Verify that all input files exist
        foreach (string path in inputPdfs)
        {
            if (!File.Exists(path))
            {
                Console.Error.WriteLine($"Input file not found: {path}");
                return;
            }
        }

        // -----------------------------------------------------------------
        // Create a single-page blank PDF using the core Document API.
        // This PDF will be inserted between each pair of source PDFs.
        // -----------------------------------------------------------------
        using (Document blankDoc = new Document())
        {
            // Add an empty page (no content)
            blankDoc.Pages.Add();
            // Save the blank page to a temporary file
            blankDoc.Save(blankPdf);
        }

        // Build the sequence: source PDF, blank PDF, source PDF, ...
        List<string> filesToMerge = new List<string>();
        for (int i = 0; i < inputPdfs.Length; i++)
        {
            filesToMerge.Add(inputPdfs[i]);
            // Add a separator after every source PDF except the last one
            if (i < inputPdfs.Length - 1)
                filesToMerge.Add(blankPdf);
        }

        // -----------------------------------------------------------------
        // Use PdfFileEditor (Facades) to concatenate the prepared list.
        // PdfFileEditor does NOT implement IDisposable, so no using block.
        // -----------------------------------------------------------------
        PdfFileEditor editor = new PdfFileEditor();
        editor.Concatenate(filesToMerge.ToArray(), outputPdf);

        // Clean up the temporary blank page file
        try { File.Delete(blankPdf); } catch { /* ignore cleanup errors */ }

        Console.WriteLine($"Merged PDF with separators saved to '{outputPdf}'.");
    }
}