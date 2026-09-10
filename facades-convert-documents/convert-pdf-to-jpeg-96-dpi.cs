using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices; // required for Resolution

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "Images";

        // Verify source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // PdfConverter implements IDisposable, so use a using block
        using (PdfConverter converter = new PdfConverter())
        {
            // Load the PDF document into the converter
            converter.BindPdf(inputPdf);

            // Set desired resolution (96 DPI) for image conversion.
            // PdfConverter.Resolution expects an Aspose.Pdf.Devices.Resolution object.
            converter.Resolution = new Resolution(96);

            // Prepare the converter for image extraction
            converter.DoConvert();

            int pageNumber = 1;
            // Extract each page as a JPEG image (default format inferred from file extension)
            while (converter.HasNextImage())
            {
                string outputPath = Path.Combine(outputFolder, $"page_{pageNumber}.jpg");
                converter.GetNextImage(outputPath);
                pageNumber++;
            }

            // Release resources held by the converter (also done by using)
            converter.Close();
        }
    }
}