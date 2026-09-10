using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string stampPath  = "stamp.png";
        const string altText    = "Company logo for accessibility";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }

        if (!File.Exists(stampPath))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: using block)
        using (Document doc = new Document(inputPath))
        {
            // Verify that the document has at least three pages (1‑based indexing)
            if (doc.Pages.Count < 3)
            {
                Console.Error.WriteLine("The document does not contain a third page.");
                return;
            }

            // Create an ImageStamp from the image file
            ImageStamp imgStamp = new ImageStamp(stampPath);

            // Set alternative text for the stamp (accessibility)
            imgStamp.AlternativeText = altText;

            // Optional positioning – place stamp in the top‑right corner with margins
            imgStamp.HorizontalAlignment = HorizontalAlignment.Right;
            imgStamp.VerticalAlignment   = VerticalAlignment.Top;
            imgStamp.RightMargin = 20;
            imgStamp.TopMargin   = 20;

            // Add the stamp to page three
            Page pageThree = doc.Pages[3];
            pageThree.AddStamp(imgStamp);

            // Save the modified PDF (lifecycle rule: using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Image stamp with alt text added to page 3 and saved as '{outputPath}'.");
    }
}