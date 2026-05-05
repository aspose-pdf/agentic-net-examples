using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // Required for FontRepository

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_page_numbers.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create a PageNumberStamp – default format is "#"
            PageNumberStamp pageNumberStamp = new PageNumberStamp
            {
                // Position the stamp at the bottom center of each page
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Bottom,
                // Optional: set margin from the bottom edge
                BottomMargin = 20,
                // Optional: make the stamp part of the page background (false = overlay)
                Background = false,
                // Optional: set font size (will be adjusted if AutoAdjustFontSizeToFitStampRectangle is true)
                TextState = { FontSize = 12, Font = FontRepository.FindFont("Helvetica") }
            };

            // Apply the stamp to every existing page
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(pageNumberStamp);
            }

            // Refresh pagination artifacts so they stay correct if pages are added/removed later
            doc.Pages.UpdatePagination();

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page numbers added and saved to '{outputPath}'.");
    }
}
