using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input files
        const string pdfPath   = "document.pdf";   // Existing PDF to merge into
        const string cgmPath   = "graphic.cgm";    // CGM file (input‑only format)
        const string outputPdf = "merged_output.pdf";

        // Verify files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }
        if (!File.Exists(cgmPath))
        {
            Console.Error.WriteLine($"CGM file not found: {cgmPath}");
            return;
        }

        try
        {
            // Load the existing PDF
            using (Document target = new Document(pdfPath))
            // Load the CGM file using CgmLoadOptions (CGM is input‑only)
            using (Document cgmDoc = new Document(cgmPath, new CgmLoadOptions()))
            {
                // Append all pages from the CGM‑derived PDF to the target PDF
                target.Pages.Add(cgmDoc.Pages);

                // Save the merged result as a PDF
                target.Save(outputPdf);
            }

            Console.WriteLine($"Merged PDF saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during merge: {ex.Message}");
        }
    }
}