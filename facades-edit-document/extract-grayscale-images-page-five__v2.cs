using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPrefix = "image_";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document document = new Document(inputPath))
        {
            // Verify that page 5 exists
            if (document.Pages.Count < 5)
            {
                Console.Error.WriteLine("The document has less than 5 pages.");
                return;
            }

            // Convert page 5 to grayscale
            Page pageFive = document.Pages[5];
            pageFive.MakeGrayscale();

            // Extract images from page 5 only
            PdfConverter converter = new PdfConverter();
            converter.BindPdf(document);
            converter.StartPage = 5;
            converter.EndPage = 5;
            converter.DoConvert();

            int imageIndex = 1;
            while (converter.HasNextImage())
            {
                string outputFile = outputPrefix + imageIndex + ".jpg";
                converter.GetNextImage(outputFile, ImageFormat.Jpeg);
                Console.WriteLine($"Saved grayscale image: {outputFile}");
                imageIndex++;
            }
        }
    }
}