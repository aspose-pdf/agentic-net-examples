using System;
using System.IO;
using Aspose.Pdf; // Core API for PDF manipulation

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath  = "input.pdf";
        const string xfdfPath      = "annotations.xfdf";
        const string outputPdfPath = "output_with_annotations.pdf";

        // Verify files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine($"XFDF file not found: {xfdfPath}");
            return;
        }

        try
        {
            // Load the existing PDF document
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Import annotations from the XFDF file.
                // This method preserves the existing page layout and content.
                pdfDoc.ImportAnnotationsFromXfdf(xfdfPath);

                // Save the updated PDF
                pdfDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"Annotations imported successfully. Saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during import: {ex.Message}");
        }
    }
}