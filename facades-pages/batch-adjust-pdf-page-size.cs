using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;

namespace AsposePdfBatch
{
    // Simple struct to hold page dimensions in points (1 point = 1/72 inch)
    internal struct PageDimensions
    {
        public double Width;
        public double Height;
        public PageDimensions(double width, double height)
        {
            Width = width;
            Height = height;
        }
    }

    // Helper class exposing common page sizes as constants
    internal static class StandardPageSizes
    {
        // A4  = 210mm × 297mm  → 595 × 842 points
        public static readonly PageDimensions A4 = new PageDimensions(595, 842);
        // Letter = 8.5in × 11in → 612 × 792 points
        public static readonly PageDimensions Letter = new PageDimensions(612, 792);
        // A5 = 148mm × 210mm → 420 × 595 points
        public static readonly PageDimensions A5 = new PageDimensions(420, 595);
        // Add more sizes as required
    }

    class Program
    {
        static void Main()
        {
            const string inputFolder = "InputPdfs";
            const string outputFolder = "OutputPdfs";

            if (!Directory.Exists(inputFolder))
            {
                Console.Error.WriteLine($"Input folder not found: {inputFolder}");
                return;
            }
            Directory.CreateDirectory(outputFolder);

            // -----------------------------------------------------------------
            // Configuration: for each PDF file specify the desired page size for
            // each page. The key is the file name (case‑insensitive). The value
            // is a list where the N‑th element corresponds to page N (1‑based).
            // If a file is not present in the dictionary a default of A4 is used
            // for all its pages.
            // -----------------------------------------------------------------
            var pageSizeConfig = new Dictionary<string, List<PageDimensions>>(StringComparer.OrdinalIgnoreCase)
            {
                // Example: doc1.pdf has three pages – first two A4, third Letter
                { "doc1.pdf", new List<PageDimensions> { StandardPageSizes.A4, StandardPageSizes.A4, StandardPageSizes.Letter } },

                // Example: doc2.pdf has two pages – Letter then A5
                { "doc2.pdf", new List<PageDimensions> { StandardPageSizes.Letter, StandardPageSizes.A5 } }

                // Add further entries as required
            };

            // Process each PDF in the input folder
            foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
            {
                string fileName = Path.GetFileName(pdfPath);
                string outPath = Path.Combine(outputFolder, fileName);

                // -----------------------------------------------------------------
                // Determine the list of page sizes for the current document.
                // If the document is not in the configuration we fall back to A4
                // for every page.
                // -----------------------------------------------------------------
                List<PageDimensions> targetSizes;
                if (!pageSizeConfig.TryGetValue(fileName, out targetSizes))
                {
                    // Use a temporary Document only to obtain the page count.
                    using (Document tempDoc = new Document(pdfPath))
                    {
                        targetSizes = new List<PageDimensions>();
                        for (int i = 1; i <= tempDoc.Pages.Count; i++)
                            targetSizes.Add(StandardPageSizes.A4);
                    }
                }

                // -----------------------------------------------------------------
                // Load the PDF, apply the required page dimensions and save.
                // The PageInfo object (Width / Height) is the supported way to
                // change a page size in recent Aspose.Pdf versions.
                // -----------------------------------------------------------------
                using (Document doc = new Document(pdfPath))
                {
                    int pageCount = doc.Pages.Count;
                    int applyCount = Math.Min(pageCount, targetSizes.Count);

                    for (int i = 1; i <= applyCount; i++) // 1‑based indexing for Pages collection
                    {
                        var size = targetSizes[i - 1];
                        var page = doc.Pages[i];
                        page.PageInfo.Width = size.Width;
                        page.PageInfo.Height = size.Height;
                        // Optional: set orientation flag if you want Aspose to treat the page as landscape
                        // page.PageInfo.IsLandscape = size.Width > size.Height;
                    }

                    doc.Save(outPath);
                }

                Console.WriteLine($"Processed '{fileName}' → '{outPath}'");
            }
        }
    }
}
