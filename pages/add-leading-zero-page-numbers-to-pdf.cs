using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // needed for FontRepository and TextState

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_page_numbers.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a PageNumberStamp with a format that pads single‑digit numbers with a leading zero.
            // The '#' placeholder is replaced by the page number; "00#" ensures at least two digits.
            PageNumberStamp pageNumberStamp = new PageNumberStamp("00#");

            // Position the stamp at the bottom‑center of each page
            pageNumberStamp.HorizontalAlignment = HorizontalAlignment.Center;
            pageNumberStamp.VerticalAlignment   = VerticalAlignment.Bottom;
            pageNumberStamp.BottomMargin        = 20;

            // Styling – modify the existing TextState (read‑only property) instead of assigning a new one
            pageNumberStamp.TextState.Font = FontRepository.FindFont("Helvetica");
            pageNumberStamp.TextState.FontSize = 12;
            pageNumberStamp.TextState.ForegroundColor = Color.Gray;

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
