using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Text;

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

        // PdfConverter implements IDisposable, so wrap it in a using block
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the source PDF file
            converter.BindPdf(inputPdf);

            // Font substitution: replace missing Helvetica with Times New Roman
            // Use FontRepository.Substitutions with SimpleFontSubstitution
            FontRepository.Substitutions.Add(new SimpleFontSubstitution("Helvetica", "Times New Roman"));

            // Set resolution (default is 150 DPI). Resolution expects a Resolution object.
            converter.Resolution = new Resolution(150);

            // Prepare the converter
            converter.DoConvert();

            int pageNumber = 1;
            // Iterate through all pages and save each as a PNG image
            while (converter.HasNextImage())
            {
                string outPath = Path.Combine(outputDir, $"page_{pageNumber}.png");
                converter.GetNextImage(outPath, ImageFormat.Png);
                pageNumber++;
            }
        }

        Console.WriteLine("PDF to PNG conversion completed.");
    }
}
