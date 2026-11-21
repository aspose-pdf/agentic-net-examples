using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "booklet.pdf";
        const string logPath    = "process.log";

        // Open log file for writing (overwrite any existing log)
        using (StreamWriter log = new StreamWriter(logPath, false))
        {
            log.WriteLine($"{DateTime.Now}: Batch booklet creation started.");

            if (!File.Exists(inputPath))
            {
                log.WriteLine($"{DateTime.Now}: ERROR - Input file not found: {inputPath}");
                return;
            }

            // Load the source PDF
            using (Document srcDoc = new Document(inputPath))
            {
                int pageCount = srcDoc.Pages.Count;
                log.WriteLine($"{DateTime.Now}: Loaded source PDF with {pageCount} pages.");

                // Compute a simple booklet page order:
                // Example for a single signature: last, first, second, second‑last, …
                List<int> newOrder = new List<int>();
                int left = 1;
                int right = pageCount;
                while (left < right)
                {
                    newOrder.Add(right); // back page
                    newOrder.Add(left);  // front page
                    left++;
                    right--;
                }
                if (left == right) // odd number of pages – middle page stays at end
                {
                    newOrder.Add(left);
                }

                log.WriteLine($"{DateTime.Now}: Computed new page order: {string.Join(",", newOrder)}");

                // Create a new document that will hold the booklet pages
                using (Document bookletDoc = new Document())
                {
                    foreach (int pageNumber in newOrder)
                    {
                        // Add a copy of the source page to the booklet document
                        bookletDoc.Pages.Add(srcDoc.Pages[pageNumber]);
                        log.WriteLine($"{DateTime.Now}: Added source page {pageNumber} to booklet.");
                    }

                    // Save the resulting booklet PDF
                    bookletDoc.Save(outputPath);
                    log.WriteLine($"{DateTime.Now}: Booklet PDF saved to {outputPath}.");
                }
            }

            log.WriteLine($"{DateTime.Now}: Batch booklet creation completed.");
        }
    }
}