using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class InsertTableRows
{
    static void Main(string[] args)
    {
        // Input and output PDF paths (adjust as needed)
        string inputPath = "input.pdf";
        string outputPath = "output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        // Load the existing PDF document
        Document pdfDocument = new Document(inputPath);

        // Create a TableAbsorber to locate tables on the first page
        TableAbsorber absorber = new TableAbsorber();
        absorber.Visit(pdfDocument.Pages[1]); // 1‑based page index

        // Ensure at least one table was found
        if (absorber.TableList.Count == 0)
        {
            Console.WriteLine("No tables were detected on the first page.");
            // Save the original document unchanged
            pdfDocument.Save(outputPath);
            return;
        }

        // Get the first detected table (you can iterate if needed)
        AbsorbedTable absorbedTable = absorber.TableList[0];

        // Determine the number of columns from the first row of the absorbed table
        int columnCount = absorbedTable.RowList[0].CellList.Count;

        // Create a new Table that will replace the absorbed one
        Table newTable = new Table();

        // Optional: copy column widths from the original table if they exist
        // (ColumnWidths is a string, e.g., "100 150 100")
        // Here we simply set equal widths for demonstration
        string[] widths = new string[columnCount];
        for (int i = 0; i < columnCount; i++) widths[i] = "100";
        newTable.ColumnWidths = string.Join(" ", widths);

        // ------------------------------
        // Insert a new row with cells
        // ------------------------------
        // Create a new row
        Row newRow = newRow = new Row();

        // Add cells to the row
        for (int col = 0; col < columnCount; col++)
        {
            Cell cell = new Cell();
            // Add a simple text fragment to each cell
            TextFragment tf = new TextFragment($"New Cell {col + 1}");
            cell.Paragraphs.Add(tf);
            newRow.Cells.Add(cell);
        }

        // Append the new row to the table
        newTable.Rows.Add(newRow);

        // ------------------------------
        // Insert another row with merged cells (example of colspan)
        // ------------------------------
        Row mergedRow = new Row();

        // Create a cell that spans all columns
        Cell mergedCell = new Cell
        {
            // Set the column span to cover all columns
            // Note: AbsorbedTable uses FlowEngine for colspan; here we just add a regular cell
            // For visual merging, you can set the cell's Width manually if needed
        };
        mergedCell.Paragraphs.Add(new TextFragment("Merged Cell Across All Columns"));
        mergedRow.Cells.Add(mergedCell);

        // Append the merged row
        newTable.Rows.Add(mergedRow);

        // Replace the original absorbed table with the newly built table on the same page
        absorber.Replace(pdfDocument.Pages[absorbedTable.PageNum], absorbedTable, newTable);

        // Save the modified PDF
        pdfDocument.Save(outputPath);

        Console.WriteLine($"Table updated and saved to '{outputPath}'.");
    }
}