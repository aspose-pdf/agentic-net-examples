using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    // Define the desired column order (zero‑based indices).
    // Example: move column 3 to first, then column 1, then column 2, etc.
    static readonly int[] DesiredColumnOrder = new int[] { 2, 0, 1 };

    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "reordered.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document.
        using (Document doc = new Document(inputPath))
        {
            // Locate tables on the first page (adjust page index as needed).
            TableAbsorber absorber = new TableAbsorber();
            absorber.Visit(doc.Pages[1]);

            if (absorber.TableList.Count == 0)
            {
                Console.WriteLine("No tables found on the page.");
                doc.Save(outputPath); // Save unchanged document.
                return;
            }

            // Work with the first detected table.
            AbsorbedTable oldTable = absorber.TableList[0];
            Page page = doc.Pages[oldTable.PageNum];

            // Create a new table that will hold the reordered columns.
            Table newTable = new Table();

            // Optional: preserve original column widths if known.
            // newTable.ColumnWidths = "100 100 100"; // adjust as required.

            // Reorder cells for each row.
            foreach (AbsorbedRow oldRow in oldTable.RowList)
            {
                // Add a new row to the new table.
                Row newRow = newTable.Rows.Add();

                // Add cells in the desired order.
                foreach (int colIndex in DesiredColumnOrder)
                {
                    // Guard against out‑of‑range indices.
                    if (colIndex < 0 || colIndex >= oldRow.CellList.Count)
                        continue;

                    var oldCell = oldRow.CellList[colIndex];

                    // Concatenate all text fragments inside the cell.
                    string cellText = string.Empty;
                    foreach (TextFragment tf in oldCell.TextFragments)
                        cellText += tf.Text;

                    // Add the cell content to the new row.
                    newRow.Cells.Add(cellText);
                }
            }

            // Replace the original table with the reordered one.
            absorber.Replace(page, oldTable, newTable);

            // Save the modified document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Reordered table saved to '{outputPath}'.");
    }
}