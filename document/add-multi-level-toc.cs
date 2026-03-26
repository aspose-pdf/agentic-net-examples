using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Insert a new page at the beginning to hold the TOC
            Page tocPage = doc.Pages.Insert(1);
            tocPage.TocInfo = new TocInfo();
            // FIX: TocInfo.Title expects a TextFragment, not a plain string
            tocPage.TocInfo.Title = new TextFragment("Table of Contents");

            // Starting vertical position for TOC entries
            float currentY = 800f;
            // Horizontal positions for different levels (indentation)
            float level1X = 50f;
            float level2X = 100f;

            // Iterate over the original pages (now shifted by one because of the inserted TOC page)
            for (int pageIndex = 2; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                // Example heading for level 1
                string level1Text = $"Chapter {pageIndex - 1}";
                TextFragment level1Fragment = new TextFragment(level1Text);
                level1Fragment.Position = new Position(level1X, currentY);
                tocPage.Paragraphs.Add(level1Fragment);

                // Link annotation for level 1 entry
                Aspose.Pdf.Rectangle level1Rect = new Aspose.Pdf.Rectangle(level1X - 5, currentY - 5, 300, currentY + 15);
                LinkAnnotation level1Link = new LinkAnnotation(tocPage, level1Rect);
                level1Link.Action = new GoToAction(doc.Pages[pageIndex]);
                tocPage.Annotations.Add(level1Link);

                currentY -= 20f;

                // Example heading for level 2 (sub‑section)
                string level2Text = $"Section {pageIndex - 1}.1";
                TextFragment level2Fragment = new TextFragment(level2Text);
                level2Fragment.Position = new Position(level2X, currentY);
                tocPage.Paragraphs.Add(level2Fragment);

                // Link annotation for level 2 entry
                Aspose.Pdf.Rectangle level2Rect = new Aspose.Pdf.Rectangle(level2X - 5, currentY - 5, 300, currentY + 15);
                LinkAnnotation level2Link = new LinkAnnotation(tocPage, level2Rect);
                level2Link.Action = new GoToAction(doc.Pages[pageIndex]);
                tocPage.Annotations.Add(level2Link);

                currentY -= 30f; // extra space before next chapter
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with TOC saved to '{outputPath}'.");
    }
}
