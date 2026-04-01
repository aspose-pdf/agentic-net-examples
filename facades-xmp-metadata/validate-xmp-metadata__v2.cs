using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "validated.pdf";
        // List of mandatory XMP keys (namespace:property)
        string[] mandatoryKeys = new string[]
        {
            "dc:title",
            "dc:creator",
            "xmp:CreateDate"
        };

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            PdfXmpMetadata xmp = new PdfXmpMetadata();
            xmp.BindPdf(doc);

            bool allPresent = true;
            foreach (string key in mandatoryKeys)
            {
                if (!xmp.ContainsKey(key))
                {
                    Console.WriteLine($"Missing mandatory XMP field: {key}");
                    allPresent = false;
                }
            }

            if (!allPresent)
            {
                Console.WriteLine("PDF cannot be published due to missing XMP metadata.");
                return;
            }

            // All required fields are present – save the PDF for publication
            doc.Save(outputPath);
            Console.WriteLine($"PDF validated and saved as '{outputPath}'.");
        }
    }
}