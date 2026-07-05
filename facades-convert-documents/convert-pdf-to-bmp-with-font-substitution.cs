using System;
using System.IO;
using System.Drawing.Imaging; // ImageFormat for BMP
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // required for font substitution

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "BmpImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Register a font substitution: replace Times New Roman with Calibri
        FontRepository.Substitutions.Add(new SimpleFontSubstitution("Times New Roman", "Calibri"));

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Initialize PdfConverter with the loaded document
            using (PdfConverter converter = new PdfConverter(doc))
            {
                // Set conversion range (first to last page)
                converter.StartPage = 1;
                converter.EndPage = doc.Pages.Count;

                // Prepare for conversion
                converter.DoConvert();

                int pageNumber = 1;
                // Iterate through pages and save each as BMP
                while (converter.HasNextImage())
                {
                    string outPath = Path.Combine(outputDir, $"page_{pageNumber}.bmp");
                    converter.GetNextImage(outPath, ImageFormat.Bmp);
                    pageNumber++;
                }

                // Release converter resources
                converter.Close();
            }
        }

        Console.WriteLine("PDF to BMP conversion completed.");
    }
}
