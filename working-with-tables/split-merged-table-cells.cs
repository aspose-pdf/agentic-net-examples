using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_split_cells.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block (lifecycle rule)
        using (Document doc = new Document(inputPath))
        {
            // Work with the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Find tables on the page
            TableAbsorber absorber = new TableAbsorber();
            absorber.Visit(page);

            // If no tables were found, just save the original document
            if (absorber.TableList.Count == 0)
            {
                doc.Save(outputPath);
                Console.WriteLine("No tables found – document saved unchanged.");
                return;
            }

            // Process the first table (as an example)
            AbsorbedTable oldTable = absorber.TableList[0];

            // Create a new table that will replace the old one
            Table newTable = new Table();

            // Iterate over each row of the absorbed table
            foreach (AbsorbedRow absorbedRow in oldTable.RowList)
            {
                // Add a new row to the new table
                Row newRow = newTable.Rows.Add();

                // Iterate over each cell in the absorbed row
                foreach (AbsorbedCell absorbedCell in absorbedRow.CellList)
                {
                    // Determine the column span of the original cell
                    int colSpan = absorbedCell.ColSpan;

                    // Extract the textual content of the cell (concatenate fragments)
                    string cellText = string.Empty;
                    foreach (TextFragment tf in absorbedCell.TextFragments)
                    {
                        cellText += tf.Text;
                    }

                    // If the cell was merged (colSpan > 1), split it into separate cells
                    if (colSpan > 1)
                    {
                        for (int i = 0; i < colSpan; i++)
                        {
                            // Add a new cell with the same text (or empty if desired)
                            Cell splitCell = newRow.Cells.Add(cellText);
                            // Ensure each split cell has a span of 1
                            splitCell.ColSpan = 1;
                        }
                    }
                    else
                    {
                        // Normal (non‑merged) cell – copy as‑is
                        Cell normalCell = newRow.Cells.Add(cellText);
                        normalCell.ColSpan = 1;
                    }
                }
            }

            // Replace the old absorbed table with the newly constructed table
            absorber.Replace(page, oldTable, newTable);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}