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

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Apply font substitution: replace Helvetica with Arial
            // FontSubstitutions is not a member of Document; use FontRepository.Substitutions instead.
            FontRepository.Substitutions.Add(new SimpleFontSubstitution("Helvetica", "Arial"));

            // Initialize the PdfConverter facade
            using (PdfConverter converter = new PdfConverter())
            {
                // Bind the document to the converter
                converter.BindPdf(doc);

                // Set the page range (Aspose.Pdf uses 1‑based indexing)
                converter.StartPage = 5;
                converter.EndPage   = 7;

                // Prepare the converter
                converter.DoConvert();

                int pageNumber = 5;
                // Convert each page in the range to BMP
                while (converter.HasNextImage())
                {
                    string outputPath = Path.Combine(outputDir, $"page_{pageNumber}.bmp");
                    converter.GetNextImage(outputPath, ImageFormat.Bmp);
                    pageNumber++;
                }
            }
        }

        Console.WriteLine("Pages 5‑7 have been converted to BMP images.");
    }
}
