using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF files to be concatenated
        const string firstPdf  = "first.pdf";
        const string secondPdf = "second.pdf";

        // Path for the temporary blank page PDF
        const string blankPdf = "blank_page.pdf";

        // Output PDF with separator pages
        const string outputPdf = "merged_with_separators.pdf";

        // Verify input files exist
        if (!File.Exists(firstPdf) || !File.Exists(secondPdf))
        {
            Console.Error.WriteLine("One or more input PDF files are missing.");
            return;
        }

        try
        {
            // ------------------------------------------------------------
            // Step 1: Create a single-page blank PDF (used as separator)
            // ------------------------------------------------------------
            using (Document blankDoc = new Document())
            {
                // Add an empty page; no content is added
                blankDoc.Pages.Add();

                // Save the blank PDF using the standard Document.Save method
                blankDoc.Save(blankPdf);
            }

            // ------------------------------------------------------------
            // Step 2: Build the file list with blank separators inserted
            // ------------------------------------------------------------
            // For two PDFs the order is: first, blank, second
            string[] filesToMerge = new string[] { firstPdf, blankPdf, secondPdf };

            // ------------------------------------------------------------
            // Step 3: Concatenate the PDFs using PdfFileEditor
            // ------------------------------------------------------------
            PdfFileEditor editor = new PdfFileEditor();

            // Concatenate the array of files into the final output PDF
            // This uses the Concatenate(string[] inputFiles, string outputFile) method.
            bool success = editor.Concatenate(filesToMerge, outputPdf);

            if (success)
                Console.WriteLine($"Merged PDF created successfully: '{outputPdf}'");
            else
                Console.Error.WriteLine("Failed to concatenate PDFs.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            // Clean up the temporary blank PDF if desired
            if (File.Exists(blankPdf))
                File.Delete(blankPdf);
        }
    }
}