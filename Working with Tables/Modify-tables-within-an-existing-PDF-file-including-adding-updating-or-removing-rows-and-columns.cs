using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;          // TableAbsorber, AbsorbedTable, etc.
using Aspose.Pdf.Drawing;      // Row, Cell, Table (these are in Aspose.Pdf namespace, but drawing types are also needed for Table)

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "modified.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block (ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // -----------------------------------------------------------------
            // 1. Locate all tables in the document using TableAbsorber
            // -----------------------------------------------------------------
            TableAbsorber absorber = new TableAbsorber();
            absorber.Visit(doc);                     // extracts tables from all pages

            if (absorber.TableList.Count == 0)
            {
                Console.WriteLine("No tables found in the document.");
                doc.Save(outputPath);               // save unchanged document
                return;
            }

            // -----------------------------------------------------------------
            // 2. Update text in the first cell of the first table (example)
            // -----------------------------------------------------------------
            AbsorbedTable firstAbsorbed = absorber.TableList[0];
            // Ensure the cell has at least one text fragment
            if (firstAbsorbed.RowList.Count > 0 &&
                firstAbsorbed.RowList[0].CellList.Count > 0 &&
                firstAbsorbed.RowList[0].CellList[0].TextFragments.Count > 0)
            {
                // Change the text of the first fragment
                firstAbsorbed.RowList[0].CellList[0].TextFragments[0].Text = "Updated cell text";
            }

            // -----------------------------------------------------------------
            // 3. Add a new row to the first table
            // -----------------------------------------------------------------
            // Build a new Table instance that mirrors the existing one
            Table newTable = new Table();

            // Copy existing rows from the absorbed table into the new Table
            foreach (var absorbedRow in firstAbsorbed.RowList)
            {
                Row newRow = new Row();
                foreach (var absorbedCell in absorbedRow.CellList)
                {
                    Cell newCell = new Cell();

                    // Transfer all text fragments from the absorbed cell to the new cell
                    foreach (var fragment in absorbedCell.TextFragments)
                    {
                        // Each fragment is added as a separate paragraph
                        newCell.Paragraphs.Add(new TextFragment(fragment.Text));
                    }

                    newRow.Cells.Add(newCell);
                }
                newTable.Rows.Add(newRow);
            }

            // Create an additional row with sample data
            Row extraRow = new Row();
            // Assuming the table has the same number of columns as the first row
            int columnCount = firstAbsorbed.RowList[0].CellList.Count;
            for (int i = 0; i < columnCount; i++)
            {
                Cell extraCell = new Cell();
                extraCell.Paragraphs.Add(new TextFragment($"New row, col {i + 1}"));
                extraRow.Cells.Add(extraCell);
            }
            newTable.Rows.Add(extraRow);   // Append the new row

            // Replace the original table on the page with the modified one
            // Here we assume the table resides on the first page (index 1)
            absorber.Replace(doc.Pages[1], firstAbsorbed, newTable);

            // -----------------------------------------------------------------
            // 4. Remove the second table (if it exists) as an example of deletion
            // -----------------------------------------------------------------
            if (absorber.TableList.Count > 1)
            {
                AbsorbedTable secondAbsorbed = absorber.TableList[1];
                absorber.Remove(secondAbsorbed);
            }

            // -----------------------------------------------------------------
            // 5. Save the modified PDF
            // -----------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}