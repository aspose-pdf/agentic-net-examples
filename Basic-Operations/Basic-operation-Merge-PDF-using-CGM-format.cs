using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input CGM file (to be converted to PDF) and an existing PDF file.
        const string cgmPath = "input.cgm";
        const string pdfPath = "input.pdf";
        const string outputPath = "merged.pdf";

        // Verify that the source files exist.
        if (!File.Exists(cgmPath))
        {
            Console.Error.WriteLine($"Error: CGM file not found – {cgmPath}");
            return;
        }

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found – {pdfPath}");
            return;
        }

        try
        {
            // Load the CGM file as a PDF document using default load options.
            Document cgmDocument = new Document(cgmPath, new CgmLoadOptions());

            // Load the existing PDF document.
            Document pdfDocument = new Document(pdfPath);

            // Merge the PDF pages into the CGM‑derived document.
            // This adds all pages from pdfDocument to cgmDocument.
            cgmDocument.Pages.Add(pdfDocument.Pages);

            // Save the merged result.
            cgmDocument.Save(outputPath);
            Console.WriteLine($"Merged PDF saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}