using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // required for font substitution

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "output_images";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Substitute missing Helvetica with Times New Roman
            // Use FontRepository.Substitutions with SimpleFontSubstitution (Aspose.Pdf.Text namespace)
            FontRepository.Substitutions.Add(new SimpleFontSubstitution("Helvetica", "Times New Roman"));

            // Initialize the PDF converter facade
            using (PdfConverter converter = new PdfConverter(doc))
            {
                // Optional: set rendering resolution (default is 150 DPI)
                var renderingOptions = new RenderingOptions();
                // renderingOptions.Resolution = 150; // Uncomment if the property exists in your version.
                converter.RenderingOptions = renderingOptions;

                // Convert all pages
                converter.StartPage = 1;
                converter.EndPage   = doc.Pages.Count;
                converter.DoConvert();

                int pageNumber = 1;
                // Extract each page as a PNG image
                while (converter.HasNextImage())
                {
                    string outPath = Path.Combine(outputDir, $"page_{pageNumber}.png");
                    // Save the next image as PNG
                    converter.GetNextImage(outPath, ImageFormat.Png);
                    pageNumber++;
                }

                // Release resources held by the converter
                converter.Close();
            }
        }

        Console.WriteLine($"PDF pages have been converted to PNG images in '{outputDir}'.");
    }
}
