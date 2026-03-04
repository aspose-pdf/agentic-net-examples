using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string cgmPath   = "input.cgm";
        const string pdfPath   = "output.pdf";

        if (!File.Exists(cgmPath))
        {
            Console.Error.WriteLine($"File not found: {cgmPath}");
            return;
        }

        try
        {
            // Load CGM file with default load options (A4 page, 300 dpi)
            CgmLoadOptions loadOptions = new CgmLoadOptions();

            // Open the CGM file and convert it to a PDF document
            using (Document doc = new Document(cgmPath, loadOptions))
            {
                // Save the resulting PDF
                doc.Save(pdfPath);
            }

            Console.WriteLine($"CGM converted to PDF successfully: {pdfPath}");
        }
        catch (PdfException ex)
        {
            // Handles errors specific to Aspose.Pdf processing
            Console.Error.WriteLine($"PDF processing error: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Generic fallback for any other unexpected errors
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}