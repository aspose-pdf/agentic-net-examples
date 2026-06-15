using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Local PDF file containing AcroForm fields
        const string inputPdfPath = @"C:\Docs\input.pdf";

        // Destination UNC path on the network share where the XFDF (XML) will be saved
        const string outputXfdfPath = @"\\Server\Share\AcroFormData.xfdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document (using ensures proper disposal)
            using (Document pdfDocument = new Document(inputPdfPath))
            {
                // Export all AcroForm annotations (field data) to an XFDF file.
                // XFDF is an XML representation of form data.
                pdfDocument.ExportAnnotationsToXfdf(outputXfdfPath);
            }

            Console.WriteLine($"AcroForm data successfully exported to: {outputXfdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during export: {ex.Message}");
        }
    }
}