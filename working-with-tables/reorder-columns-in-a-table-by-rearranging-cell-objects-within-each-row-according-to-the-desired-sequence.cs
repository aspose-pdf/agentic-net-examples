using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Facades; // included as requested

class Program
{
    // Desired column order: new index -> original index
    // Example: {2, 0, 1} means column 3 becomes first, column 1 second, column 2 third
    static readonly int[] DesiredOrder = new int[] { 2, 0, 1 };

    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "reordered.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Process each page that contains tables
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Find tables on the current page
                TableAbsorber absorber = new TableAbsorber();
                absorber.Visit(page);

                // If no tables found, continue to next page
                if (absorber.TableList.Count == 0)
                    continue;

                // Work on a copy of the TableList to avoid collection modification issues
                IList<AbsorbedTable> tables = new List<AbsorbedTable>(absorber.TableList);

                foreach (AbsorbedTable oldTable in tables)
                {
                    // Create a new table that will replace the old one
                    Table newTable = new Table();

                    // Optional: copy column widths if needed (here we set equal widths)
                    // newTable.ColumnWidths = "100 100 100";

                    // Reorder cells for each row according to DesiredOrder
                    foreach (AbsorbedRow oldRow in oldTable.RowList)
                    {
                        // Create a new row in the replacement table
                        Row newRow = newRow = newRow = newRow = newRow = new Row();
                        newTable.Rows.Add(newRow);

                        // Ensure the desired order does not exceed the original cell count
                        int cellCount = oldRow.CellList.Count;
                        foreach (int targetIdx in DesiredOrder)
                        {
                            if (targetIdx < 0 || targetIdx >= cellCount)
                                continue; // skip invalid indices

                            // Extract text from the original cell
                            string cellText = string.Empty;
                            foreach (TextFragment tf in oldRow.CellList[targetIdx].TextFragments)
                            {
                                cellText += tf.Text;
                            }

                            // Add the text as a new cell in the reordered row
                            newRow.Cells.Add(cellText);
                        }
                    }

                    // Replace the old table with the newly constructed table
                    absorber.Replace(page, oldTable, newTable);
                }
            }

            // Save the modified document (save rule: explicit path, no format options needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Reordered PDF saved to '{outputPath}'.");
    }
}