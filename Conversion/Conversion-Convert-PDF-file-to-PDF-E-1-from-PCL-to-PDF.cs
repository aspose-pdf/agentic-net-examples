using System;
using System.IO;
using Aspose.Pdf; // PdfFormatConversionOptions and PclLoadOptions are in this namespace

class Program
{
    static void Main()
    {
        // Generic input and output file names
        const string inputPclPath = "input.pcl";
        const string outputPdfPath = "output.pdf";

        // Verify that the source PCL file exists
        if (!File.Exists(inputPclPath))
        {
            Console.Error.WriteLine($"Error: PCL file not found at '{inputPclPath}'.");
            return;
        }

        try
        {
            // Load options for PCL files
            var pclLoadOptions = new PclLoadOptions();

            // Load the PCL file into an Aspose.Pdf Document
            using (var pdfDocument = new Document(inputPclPath, pclLoadOptions))
            {
                // Conversion options to produce a PDF/E‑1 file
                var conversionOptions = new PdfFormatConversionOptions(PdfFormat.PDF_E_1);

                // Perform the format conversion first
                pdfDocument.Convert(conversionOptions);

                // Then save the resulting PDF document
                pdfDocument.Save(outputPdfPath);
            }

            Console.WriteLine($"Conversion successful. PDF/E‑1 saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
