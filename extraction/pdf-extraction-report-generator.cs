using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;
using Aspose.Pdf.Text;

class ExtractionReportGenerator
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string reportPath   = "ExtractionReport.txt";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdfPath))
            {
                // Total number of pages (1‑based indexing, but Count is zero‑based)
                int pageCount = doc.Pages.Count;

                // Total number of distinct fonts used in the document
                // Document.Fonts property does not exist – use FontUtilities instead
                Aspose.Pdf.Text.Font[] allFonts = doc.FontUtilities.GetAllFonts();
                int fontCount = allFonts?.Length ?? 0;

                // Total number of tables – only available if the PDF is tagged
                int tableCount = 0;
                if (doc.TaggedContent != null)
                {
                    ITaggedContent tagged = doc.TaggedContent;
                    // Find all TableElement instances recursively in the structure tree
                    var tables = tagged.RootElement.FindElements<TableElement>(true);
                    tableCount = tables?.Count ?? 0;
                }

                // Build the report text
                string report = $"PDF Extraction Report{Environment.NewLine}" +
                                $"----------------------{Environment.NewLine}" +
                                $"Pages processed : {pageCount}{Environment.NewLine}" +
                                $"Font count       : {fontCount}{Environment.NewLine}" +
                                $"Table count      : {tableCount}{Environment.NewLine}";

                // Write the report to a text file
                File.WriteAllText(reportPath, report);
                Console.WriteLine($"Report generated: {reportPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}
