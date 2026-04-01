using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string logoPath = "logo.png";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(logoPath))
        {
            Console.Error.WriteLine($"Logo image not found: {logoPath}");
            return;
        }

        using (Document document = new Document(inputPath))
        {
            ImageStamp stamp = new ImageStamp(logoPath);
            stamp.HorizontalAlignment = HorizontalAlignment.Right;
            // Optional margins (points)
            stamp.RightMargin = 10;
            stamp.TopMargin = 10;

            foreach (Page page in document.Pages)
            {
                page.AddStamp(stamp);
            }

            document.Save(outputPath);
        }

        Console.WriteLine($"Stamp applied and saved to '{outputPath}'.");
    }
}
