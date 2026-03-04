using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string cgmPath = "input.cgm";
        const string pdfPath = "output.pdf";

        // Verify that the source CGM file exists
        if (!File.Exists(cgmPath))
        {
            Console.Error.WriteLine($"File not found: {cgmPath}");
            return;
        }

        try
        {
            // Load the CGM file using default CgmLoadOptions.
            // The constructor Document(string, LoadOptions) handles the conversion to PDF internally.
            using (Document doc = new Document(cgmPath, new CgmLoadOptions()))
            {
                // Save the resulting PDF document.
                doc.Save(pdfPath);
            }

            Console.WriteLine($"CGM successfully converted to PDF: '{pdfPath}'");
        }
        catch (PdfException ex)
        {
            // Handles errors specific to Aspose.Pdf processing.
            Console.Error.WriteLine($"PDF processing error: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Handles any other unexpected errors.
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}