using System;
using System.IO;
using Aspose.Pdf.Facades;

public class Program
{
    public static void Main()
    {
        string inputPdf = "input.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine("File not found: " + inputPdf);
            return;
        }

        PdfExtractor extractor = new PdfExtractor();
        extractor.BindPdf(inputPdf);
        extractor.ExtractImage();

        while (extractor.HasNextImage())
        {
            string guid = Guid.NewGuid().ToString();
            string outputFile = guid + ".png";
            bool success = extractor.GetNextImage(outputFile);
            if (success)
            {
                Console.WriteLine("Extracted image to " + outputFile);
            }
            else
            {
                Console.Error.WriteLine("Failed to extract image for GUID " + guid);
            }
        }
    }
}