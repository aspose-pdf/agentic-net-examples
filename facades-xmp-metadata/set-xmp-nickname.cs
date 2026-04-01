using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string nickname = "CustomIdentifier";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            PdfXmpMetadata xmp = new PdfXmpMetadata();
            xmp.BindPdf(inputPath);
            xmp.Add(DefaultMetadataProperties.Nickname, nickname);
            xmp.Save(outputPath);
            Console.WriteLine($"Nickname set and saved to {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
