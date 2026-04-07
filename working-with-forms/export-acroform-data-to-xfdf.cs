using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Local PDF file containing AcroForm fields
        const string pdfPath = @"C:\Docs\input.pdf";

        // Destination on a network share (UNC path) for the exported XFDF (XML) data
        const string xfdfPath = @"\\fileserver\shared\formdata.xfdf";

        // Verify source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {pdfPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document pdfDocument = new Document(pdfPath))
            {
                // Export all form annotations (AcroForm data) to XFDF (XML) file
                pdfDocument.ExportAnnotationsToXfdf(xfdfPath);
            }

            Console.WriteLine($"AcroForm data exported to: {xfdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during export: {ex.Message}");
        }
    }
}