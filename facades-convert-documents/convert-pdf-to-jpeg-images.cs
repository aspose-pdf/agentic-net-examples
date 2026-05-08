using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";

        // Folder where JPEG images will be saved
        const string outputFolder = "output_images";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Use the PdfConverter facade to extract images page‑by‑page
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the PDF file to the converter
            converter.BindPdf(inputPdfPath);

            // Prepare the converter for image extraction
            converter.DoConvert();

            int pageNumber = 1;

            // Iterate over all pages; GetNextImage saves each page as a JPEG by default
            while (converter.HasNextImage())
            {
                string outputPath = Path.Combine(outputFolder, $"page_{pageNumber}.jpg");
                converter.GetNextImage(outputPath); // default format is JPEG
                pageNumber++;
            }

            // Close the converter (optional, Dispose will be called by using)
            converter.Close();
        }

        Console.WriteLine($"Conversion complete. {outputFolder} contains the JPEG images.");
    }
}