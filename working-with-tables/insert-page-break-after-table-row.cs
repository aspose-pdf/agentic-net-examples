using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "NewPageFragmentExample.pdf";

        // Create a new PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document())
        {
            // Add the first page and the first part of the table (rows 1 and 2).
            Page firstPage = doc.Pages.Add();
            Table firstPart = CreateTable(new[] {
                "Row 1, Col 1", "Row 1, Col 2",
                "Row 2, Col 1", "Row 2, Col 2"
            });
            firstPage.Paragraphs.Add(firstPart);

            // Instead of using NewPageFragment (which may be unavailable in some versions),
            // add a new page to act as a page break and place the remaining table there.
            Page secondPage = doc.Pages.Add();
            Table secondPart = CreateTable(new[] {
                "Row 3, Col 1", "Row 3, Col 2",
                "Row 4, Col 1", "Row 4, Col 2"
            });
            secondPage.Paragraphs.Add(secondPart);

            // Save the document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }

    // Helper method to create a simple 2‑column table from a flat array of cell texts.
    // The array length must be a multiple of 2 (columns) and rows are inferred.
    private static Table CreateTable(string[] cellTexts)
    {
        if (cellTexts == null) throw new ArgumentNullException(nameof(cellTexts));
        if (cellTexts.Length % 2 != 0) throw new ArgumentException("Cell count must be even (2 columns).");

        // Create a table with two columns of equal width.
        Table table = new Table
        {
            ColumnWidths = "200 200", // adjust as needed
            Border = new BorderInfo(BorderSide.All, 0.5f)
        };

        // Create rows.
        for (int i = 0; i < cellTexts.Length; i += 2)
        {
            // Add a new row to the table.
            Row row = table.Rows.Add();

            // First cell.
            Cell cell1 = new Cell();
            cell1.Paragraphs.Add(new TextFragment(cellTexts[i]));
            row.Cells.Add(cell1);

            // Second cell.
            Cell cell2 = new Cell();
            cell2.Paragraphs.Add(new TextFragment(cellTexts[i + 1]));
            row.Cells.Add(cell2);
        }

        return table;
    }
}
