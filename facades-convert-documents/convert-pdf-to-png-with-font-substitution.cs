using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Text; // Needed for font substitution

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "output_images";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Enable font substitution for any missing fonts by adding a fallback substitution.
            // This will replace any font that cannot be found with Arial.
            FontRepository.Substitutions.Add(new SimpleFontSubstitution("*", "Arial"));

            // Initialize the PdfConverter with the loaded document
            using (PdfConverter converter = new PdfConverter(doc))
            {
                // Set the page range to convert (all pages)
                converter.StartPage = 1;
                converter.EndPage   = doc.Pages.Count;

                // Set a higher resolution for better image quality
                converter.Resolution = new Resolution(300);

                // Prepare the converter for processing
                converter.DoConvert();

                // Iterate through each page and save it as a PNG image
                for (int page = converter.StartPage; page <= converter.EndPage; page++)
                {
                    string outputPath = Path.Combine(outputDir, $"page_{page}.png");
                    // Save the current page as PNG
                    converter.GetNextImage(outputPath, ImageFormat.Png);
                }

                // Release resources held by the converter
                converter.Close();
            }
        }

        Console.WriteLine("PDF has been successfully converted to PNG images.");
    }
}
