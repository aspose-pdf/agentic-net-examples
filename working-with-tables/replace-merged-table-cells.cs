using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Work with the first page (1‑based indexing)
            Aspose.Pdf.Page page = doc.Pages[1];

            // Find tables on the page
            TableAbsorber absorber = new TableAbsorber();
            absorber.Visit(page);

            // Ensure at least one table was found
            if (absorber.TableList.Count == 0)
            {
                Console.WriteLine("No tables found on the page.");
                doc.Save(outputPath);
                return;
            }

            // Get the first absorbed table (the one we will replace)
            AbsorbedTable oldTable = absorber.TableList[0];

            // Create a new table that will replace the old one
            Table newTable = new Table();

            // Iterate through each row of the absorbed table
            foreach (var absorbedRow in oldTable.RowList)
            {
                // Add a new row to the replacement table
                Row newRow = newTable.Rows.Add();

                // Iterate through each cell in the absorbed row
                foreach (AbsorbedCell absorbedCell in absorbedRow.CellList)
                {
                    // If the cell spans multiple columns, split it into separate cells
                    int span = absorbedCell.ColSpan > 1 ? absorbedCell.ColSpan : 1;

                    for (int i = 0; i < span; i++)
                    {
                        // Add a new cell to the current row
                        Cell newCell = newRow.Cells.Add();

                        // Copy the text of the original cell into the first split cell only
                        if (i == 0 && absorbedCell.TextFragments.Count > 0)
                        {
                            // Use the first text fragment as the cell content
                            string cellText = absorbedCell.TextFragments[0].Text;
                            TextFragment tf = new TextFragment(cellText);
                            newCell.Paragraphs.Add(tf);
                        }
                    }
                }
            }

            // Replace the old table with the newly constructed table
            absorber.Replace(page, oldTable, newTable);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table with merged cells replaced and saved to '{outputPath}'.");
    }
}