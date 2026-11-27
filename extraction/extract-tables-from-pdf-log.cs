using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class TableExtraction
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string logPath = "ExtractionLog.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Create a fresh log file
        using (var logWriter = new StreamWriter(logPath, false))
        {
            int totalTables = 0;

            // Load the PDF document
            using (var doc = new Document(inputPdf))
            {
                // Pages are 1‑based in Aspose.Pdf
                for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
                {
                    Page page = doc.Pages[pageNum];

                    // TableAbsorber extracts tables from the current page
                    var tableAbsorber = new TableAbsorber();
                    tableAbsorber.Visit(page); // <-- correct way to run the absorber

                    int pageTableCount = tableAbsorber.TableList.Count;
                    totalTables += pageTableCount;

                    logWriter.WriteLine($"Page {pageNum}: {pageTableCount} table{(pageTableCount == 1 ? "" : "s")} found");
                }
            }

            // Overall summary
            logWriter.WriteLine($"Total tables found: {totalTables}");
        }

        Console.WriteLine($"Extraction completed. Log written to '{logPath}'.");
    }
}
