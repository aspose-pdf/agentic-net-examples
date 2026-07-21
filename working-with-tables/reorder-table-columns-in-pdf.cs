using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    // Desired column order (zero‑based indices). Adjust as needed.
    static readonly int[] DesiredOrder = new int[] { 2, 0, 1 };

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
            // Create a TableAbsorber to locate tables.
            Aspose.Pdf.Text.TableAbsorber absorber = new Aspose.Pdf.Text.TableAbsorber();

            // Extract tables from all pages.
            absorber.Visit(doc);

            // Work on a copy of the TableList to avoid collection modification issues.
            List<Aspose.Pdf.Text.AbsorbedTable> tables = new List<Aspose.Pdf.Text.AbsorbedTable>(absorber.TableList);

            foreach (Aspose.Pdf.Text.AbsorbedTable oldTable in tables)
            {
                // The page that contains the current table.
                Page page = doc.Pages[oldTable.PageNum];

                // Create a new empty table.
                Aspose.Pdf.Table newTable = new Aspose.Pdf.Table();

                // Preserve column widths if they are available.
                // (AbsorbedTable does not expose widths directly; this step is optional.)

                // Rebuild each row with cells reordered according to DesiredOrder.
                foreach (Aspose.Pdf.Text.AbsorbedRow oldRow in oldTable.RowList)
                {
                    // Add a new row to the new table.
                    Aspose.Pdf.Row newRow = newTable.Rows.Add();

                    // Ensure the desired order does not exceed the original column count.
                    int columnCount = oldRow.CellList.Count;
                    foreach (int srcIndex in DesiredOrder)
                    {
                        if (srcIndex < 0 || srcIndex >= columnCount)
                            continue; // Skip invalid indices.

                        // Get the source cell.
                        Aspose.Pdf.Text.AbsorbedCell srcCell = oldRow.CellList[srcIndex];

                        // Create a new cell for the reordered table.
                        Aspose.Pdf.Cell newCell = new Aspose.Pdf.Cell();

                        // Copy all text fragments from the source cell to the new cell.
                        foreach (TextFragment fragment in srcCell.TextFragments)
                        {
                            // Clone the fragment to preserve formatting.
                            TextFragment cloned = (TextFragment)fragment.Clone();
                            newCell.Paragraphs.Add(cloned);
                        }

                        // Add the new cell to the row.
                        newRow.Cells.Add(newCell);
                    }
                }

                // Replace the old table with the newly built table on the same page.
                absorber.Replace(page, oldTable, newTable);
            }

            // Save the modified document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Reordered PDF saved to '{outputPath}'.");
    }
}