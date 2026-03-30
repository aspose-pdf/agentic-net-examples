using System;
using System.IO;
using Aspose.Pdf.Facades;

public class Program
{
    public static void Main(string[] args)
    {
        string inputDirectory = args.Length > 0 ? args[0] : "/pdfs";

        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine("Input directory does not exist: " + inputDirectory);
            return;
        }

        string[] pdfFiles = Directory.GetFiles(inputDirectory, "*.pdf");
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found in: " + inputDirectory);
            return;
        }

        foreach (string pdfPath in pdfFiles)
        {
            try
            {
                using (PdfExtractor extractor = new PdfExtractor())
                {
                    extractor.BindPdf(pdfPath);

                    // Extract full text from the PDF
                    extractor.ExtractText();
                    string textFileName = Path.GetFileNameWithoutExtension(pdfPath) + "-text.txt";
                    extractor.GetText(textFileName);
                    Console.WriteLine("Extracted text to " + textFileName);

                    // Extract all images from the PDF
                    extractor.ExtractImage();
                    int imageIndex = 1;
                    while (extractor.HasNextImage())
                    {
                        string imageFileName = Path.GetFileNameWithoutExtension(pdfPath) + "-image-" + imageIndex + ".png";
                        extractor.GetNextImage(imageFileName);
                        Console.WriteLine("Extracted image to " + imageFileName);
                        imageIndex++;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Error processing " + pdfPath + ": " + ex.Message);
            }
        }
    }
}