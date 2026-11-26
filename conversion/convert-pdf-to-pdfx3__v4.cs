using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_pdfx3.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Configure conversion to PDF/X-3
                PdfFormatConversionOptions conversionOptions = new PdfFormatConversionOptions(PdfFormat.PDF_X_3)
                {
                    OptimizeFileSize = true
                };

                // Convert the document
                doc.Convert(conversionOptions);

                // Save the converted PDF/X-3 file
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF successfully converted to PDF/X-3 and saved as '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}