using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the source PDF, the XFDF (XML) file containing visibility states,
        // and the output PDF where the changes will be saved.
        const string pdfPath   = "source.pdf";
        const string xfdfPath  = "visibility_states.xfdf";
        const string outputPdf = "updated.pdf";

        // Verify that the input files exist.
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine($"XFDF file not found: {xfdfPath}");
            return;
        }

        try
        {
            // Load the existing PDF document.
            using (Document pdfDocument = new Document(pdfPath))
            {
                // Import annotations (including visibility settings) from the XFDF file.
                // XFDF is an XML representation of PDF annotations.
                pdfDocument.ImportAnnotationsFromXfdf(xfdfPath);

                // Save the modified PDF with the imported visibility states.
                pdfDocument.Save(outputPdf);
            }

            Console.WriteLine($"PDF updated successfully and saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing files: {ex.Message}");
        }
    }
}