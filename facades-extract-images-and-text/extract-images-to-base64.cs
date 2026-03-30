using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        string inputPdf = "sample.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine("File not found: " + inputPdf);
            return;
        }

        PdfExtractor extractor = new PdfExtractor();
        extractor.BindPdf(inputPdf);
        extractor.ExtractImage();

        List<string> base64Images = new List<string>();
        int imageIndex = 1;
        while (extractor.HasNextImage())
        {
            using (MemoryStream imageStream = new MemoryStream())
            {
                bool success = extractor.GetNextImage(imageStream);
                if (success)
                {
                    byte[] imageBytes = imageStream.ToArray();
                    string base64 = Convert.ToBase64String(imageBytes);
                    base64Images.Add(base64);
                    Console.WriteLine("Image " + imageIndex + " extracted, size: " + imageBytes.Length + " bytes");
                }
                else
                {
                    Console.Error.WriteLine("Failed to extract image " + imageIndex);
                }
            }
            imageIndex++;
        }

        // Output the Base64 strings as a JSON array (for transmission)
        Console.WriteLine("[");
        for (int i = 0; i < base64Images.Count; i++)
        {
            string comma = (i < base64Images.Count - 1) ? "," : "";
            Console.WriteLine("  \"" + base64Images[i] + "\"" + comma);
        }
        Console.WriteLine("]");
    }
}