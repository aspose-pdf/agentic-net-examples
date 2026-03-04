using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "sample.pdf";
        const string outputText = "extracted_text.txt";
        const string imagePattern = "image-{0}.png";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Use PdfExtractor facade to extract text and images
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF document
            extractor.BindPdf(inputPdf);

            // -------- Extract Text --------
            extractor.ExtractText();               // Use default Unicode encoding
            extractor.GetText(outputText);         // Save all extracted text to a file
            Console.WriteLine($"Text extracted to '{outputText}'.");

            // -------- Extract Images --------
            extractor.ExtractImage();              // Prepare image extraction
            int imgIndex = 1;
            while (extractor.HasNextImage())
            {
                string imgPath = string.Format(imagePattern, imgIndex);
                // Save each image; format inferred from file extension (.png)
                extractor.GetNextImage(imgPath);
                Console.WriteLine($"Image {imgIndex} saved to '{imgPath}'.");
                imgIndex++;
            }

            // Optional explicit close (Dispose will be called by using)
            extractor.Close();
        }
    }
}