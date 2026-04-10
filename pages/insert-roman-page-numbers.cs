using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // for HorizontalAlignment, VerticalAlignment enums

class InsertRomanPageNumbers
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_roman.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Number of introductory pages that should use Roman numerals.
        // Adjust this value as needed for your document.
        const int introPageCount = 5;

        // Load the PDF inside a using block to ensure proper disposal.
        using (Document doc = new Document(inputPath))
        {
            // Ensure we do not exceed the actual page count.
            int pagesToNumber = Math.Min(introPageCount, doc.Pages.Count);

            // Apply Roman numeral page numbers to the first 'pagesToNumber' pages.
            for (int i = 1; i <= pagesToNumber; i++) // 1‑based indexing
            {
                // Create a PageNumberStamp. The default format "#" will be replaced by the page number.
                PageNumberStamp stamp = new PageNumberStamp();

                // Use uppercase Roman numerals (I, II, III, ...).
                stamp.NumberingStyle = NumberingStyle.NumeralsRomanUppercase;

                // Optional: start numbering at 1 (default) – can be changed via StartingNumber.
                stamp.StartingNumber = 1;

                // Position the stamp at the bottom center of the page.
                stamp.HorizontalAlignment = HorizontalAlignment.Center;
                stamp.VerticalAlignment   = VerticalAlignment.Bottom;

                // Add a small bottom margin so the number does not touch the page edge.
                stamp.BottomMargin = 20;

                // Add the stamp to the current page.
                doc.Pages[i].AddStamp(stamp);
            }

            // Save the modified PDF. The using block ensures the document is not disposed before saving.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Roman numeral page numbers added. Output saved to '{outputPath}'.");
    }
}