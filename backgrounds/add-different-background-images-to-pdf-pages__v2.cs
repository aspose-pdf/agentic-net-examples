using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        using (Document doc = new Document())
        {
            // ---------- First page with background ----------
            Page page1 = doc.Pages.Add();
            const string bgPath1 = "background1.jpg";
            if (File.Exists(bgPath1))
            {
                BackgroundArtifact bgArtifact1 = new BackgroundArtifact();
                // Use SetImage which loads the image internally; no need to keep a stream alive.
                bgArtifact1.SetImage(bgPath1);
                page1.Artifacts.Add(bgArtifact1);
            }
            else
            {
                Console.WriteLine($"Warning: background image not found: {bgPath1}");
            }

            // ---------- Second page with a different background ----------
            Page page2 = doc.Pages.Add();
            const string bgPath2 = "background2.jpg";
            if (File.Exists(bgPath2))
            {
                BackgroundArtifact bgArtifact2 = new BackgroundArtifact();
                bgArtifact2.SetImage(bgPath2);
                page2.Artifacts.Add(bgArtifact2);
            }
            else
            {
                Console.WriteLine($"Warning: background image not found: {bgPath2}");
            }

            // Save the PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}