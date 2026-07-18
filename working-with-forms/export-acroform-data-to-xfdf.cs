using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the source PDF containing AcroForm fields
        const string pdfPath = "input.pdf";

        // UNC path to the network share where the XFDF file will be saved
        const string xfdfPath = @"\\server\share\AcroFormData.xfdf";

        // Verify that the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(pdfPath))
            {
                // Export all form annotations (AcroForm data) to an XFDF file on the network share
                pdfDoc.ExportAnnotationsToXfdf(xfdfPath);
            }

            Console.WriteLine($"AcroForm data successfully exported to: {xfdfPath}");
        }
        catch (Exception ex)
        {
            // Handle any errors that may occur during loading or export
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}