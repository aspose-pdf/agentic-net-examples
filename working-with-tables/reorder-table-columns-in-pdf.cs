using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;   // TableAbsorber and related types

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "reordered.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Desired column order (zero‑based indexes). Adjust as needed.
        // Example: for a 3‑column table, {2,0,1} will move column 3 to first,
        // column 1 to second, and column 2 to third.
        int[] newOrder = new int[] { 2, 0, 1 };

        using (Document doc = new Document(inputPath))
        {
            // Work with the first page – change if another page is required.
            Page page = doc.Pages[1];

            // Locate tables on the page.
            TableAbsorber absorber = new TableAbsorber();
            absorber.Visit(page);

            if (absorber.TableList.Count == 0)
            {
                Console.WriteLine("No tables found on the page.");
                doc.Save(outputPath);   // Save unchanged document.
                return;
            }

            // Process the first detected table.
            AbsorbedTable oldTable = absorber.TableList[0];

            // Create a new table that will hold the reordered columns.
            Table newTable = new Table();

            // Optional: copy visual settings from the old table if needed.
            // newTable.ColumnWidths = oldTable.ColumnWidths; // not available on AbsorbedTable
            // newTable.Border = oldTable.Border; // not available on AbsorbedTable

            // Reorder cells row by row.
            foreach (AbsorbedRow oldRow in oldTable.RowList)
            {
                Row newRow = newTable.Rows.Add();

                // Ensure the new order does not exceed the original column count.
                foreach (int colIdx in newOrder)
                {
                    if (colIdx < oldRow.CellList.Count)
                    {
                        // Concatenate all text fragments inside the original cell.
                        string cellText = string.Empty;
                        foreach (TextFragment tf in oldRow.CellList[colIdx].TextFragments)
                        {
                            cellText += tf.Text;
                        }

                        // Add a new cell with the extracted text.
                        newRow.Cells.Add(cellText);
                    }
                    else
                    {
                        // If the requested index is out of range, add an empty cell.
                        newRow.Cells.Add(string.Empty);
                    }
                }
            }

            // Replace the original table with the newly ordered one.
            absorber.Replace(page, oldTable, newTable);

            // Save the modified document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Reordered table saved to '{outputPath}'.");
    }
}