using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document doc = new Document(inputPath);
        int pageCount = doc.Pages.Count;

        // Determine odd pages (1‑based indexing)
        int[] oddPages = Enumerable.Range(1, pageCount)
                                   .Where(p => p % 2 == 1)
                                   .ToArray();

        // Create a PageNumberStamp that will render the page number in lower‑roman style.
        // "#" is the placeholder that Aspose.Pdf replaces with the actual page number.
        PageNumberStamp pageNumberStamp = new PageNumberStamp()
        {
            Value = "#",
            NumberingStyle = NumberingStyle.NumeralsRomanLowercase,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Bottom,
            YIndent = 20, // distance from the bottom edge
            TextState =
            {
                Font = FontRepository.FindFont("Helvetica"),
                FontSize = 12,
                ForegroundColor = Color.Black
            }
        };

        // Apply the stamp only to odd pages.
        foreach (int pageNum in oddPages)
        {
            doc.Pages[pageNum].AddStamp(pageNumberStamp);
        }

        // Save the modified PDF.
        doc.Save(outputPath);
        Console.WriteLine($"Page numbers added to odd pages (lower‑roman) and saved as '{outputPath}'.");
    }
}
