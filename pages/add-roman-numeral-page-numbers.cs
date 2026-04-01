using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const int introPageCount = 3; // number of pages that should use Roman numerals

        // ------------------------------------------------------------
        // Ensure the source PDF exists. If it does not, create a simple
        // placeholder document. NOTE: In evaluation mode Aspose.PDF
        // allows a maximum of 4 elements in any collection (Pages, 
        // Annotations, Bookmarks, etc.). Therefore we create only 4
        // pages to avoid the IndexOutOfRangeException.
        // ------------------------------------------------------------
        if (!File.Exists(inputPath))
        {
            var placeholder = new Document();
            // Create 4 blank pages (the evaluation‑mode limit).
            for (int i = 0; i < 4; i++)
            {
                placeholder.Pages.Add();
            }
            placeholder.Save(inputPath);
        }

        // Load the source PDF document
        using (Document document = new Document(inputPath))
        {
            // Apply Roman‑numeral page numbers to the introductory pages.
            // The loop also respects the evaluation limit – it will never
            // attempt to process more than 4 pages.
            int maxPagesToProcess = Math.Min(introPageCount, Math.Min(4, document.Pages.Count));
            for (int pageIndex = 1; pageIndex <= maxPagesToProcess; pageIndex++)
            {
                Page page = document.Pages[pageIndex];
                PageNumberStamp stamp = new PageNumberStamp
                {
                    NumberingStyle = NumberingStyle.NumeralsRomanUppercase,
                    StartingNumber = 1,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Bottom,
                    BottomMargin = 20f
                };
                page.AddStamp(stamp);
            }

            // Save the modified PDF
            document.Save(outputPath);
        }
    }
}
