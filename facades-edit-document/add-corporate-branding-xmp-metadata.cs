using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

public class Program
{
    public static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input file not found: " + inputPath);
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            PdfXmpMetadata xmp = new PdfXmpMetadata();
            xmp.BindPdf(doc);
            xmp.RegisterNamespaceURI("brand", "http://example.com/brand");
            xmp.Add("brand:LogoURL", "https://example.com/logo.png");
            xmp.Add("brand:ColorHex", "#FF5733");
            xmp.Save(outputPath);
        }

        Console.WriteLine("XMP metadata added and saved to '" + outputPath + "'.");
    }
}