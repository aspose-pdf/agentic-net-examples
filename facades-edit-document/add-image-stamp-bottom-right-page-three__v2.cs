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
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(logoPath))
        {
            Console.Error.WriteLine($"Logo image not found: {logoPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least three pages
            if (doc.Pages.Count < 3)
            {
                Console.Error.WriteLine("The PDF does not contain a third page.");
                return;
            }

            Page pageThree = doc.Pages[3]; // 1‑based indexing

            ImageStamp logoStamp = new ImageStamp(logoPath);
            logoStamp.Background = false;
            logoStamp.Opacity = 0.8f;
            logoStamp.HorizontalAlignment = HorizontalAlignment.Right;
            logoStamp.VerticalAlignment = VerticalAlignment.Bottom;

            pageThree.AddStamp(logoStamp);

            doc.Save(outputPath);
        }

        Console.WriteLine($"Image stamp added and saved to '{outputPath}'.");
    }
}