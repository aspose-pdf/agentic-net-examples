using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // Required for FontRepository

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "paged_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a PageNumberStamp with the desired format.
            // "Page # of #" – first # is current page, second # is total page count.
            PageNumberStamp pageNumberStamp = new PageNumberStamp("Page # of #");

            // Optional: set appearance (font, size, color)
            pageNumberStamp.TextState.Font = FontRepository.FindFont("Helvetica");
            pageNumberStamp.TextState.FontSize = 12;
            pageNumberStamp.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

            // Position the stamp at the bottom center of each page
            pageNumberStamp.HorizontalAlignment = HorizontalAlignment.Center;
            pageNumberStamp.VerticalAlignment = VerticalAlignment.Bottom;
            pageNumberStamp.BottomMargin = 20; // distance from bottom edge

            // Apply the stamp to every page in the document
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(pageNumberStamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page numbers added and saved to '{outputPath}'.");
    }
}