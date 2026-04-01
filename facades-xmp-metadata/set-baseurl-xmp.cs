using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string baseUrl = "https://www.example.com/";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            PdfXmpMetadata xmp = new PdfXmpMetadata();
            xmp.BindPdf(doc);
            xmp.Add(DefaultMetadataProperties.BaseURL, baseUrl);
            xmp.Save(outputPath);
        }

        Console.WriteLine($"BaseURL set and saved to '{outputPath}'.");
    }
}