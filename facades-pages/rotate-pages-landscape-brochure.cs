using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Standard A4 dimensions (points)
            double a4Width = PageSize.A4.Width;
            double a4Height = PageSize.A4.Height;

            // Rotate each page to landscape and set A4 landscape size
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                // Use the correct Rotation enum value ("on" prefix)
                page.Rotate = Rotation.on90;
                // Set the page size for landscape orientation
                page.SetPageSize(a4Height, a4Width);
            }

            doc.Save(outputPath);
        }

        Console.WriteLine($"Landscape brochure PDF saved to '{outputPath}'.");
    }
}
