using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create a page number stamp with the desired format.
            // The character '#' is replaced with the current page number,
            // and a second '#' is replaced with the total page count.
            PageNumberStamp pageNumberStamp = new PageNumberStamp("Page # of #")
            {
                // Center the stamp horizontally at the bottom of each page.
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Bottom,
                BottomMargin        = 20, // distance from the bottom edge
                // Adjust font size automatically to fit the stamp rectangle.
                AutoAdjustFontSizeToFitStampRectangle = true
            };

            // Apply the stamp to every page in the document.
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                doc.Pages[i].AddStamp(pageNumberStamp);
            }

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page numbers added and saved to '{outputPath}'.");
    }
}