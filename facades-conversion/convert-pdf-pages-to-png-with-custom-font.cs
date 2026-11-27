using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;
using Aspose.Pdf;
using Aspose.Pdf.Text; // FontRepository and SimpleFontSubstitution

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "Pages";
        const string customFontPath = "MyFont.ttf"; // path to a custom TrueType font

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        try
        {
            // PdfConverter implements IDisposable – wrap it in a using block
            using (PdfConverter converter = new PdfConverter())
            {
                // Bind the source PDF document
                converter.BindPdf(inputPdfPath);

                // Set the page range to convert (pages 1 through 7)
                converter.StartPage = 1;
                converter.EndPage   = 7;

                // Optional: set resolution (higher value = better quality)
                converter.Resolution = new Resolution(150);

                // Register custom fonts if the file exists
                if (File.Exists(customFontPath))
                {
                    // Use a substitution so the custom font is used when a matching font name is requested
                    // "*" means the substitution applies to any missing font name.
                    FontRepository.Substitutions.Add(new SimpleFontSubstitution("*", customFontPath));
                }

                // Prepare the conversion – this populates the internal image queue
                converter.DoConvert();

                // Export each page in the selected range as a PNG image
                int pageNumber = converter.StartPage;
                while (converter.HasNextImage())
                {
                    string outputPath = Path.Combine(outputFolder, $"page_{pageNumber}.png");
                    converter.GetNextImage(outputPath, ImageFormat.Png);
                    Console.WriteLine($"Page {pageNumber} saved as PNG: {outputPath}");
                    pageNumber++;
                }
            }

            Console.WriteLine("Conversion completed successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
