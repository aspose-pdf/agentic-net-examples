using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Facades; // Facades namespace is referenced as required

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string summaryCsvPath = "summary.csv";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Prepare a list to hold the summary lines (CSV format)
            List<string> summaryLines = new List<string>();
            summaryLines.Add("PageNumber,RowIdentifier"); // header

            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
            {
                // Create a TableAbsorber for the current page
                TableAbsorber absorber = new TableAbsorber();

                // Extract tables on the current page
                absorber.Visit(pdfDoc.Pages[pageIndex]);

                // Process each detected table
                foreach (AbsorbedTable table in absorber.TableList)
                {
                    // Process each row of the table
                    foreach (var row in table.RowList)
                    {
                        // Assume the first cell contains the identifier we need
                        var firstCell = row.CellList[0];

                        // Concatenate all text fragments inside the cell
                        string identifier = string.Empty;
                        foreach (TextFragment fragment in firstCell.TextFragments)
                        {
                            identifier += fragment.Text;
                        }

                        // Add a line to the summary: page number and identifier
                        summaryLines.Add($"{pageIndex},{identifier}");
                    }
                }
            }

            // Write the summary to a CSV file
            File.WriteAllLines(summaryCsvPath, summaryLines);
            Console.WriteLine($"Summary report saved to '{summaryCsvPath}'.");
        }
    }
}