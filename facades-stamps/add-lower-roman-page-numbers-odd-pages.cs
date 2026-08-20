using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the document.
        Document doc = new Document(inputPdf);
        int pageCount = doc.Pages.Count; // 1‑based page count

        // Iterate over odd pages only and add a lower‑case Roman numeral page number.
        for (int i = 1; i <= pageCount; i++)
        {
            if (i % 2 == 0) // skip even pages
                continue;

            // Create a PageNumberStamp that will be replaced by the page number.
            PageNumberStamp stamp = new PageNumberStamp()
            {
                // Position the stamp at the bottom‑center of the page.
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Bottom,
                // Use lower‑case Roman numerals.
                NumberingStyle = NumberingStyle.NumeralsRomanLowercase,
                // Adjust bottom margin (20 points from the bottom).
                BottomMargin = 20
            };

            // Configure the visual appearance of the stamp.
            stamp.TextState.Font = FontRepository.FindFont("Helvetica");
            stamp.TextState.FontSize = 12;
            stamp.TextState.ForegroundColor = Color.Black;

            // Add the stamp to the current page.
            doc.Pages[i].AddStamp(stamp);
        }

        // Save the modified PDF.
        doc.Save(outputPdf);
        Console.WriteLine($"Page numbers added (lower‑roman, odd pages) to '{outputPdf}'.");
    }
}
