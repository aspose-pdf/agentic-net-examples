using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the source PDF containing AcroForm fields
        const string inputPdfPath = @"C:\Docs\input.pdf";

        // UNC path on the network share where the XFDF (XML) will be saved
        const string outputXfdfPath = @"\\fileserver\shared\formdata.xfdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Ensure the target directory on the network share exists
        string outputDir = Path.GetDirectoryName(outputXfdfPath);
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        try
        {
            // Load the PDF document
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Export all AcroForm annotations (XFDF) to the specified network location
                pdfDoc.ExportAnnotationsToXfdf(outputXfdfPath);
            }

            Console.WriteLine($"AcroForm data successfully exported to: {outputXfdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during export: {ex.Message}");
        }
    }
}