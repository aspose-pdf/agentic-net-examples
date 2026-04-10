using System;
using System.IO;
using System.Linq;
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
            Page page = doc.Pages[1];

            // Find tables on the page
            TableAbsorber absorber = new TableAbsorber();
            absorber.Visit(page);

            if (absorber.TableList.Count == 0)
            {
                Console.WriteLine("No tables found on the page.");
                doc.Save(outputPath);
                return;
            }

            // Process the first detected table
            AbsorbedTable oldTable = absorber.TableList[0];

            // Determine total number of columns after splitting merged cells
            int totalColumns = oldTable.RowList[0].CellList
                .Sum(c => c.ColSpan);

            // Create a new table with the same number of rows and columns
            Table newTable = new Table();

            // Simple equal column widths (adjust as needed)
            newTable.ColumnWidths = string.Join(" ", Enumerable.Repeat("100", totalColumns));

            // Iterate through each absorbed row
            foreach (AbsorbedRow absorbedRow in oldTable.RowList)
            {
                // Add a new row to the replacement table
                Row newRow = newTable.Rows.Add();

                // Iterate through each absorbed cell in the row
                foreach (AbsorbedCell absorbedCell in absorbedRow.CellList)
                {
                    // Extract the text of the cell (if any)
                    string cellText = absorbedCell.TextFragments.Count > 0
                        ? absorbedCell.TextFragments[0].Text
                        : string.Empty;

                    // If the cell spans multiple columns, split it into separate cells
                    for (int i = 0; i < absorbedCell.ColSpan; i++)
                    {
                        // For the first split part keep the original text, others are empty
                        string textForCell = i == 0 ? cellText : string.Empty;
                        newRow.Cells.Add(textForCell);
                    }
                }
            }

            // Replace the original absorbed table with the newly built table
            absorber.Replace(page, oldTable, newTable);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Merged cells have been replaced and saved to '{outputPath}'.");
    }
}