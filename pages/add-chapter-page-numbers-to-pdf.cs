using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // Added for FontRepository

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_chapter_numbers.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create a PageNumberStamp with custom format "Chapter #"
            // The '#' placeholder will be replaced by the actual page number.
            PageNumberStamp pageNumberStamp = new PageNumberStamp("Chapter #")
            {
                // Position the stamp at the bottom center of each page
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Bottom,
                // Optional styling
                TextState = {
                    FontSize = 12,
                    Font = FontRepository.FindFont("Helvetica"),
                    ForegroundColor = Color.Black
                },
                // Ensure the stamp is drawn on top of page content
                Background = false
            };

            // Apply the stamp to every page in the document
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(pageNumberStamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page numbers with prefix \"Chapter\" added. Saved to '{outputPath}'.");
    }
}
