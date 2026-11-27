using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices; // Required for Resolution

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputTiff = "page4.tiff";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPdf))
            {
                // Initialize the PdfConverter facade
                using (PdfConverter converter = new PdfConverter())
                {
                    // Bind the loaded document to the converter
                    converter.BindPdf(doc);

                    // Set form rendering mode to Production (if the property exists in the used version)
                    converter.FormPresentationMode = FormPresentationMode.Production;

                    // Specify the page range (only page 4)
                    converter.StartPage = 4;
                    converter.EndPage   = 4;

                    // Set resolution (optional, higher value gives better quality)
                    converter.Resolution = new Resolution(300);

                    // Perform the conversion
                    converter.DoConvert();

                    // Save the result as a TIFF image
                    converter.SaveAsTIFF(outputTiff);
                }
            }

            Console.WriteLine($"Page 4 has been converted to TIFF: {outputTiff}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
