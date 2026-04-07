using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string logoPath = "logo.png";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }
        if (!File.Exists(logoPath))
        {
            Console.Error.WriteLine($"Logo file not found: {logoPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            if (doc.Pages.Count < 3)
            {
                Console.Error.WriteLine("Document does not contain a third page.");
                return;
            }

            Aspose.Pdf.ImageStamp stamp = new Aspose.Pdf.ImageStamp(logoPath);
            stamp.Background = false;
            stamp.Opacity = 0.5f;
            stamp.HorizontalAlignment = Aspose.Pdf.HorizontalAlignment.Right;
            stamp.VerticalAlignment = Aspose.Pdf.VerticalAlignment.Bottom;

            Aspose.Pdf.Page page = doc.Pages[3];
            page.AddStamp(stamp);

            doc.Save(outputPath);
        }

        Console.WriteLine($"Image stamp added to page 3 and saved as '{outputPath}'.");
    }
}
