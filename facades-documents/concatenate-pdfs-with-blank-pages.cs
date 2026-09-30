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
        // Output file path
        const string outputPdf = "merged_with_blank.pdf";
        // Temporary blank PDF file path
        const string blankPdf = "blank_temp.pdf";

        // -----------------------------------------------------------------
        // Create a temporary PDF that contains a single blank page.
        // This file will be inserted between each source document.
        // -----------------------------------------------------------------
        using (Document blankDoc = new Document())
        {
            // Add an empty page to the document
            blankDoc.Pages.Add();
            // Save the blank PDF to a temporary file
            blankDoc.Save(blankPdf);
        }

        // -----------------------------------------------------------------
        // Build the ordered list of files to merge:
        //   source1.pdf, blank.pdf, source2.pdf, blank.pdf, source3.pdf
        // -----------------------------------------------------------------
        List<string> filesToMerge = new List<string>();
        for (int i = 0; i < inputPdfs.Length; i++)
        {
            if (File.Exists(inputPdfs[i]))
            {
                filesToMerge.Add(inputPdfs[i]);
            }
            else
            {
                Console.Error.WriteLine($"File not found: {inputPdfs[i]}");
                continue; // skip missing file
            }

            // Insert a blank page after each document except the last one
            if (i < inputPdfs.Length - 1)
            {
                filesToMerge.Add(blankPdf);
            }
        }

        // -----------------------------------------------------------------
        // Concatenate the PDFs using Aspose.Pdf.Facades.PdfFileEditor.
        // PdfFileEditor does NOT implement IDisposable, so no using block.
        // -----------------------------------------------------------------
        PdfFileEditor editor = new PdfFileEditor();
        editor.Concatenate(filesToMerge.ToArray(), outputPdf);

        // -----------------------------------------------------------------
        // Clean up the temporary blank PDF.
        // -----------------------------------------------------------------
        if (File.Exists(blankPdf))
        {
            File.Delete(blankPdf);
        }

        Console.WriteLine($"Merged PDF saved to '{outputPdf}'.");
    }
}