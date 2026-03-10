using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "updated_table.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Locate the first table on the first page
            TableAbsorber absorber = new TableAbsorber();
            absorber.Visit(doc.Pages[1]); // Pages are 1‑based

            if (absorber.TableList.Count == 0)
            {
                Console.WriteLine("No tables found on the first page.");
                doc.Save(outputPath); // Save unchanged document
                return;
            }

            // Get the first extracted table
            var extractedTable = absorber.TableList[0];

            // Create a new Table that will replace the extracted one
            Table newTable = new Table();

            // ----- Table appearance -----
            // Set outer border for the whole table
            newTable.Border = new BorderInfo(BorderSide.All, 1f, Aspose.Pdf.Color.Black);
            // Set default cell border
            newTable.DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Gray);
            // Example column widths (three columns, each 100 points wide)
            newTable.ColumnWidths = "100 100 100";

            // ----- Build rows and cells -----
            for (int r = 0; r < extractedTable.RowList.Count; r++)
            {
                // Add a new row to the table
                Row row = newTable.Rows.Add();

                // Optional: set a background color for the header row
                if (r == 0)
                {
                    row.BackgroundColor = Aspose.Pdf.Color.LightGray;
                }

                // Iterate through the cells of the extracted row
                for (int c = 0; c < extractedTable.RowList[r].CellList.Count; c++)
                {
                    // Retrieve the original text (if any)
                    string cellText = string.Empty;
                    var fragments = extractedTable.RowList[r].CellList[c].TextFragments;
                    if (fragments != null && fragments.Count > 0)
                    {
                        cellText = fragments[0].Text;
                    }

                    // Example modification: replace text in the second column of the first data row
                    if (r == 1 && c == 1) // zero‑based indices
                    {
                        cellText = "Updated Value";
                    }

                    // Add a new cell and insert the (possibly modified) text
                    Cell cell = row.Cells.Add();
                    cell.Paragraphs.Add(new TextFragment(cellText));

                    // Example: set a background color for a specific cell
                    if (r == 2 && c == 0)
                    {
                        cell.BackgroundColor = Aspose.Pdf.Color.LightYellow;
                    }
                }

                // ----- Merge cells example -----
                // Merge the first two cells of the header row (row index 0)
                if (r == 0 && newTable.Rows[0].Cells.Count >= 2)
                {
                    // Set column span on the first cell
                    newTable.Rows[0].Cells[0].ColSpan = 2;
                    // Optionally hide the border of the second cell (it will be visually merged)
                    newTable.Rows[0].Cells[1].IsNoBorder = true;
                }
            }

            // Replace the original table with the newly built one
            absorber.Replace(doc.Pages[1], extractedTable, newTable);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved to '{outputPath}'.");
    }
}