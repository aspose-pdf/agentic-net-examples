using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Local PDF file containing the AcroForm
        const string pdfPath = @"C:\Input\sample.pdf";

        // Destination UNC path on the network share where the XFDF (XML) will be saved
        const string xfdfPath = @"\\NetworkShare\AcroFormData\sample.xfdf";

        // Verify the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        try
        {
            // Load the PDF document (wrapped in using for deterministic disposal)
            using (Document pdfDoc = new Document(pdfPath))
            {
                // Export all form annotations (AcroForm data) to an XFDF file.
                // XFDF is an XML representation of the form fields.
                pdfDoc.ExportAnnotationsToXfdf(xfdfPath);
            }

            Console.WriteLine($"AcroForm data successfully exported to: {xfdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during export: {ex.Message}");
        }
    }
}