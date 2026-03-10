using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Facades; // Included as requested; not directly used in this example

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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Choose the page to edit (1‑based indexing)
            int pageNumber = 1;
            Page page = doc.Pages[pageNumber];

            // Locate tables on the selected page
            TableAbsorber absorber = new TableAbsorber();
            absorber.Visit(page);

            // Proceed only if at least one table was found
            if (absorber.TableList.Count > 0)
            {
                // Get the first detected table
                var absorbedTable = absorber.TableList[0];

                // -------------------------------------------------
                // Example 1: Edit the text of the first cell
                // -------------------------------------------------
                if (absorbedTable.RowList.Count > 0 &&
                    absorbedTable.RowList[0].CellList.Count > 0 &&
                    absorbedTable.RowList[0].CellList[0].TextFragments.Count > 0)
                {
                    // Change the text of the first text fragment in the cell
                    absorbedTable.RowList[0].CellList[0].TextFragments[0].Text = "Edited cell text";
                }

                // -------------------------------------------------
                // Example 2: Replace the entire table with a new one
                // -------------------------------------------------
                // Create a simple 2 × 2 table
                Table newTable = new Table
                {
                    // Define column widths (percentage values)
                    ColumnWidths = "50 50",
                    // Optional visual styling
                    Border = new BorderInfo(BorderSide.All, 1, Aspose.Pdf.Color.Black)
                };

                // Add header row
                Row header = newRow(newTable);
                header.Cells.Add("Header 1");
                header.Cells.Add("Header 2");

                // Add data row
                Row data = newRow(newTable);
                data.Cells.Add("Value 1");
                data.Cells.Add("Value 2");

                // Replace the original table on the same page
                absorber.Replace(page, absorbedTable, newTable);
            }
            else
            {
                Console.WriteLine("No tables were found on the specified page.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Edited PDF saved to '{outputPath}'.");
    }

    // Helper method to create a new row and add it to a table
    private static Row newRow(Table table)
    {
        Row row = table.Rows.Add();
        return row;
    }
}