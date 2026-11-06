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
        string outputPdf = "merged_with_blank.pdf";

        // Temporary blank PDF file (single blank page)
        string blankPdfPath = Path.Combine(Path.GetTempPath(), "blank_temp.pdf");

        // Create a blank PDF with one empty page
        using (Document blankDoc = new Document())
        {
            // Add an empty page
            blankDoc.Pages.Add();
            // Save to temporary location
            blankDoc.Save(blankPdfPath);
        }

        // Build the list of files to merge, inserting the blank page between each document
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

            // Add the blank page after each document except the last one
            if (i < inputPdfs.Length - 1)
            {
                filesToMerge.Add(blankPdfPath);
            }
        }

        // Concatenate using PdfFileEditor (does NOT implement IDisposable, so no using)
        PdfFileEditor editor = new PdfFileEditor();
        editor.Concatenate(filesToMerge.ToArray(), outputPdf);

        // Clean up the temporary blank PDF
        try
        {
            File.Delete(blankPdfPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Could not delete temporary file: {ex.Message}");
        }

        Console.WriteLine($"Merged PDF saved to '{outputPdf}'.");
    }
}