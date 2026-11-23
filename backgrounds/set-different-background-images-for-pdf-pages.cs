using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";
        const string img1Path = "bg1.jpg";
        const string img2Path = "bg2.jpg";
        const string img3Path = "bg3.jpg";

        if (!File.Exists(img1Path) || !File.Exists(img2Path) || !File.Exists(img3Path))
        {
            Console.Error.WriteLine("One or more background image files were not found.");
            return;
        }

        // Keep the image streams alive until the document is saved
        var streams = new List<FileStream>();

        using (Document doc = new Document())
        {
            // Add three pages (1‑based indexing)
            doc.Pages.Add(); // Page 1
            doc.Pages.Add(); // Page 2
            doc.Pages.Add(); // Page 3

            // Page 1 background image
            var fs1 = File.OpenRead(img1Path);
            streams.Add(fs1);
            var bgImg1 = new Image { ImageStream = fs1 };
            doc.Pages[1].BackgroundImage = bgImg1;

            // Page 2 background image
            var fs2 = File.OpenRead(img2Path);
            streams.Add(fs2);
            var bgImg2 = new Image { ImageStream = fs2 };
            doc.Pages[2].BackgroundImage = bgImg2;

            // Page 3 background image
            var fs3 = File.OpenRead(img3Path);
            streams.Add(fs3);
            var bgImg3 = new Image { ImageStream = fs3 };
            doc.Pages[3].BackgroundImage = bgImg3;

            // Save the PDF
            doc.Save(outputPath);
        }

        // Dispose the streams after the document has been saved
        foreach (var s in streams)
        {
            s.Dispose();
        }

        Console.WriteLine($"PDF successfully saved to '{outputPath}'.");
    }
}
