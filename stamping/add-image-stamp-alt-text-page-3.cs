using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string stampImagePath = "stamp.png";
        const string altText = "Company logo";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        if (!File.Exists(stampImagePath))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImagePath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Verify that the document has at least three pages
            if (doc.Pages.Count < 3)
            {
                Console.Error.WriteLine("Document does not contain a third page.");
                return;
            }

            // Create an ImageStamp from the image file
            ImageStamp imgStamp = new ImageStamp(stampImagePath);
            // Set alternative text for accessibility
            imgStamp.AlternativeText = altText;
            // Position the stamp (optional)
            imgStamp.HorizontalAlignment = HorizontalAlignment.Right;
            imgStamp.VerticalAlignment = VerticalAlignment.Top;
            // Use XIndent/YIndent instead of the non‑existent Margin property
            imgStamp.XIndent = 10f; // left offset in points
            imgStamp.YIndent = 10f; // top offset in points

            // Add the stamp to page three
            Page pageThree = doc.Pages[3];
            pageThree.AddStamp(imgStamp);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Image stamp added to page 3 and saved as '{outputPath}'.");
    }
}
