using System;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class ReorderTableColumns
{
    static void Main()
    {
        const string outputPath = "reordered.pdf";

        // Desired column order (zero‑based indexes). Adjust as needed.
        int[] newOrder = new int[] { 2, 0, 1 };

        // ---------------------------------------------------------------------
        // 1. Create a sample PDF containing a simple table. This removes the
        //    external file dependency that caused the original FileNotFoundException.
        // ---------------------------------------------------------------------
        Document doc = new Document();
        Page page = doc.Pages.Add();
        Table sampleTable = new Table();
        // Define three columns.
        sampleTable.ColumnWidths = "100 100 100";
        // Keep a copy of the original column‑width definition – AbsorbedTable does not expose it.
        string originalColumnWidths = sampleTable.ColumnWidths;
        // Header row.
        Row header = new Row();
        header.Cells.Add(new Cell { Paragraphs = { new TextFragment("Header 1") } });
        header.Cells.Add(new Cell { Paragraphs = { new TextFragment("Header 2") } });
        header.Cells.Add(new Cell { Paragraphs = { new TextFragment("Header 3") } });
        sampleTable.Rows.Add(header);
        // Data rows.
        for (int i = 1; i <= 3; i++)
        {
            Row dataRow = new Row();
            dataRow.Cells.Add(new Cell { Paragraphs = { new TextFragment($"R{i}C1") } });
            dataRow.Cells.Add(new Cell { Paragraphs = { new TextFragment($"R{i}C2") } });
            dataRow.Cells.Add(new Cell { Paragraphs = { new TextFragment($"R{i}C3") } });
            sampleTable.Rows.Add(dataRow);
        }
        page.Paragraphs.Add(sampleTable);

        // ---------------------------------------------------------------------
        // 2. Absorb the table so we can manipulate its rows/cells.
        // ---------------------------------------------------------------------
        TableAbsorber absorber = new TableAbsorber();
        absorber.Visit(doc.Pages[1]);

        if (absorber.TableList.Count == 0)
        {
            Console.WriteLine("No table found in the document.");
            return;
        }

        // The first (and only) absorbed table.
        var oldTable = absorber.TableList[0];

        // ---------------------------------------------------------------------
        // 3. Build a new table with columns reordered according to newOrder.
        // ---------------------------------------------------------------------
        Table newTable = new Table();
        // Preserve original column widths (re‑order them as well).
        if (!string.IsNullOrEmpty(originalColumnWidths))
        {
            string[] widths = originalColumnWidths.Split(' ');
            List<string> reorderedWidths = new List<string>();
            foreach (int idx in newOrder)
            {
                if (idx >= 0 && idx < widths.Length)
                    reorderedWidths.Add(widths[idx]);
            }
            newTable.ColumnWidths = string.Join(" ", reorderedWidths);
        }

        foreach (AbsorbedRow absorbedRow in oldTable.RowList)
        {
            Row newRow = new Row();
            IList<AbsorbedCell> originalCells = absorbedRow.CellList;

            foreach (int srcIdx in newOrder)
            {
                if (srcIdx < 0 || srcIdx >= originalCells.Count)
                    continue;

                AbsorbedCell srcCell = originalCells[srcIdx];
                Cell newCell = new Cell();

                // Copy text fragments (preserve text; styling can be extended if needed).
                foreach (TextFragment tf in srcCell.TextFragments)
                {
                    newCell.Paragraphs.Add(new TextFragment(tf.Text));
                }

                newRow.Cells.Add(newCell);
            }

            newTable.Rows.Add(newRow);
        }

        // ---------------------------------------------------------------------
        // 4. Replace the original table with the reordered one.
        // ---------------------------------------------------------------------
        absorber.Replace(doc.Pages[1], oldTable, newTable);

        // Save the result.
        doc.Save(outputPath);
        Console.WriteLine($"Table columns reordered and saved to '{outputPath}'.");
    }
}
