using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string stampImagePath = "stamp.png";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath) || !File.Exists(stampImagePath))
        {
            Console.Error.WriteLine("Input PDF or stamp image not found.");
            return;
        }

        // Load the PDF document
        Document pdf = new Document(inputPath);

        // Determine the baseline position of the first text fragment on page 5
        // (using the PDF object model directly – avoids the external AsposePdfApi process)
        double baselineY = 0;
        double baselineX = 0;
        {
            // Pages collection is 1‑based
            Page page = pdf.Pages[5];
            TextFragment firstFragment = null;

            // Iterate over the page paragraphs until we find a TextFragment
            foreach (var paragraph in page.Paragraphs)
            {
                if (paragraph is TextFragment tf)
                {
                    firstFragment = tf;
                    break;
                }
            }

            if (firstFragment != null)
            {
                baselineY = firstFragment.Position.YIndent; // baseline (approx.)
                baselineX = firstFragment.Position.XIndent; // start of the fragment
            }
            else
            {
                Console.Error.WriteLine("No text fragments found on page 5.");
                return;
            }
        }

        // Create an image stamp and align it with the text baseline
        ImageStamp stamp = new ImageStamp(stampImagePath)
        {
            Background = false,                     // place stamp over existing content
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Bottom,
            XIndent = (float)baselineX,            // distance from the left edge
            YIndent = (float)baselineY             // distance from the bottom edge (baseline)
        };

        // Add the stamp to page 5 (pages are 1‑based)
        pdf.Pages[5].AddStamp(stamp);

        // Save the modified PDF
        pdf.Save(outputPath);

        Console.WriteLine($"Stamp repositioned and saved to '{outputPath}'.");
    }
}
