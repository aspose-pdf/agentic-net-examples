using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input files
        const string pdfPath = "input.pdf";
        const string cgmPath = "input.cgm";
        const string outputPath = "merged_output.pdf";

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
            // Load the existing PDF document
            Document pdfDocument = new Document(pdfPath);

            // Load the CGM file as a PDF document using CgmLoadOptions
            CgmLoadOptions cgmOptions = new CgmLoadOptions();
            Document cgmDocument = new Document(cgmPath, cgmOptions);

            // Merge the CGM‑derived pages into the original PDF
            pdfDocument.Pages.Add(cgmDocument.Pages);

            // Save the merged document
            pdfDocument.Save(outputPath);

            Console.WriteLine($"Documents merged successfully. Output saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
