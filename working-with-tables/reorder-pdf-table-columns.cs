using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    // Desired column order – zero‑based indices of the original columns.
    // Example: {2,0,1} will move the original 3rd column to first, then 1st, then 2nd.
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

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // Find all tables in the whole document.
            TableAbsorber absorber = new TableAbsorber();
            absorber.Visit(doc);

            // Work on a copy of the TableList because Replace modifies the collection.
            var tablesCopy = new System.Collections.Generic.List<AbsorbedTable>(absorber.TableList);

            foreach (AbsorbedTable absorbedTable in tablesCopy)
            {
                // Create a new Table that will replace the absorbed one.
                Table newTable = new Table();

                // Preserve the original position. Cast double to float as Table.Left/Top expect float.
                newTable.Left = (float)absorbedTable.Rectangle.LLX;
                // Top coordinate is the upper‑right Y value.
                newTable.Top = (float)absorbedTable.Rectangle.URY;

                // Iterate over each row of the absorbed table.
                foreach (AbsorbedRow absorbedRow in absorbedTable.RowList)
                {
                    // Add a new row to the new table.
                    Row newRow = newTable.Rows.Add();

                    // Reorder cells according to DesiredColumnOrder.
                    foreach (int srcIndex in DesiredColumnOrder)
                    {
                        // Guard against out‑of‑range indices (in case the source table has fewer columns).
                        if (srcIndex < 0 || srcIndex >= absorbedRow.CellList.Count)
                            continue;

                        AbsorbedCell srcCell = absorbedRow.CellList[srcIndex];

                        // Add a new cell to the new row.
                        Cell newCell = newRow.Cells.Add();

                        // Copy all text fragments from the source cell to the new cell.
                        foreach (TextFragment fragment in srcCell.TextFragments)
                        {
                            // Preserve the text and its formatting.
                            TextFragment newFragment = new TextFragment(fragment.Text);
                            newFragment.TextState.Font = fragment.TextState.Font;
                            newFragment.TextState.FontSize = fragment.TextState.FontSize;
                            newFragment.TextState.ForegroundColor = fragment.TextState.ForegroundColor;
                            newFragment.TextState.FontStyle = fragment.TextState.FontStyle;

                            newCell.Paragraphs.Add(newFragment);
                        }
                    }
                }

                // Replace the original table with the newly built one.
                Page page = doc.Pages[absorbedTable.PageNum];
                absorber.Replace(page, absorbedTable, newTable);
            }

            // Save the modified document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Reordered table saved to '{outputPath}'.");
    }
}
