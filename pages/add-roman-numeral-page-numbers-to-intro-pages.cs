using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_roman_numbers.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Number of introductory pages that should use Roman numerals.
        const int introPageCount = 5;

        // Load the PDF document.
        using (Document doc = new Document(inputPath))
        {
            // Ensure we do not exceed the actual page count.
            int pagesToNumber = Math.Min(introPageCount, doc.Pages.Count);

            // Create a PageNumberStamp configured for uppercase Roman numerals.
            PageNumberStamp romanStamp = new PageNumberStamp();
            romanStamp.NumberingStyle = NumberingStyle.NumeralsRomanUppercase; // I, II, III...
            romanStamp.StartingNumber = 1;                                      // Start from I
            romanStamp.HorizontalAlignment = HorizontalAlignment.Center;        // Centered horizontally
            romanStamp.VerticalAlignment   = VerticalAlignment.Bottom;         // Bottom of the page
            romanStamp.BottomMargin = 20;                                       // Space from bottom edge
            romanStamp.TextState.FontSize = 12;                                 // Font size
            romanStamp.TextState.Font = FontRepository.FindFont("Helvetica");   // Font
            romanStamp.TextState.ForegroundColor = Aspose.Pdf.Color.Black;     // Text color

            // Apply the stamp to each introductory page.
            for (int i = 1; i <= pagesToNumber; i++)
            {
                doc.Pages[i].AddStamp(romanStamp);
            }

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Roman numeral page numbers added. Saved to '{outputPath}'.");
    }
}