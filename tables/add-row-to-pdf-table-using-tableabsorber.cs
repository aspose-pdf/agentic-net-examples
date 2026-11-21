using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Verify that page 33 exists (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 33)
            {
                Console.Error.WriteLine("The document contains fewer than 33 pages.");
                return;
            }

            Page page = doc.Pages[33];

            // Absorb existing tables on page 33
            TableAbsorber absorber = new TableAbsorber();
            absorber.Visit(page);

            if (absorber.TableList.Count == 0)
            {
                // No table found – create a new table with one row and two cells
                Table newTable = new Table();
                newTable.ColumnWidths = "100 100"; // simple equal widths
                var row = newTable.Rows.Add();
                row.Cells.Add("Cell 1");
                row.Cells.Add("Cell 2");
                page.Paragraphs.Add(newTable);
            }
            else
            {
                // Work with the first absorbed table
                AbsorbedTable absorbedTable = absorber.TableList[0];

                // Determine column count from the first absorbed row (fallback to 2)
                int columnCount = absorbedTable.RowList.Count > 0 ? absorbedTable.RowList[0].CellList.Count : 2;
                string columnWidths = string.Join(" ", Enumerable.Repeat("100", columnCount));

                // Build a new Table based on the absorbed content
                Table newTable = new Table { ColumnWidths = columnWidths };

                // Copy existing rows
                foreach (var absRow in absorbedTable.RowList)
                {
                    var tfRow = newTable.Rows.Add();
                    foreach (var absCell in absRow.CellList)
                    {
                        // Concatenate all text fragments inside the cell
                        string cellText = string.Concat(absCell.TextFragments.Select(tf => tf.Text));
                        tfRow.Cells.Add(cellText);
                    }
                }

                // Append a new row with two cells (adjust to column count if needed)
                var addedRow = newTable.Rows.Add();
                addedRow.Cells.Add("New Cell 1");
                addedRow.Cells.Add("New Cell 2");
                // If the original table had more columns, fill the remaining cells with empty strings
                for (int i = addedRow.Cells.Count; i < columnCount; i++)
                {
                    addedRow.Cells.Add("");
                }

                // Replace the absorbed table with the newly built table
                absorber.Replace(page, absorbedTable, newTable);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
