using System;
using System.IO;
using System.Drawing.Imaging; // for ImageFormat
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;               // FontRepository, SimpleFontSubstitution
using Aspose.Pdf.Devices;            // Resolution

class Program
{
    static void Main()
    {
        // Paths
        const string inputPdf = "input.pdf";
        const string outputFolder = "Images";

        // Custom font files to be added (adjust paths as needed)
        string[] customFonts = { "fonts/CustomFont1.ttf", "fonts/CustomFont2.otf" };

        // Validate input PDF
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Register custom fonts using FontRepository.Substitutions (Aspose.Pdf 26+)
        foreach (string fontPath in customFonts)
        {
            if (File.Exists(fontPath))
            {
                // Use the font file name (without extension) as the logical font name
                string fontName = Path.GetFileNameWithoutExtension(fontPath);
                FontRepository.Substitutions.Add(new SimpleFontSubstitution(fontName, fontPath));
            }
            else
            {
                Console.Error.WriteLine($"Font file not found: {fontPath}");
            }
        }

        // Load the PDF document (needed for page count)
        Document doc = new Document(inputPdf);

        // Use PdfConverter (implements IDisposable) to convert pages to PNG images
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the PDF to the converter
            converter.BindPdf(inputPdf);

            // Set the desired resolution (DPI) for rasterisation
            converter.Resolution = new Resolution(300);

            // Convert all pages
            converter.StartPage = 1;
            converter.EndPage   = doc.Pages.Count;

            // Prepare the conversion
            converter.DoConvert();

            int pageNumber = converter.StartPage;
            while (converter.HasNextImage())
            {
                string outPath = Path.Combine(outputFolder, $"page_{pageNumber}.png");
                using (FileStream fs = new FileStream(outPath, FileMode.Create))
                {
                    // Write the next rasterised page as PNG
                    converter.GetNextImage(fs, ImageFormat.Png);
                }
                pageNumber++;
            }
        }

        Console.WriteLine("PDF conversion to PNG images completed successfully.");
    }
}
