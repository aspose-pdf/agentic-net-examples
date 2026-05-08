using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

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

        // Substitute missing Helvetica with Arial
        FontRepository.Substitutions.Add(new SimpleFontSubstitution("Helvetica", "Arial"));

        using (Document doc = new Document(inputPdf))
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the PDF to the converter
            converter.BindPdf(doc);

            // Set page range (5 to 7)
            converter.StartPage = 5;
            converter.EndPage = 7;

            // Initialize conversion
            converter.DoConvert();

            int pageNumber = converter.StartPage;
            while (converter.HasNextImage())
            {
                string outPath = Path.Combine(outputDir, $"page_{pageNumber}.bmp");
                converter.GetNextImage(outPath, ImageFormat.Bmp);
                pageNumber++;
            }

            // Release converter resources
            converter.Close();
        }

        Console.WriteLine("Pages 5‑7 have been converted to BMP images.");
    }
}