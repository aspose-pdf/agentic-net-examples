using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath      = "input.pdf";          // Original PDF
        const string xfdfPath     = "interaction_log.xfdf"; // Interaction log in XFDF (XML) format
        const string outputPath   = "reconstructed.pdf";   // Resulting PDF with imported behavior

        // Verify input files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine($"XFDF not found: {xfdfPath}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle rule: wrap in using)
            using (Document doc = new Document(pdfPath))
            {
                // Import annotations (including interaction data) from the XFDF file
                doc.ImportAnnotationsFromXfdf(xfdfPath);

                // Save the reconstructed PDF (lifecycle rule: save inside using)
                doc.Save(outputPath);
            }

            Console.WriteLine($"Reconstructed PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}