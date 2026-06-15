using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // for HorizontalAlignment and VerticalAlignment enums

class InsertRomanPageNumbers
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_roman.pdf";
        const int introPageCount = 5; // number of introductory pages to number with Roman numerals

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Ensure we have enough pages
            int totalPages = doc.Pages.Count;
            int pagesToNumber = Math.Min(introPageCount, totalPages);

            // Add Roman numeral page numbers to the first 'pagesToNumber' pages
            for (int i = 1; i <= pagesToNumber; i++)
            {
                Page page = doc.Pages[i];

                // Create a PageNumberStamp for Roman numerals
                PageNumberStamp romanStamp = new PageNumberStamp();
                romanStamp.NumberingStyle = NumberingStyle.NumeralsRomanUppercase; // I, II, III...
                romanStamp.HorizontalAlignment = HorizontalAlignment.Center;
                romanStamp.VerticalAlignment   = VerticalAlignment.Bottom;
                romanStamp.BottomMargin = 20; // distance from bottom edge

                // Apply the stamp to the current page
                page.AddStamp(romanStamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Roman numeral page numbers added. Saved to '{outputPath}'.");
    }
}