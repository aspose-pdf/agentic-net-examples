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

        // Load the PDF document inside a using block (ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Create a TableAbsorber to locate tables on the first page
            TableAbsorber absorber = new TableAbsorber
            {
                // Enable the flow engine – it provides richer table information
                UseFlowEngine = true
            };

            // Extract tables from the first page (pages are 1‑based)
            absorber.Visit(doc.Pages[1]);

            // Ensure at least one table was found
            if (absorber.TableList.Count == 0)
            {
                Console.WriteLine("No tables found on the first page.");
                doc.Save(outputPath); // Save unchanged document
                return;
            }

            // Get the first detected table (AbsorbedTable)
            var absorbedTable = absorber.TableList[0];

            // ------------------------------------------------------------
            // 1) Update a cell value (e.g., first row, second column)
            // ------------------------------------------------------------
            // RowList and CellList are also 0‑based collections
            var targetCell = absorbedTable.RowList[0].CellList[1];

            // Change the text of the first text fragment in the cell
            if (targetCell.TextFragments.Count > 0)
                targetCell.TextFragments[0].Text = "Updated Value";

            // ------------------------------------------------------------
            // 2) Create a new table with merged cells and modified properties
            // ------------------------------------------------------------
            Table newTable = new Table();

            // Example: set column widths (two columns, each 100 points wide)
            newTable.ColumnWidths = "100 100";

            // ---- Row 1 (merged across two columns) ----
            Row headerRow = new Row();

            Cell mergedCell = new Cell
            {
                // Merge the first two columns
                ColSpan = 2,
                // Set the cell text
                DefaultCellTextState = new TextState("Merged Header")
            };
            headerRow.Cells.Add(mergedCell);
            newTable.Rows.Add(headerRow);

            // ---- Row 2 (regular two cells) ----
            Row dataRow = new Row();

            Cell cellA = new Cell
            {
                DefaultCellTextState = new TextState("Cell A")
            };
            Cell cellB = new Cell
            {
                DefaultCellTextState = new TextState("Cell B")
            };
            dataRow.Cells.Add(cellA);
            dataRow.Cells.Add(cellB);
            newTable.Rows.Add(dataRow);

            // Modify visual properties of the new table
            newTable.Border = new BorderInfo(BorderSide.All, 1, Aspose.Pdf.Color.Black);
            newTable.DefaultCellPadding = new MarginInfo(5, 5, 5, 5);
            newTable.DefaultCellTextState = new TextState
            {
                FontSize = 12,
                Font = FontRepository.FindFont("Helvetica"),
                ForegroundColor = Aspose.Pdf.Color.DarkBlue
            };

            // ------------------------------------------------------------
            // 3) Replace the original table with the newly created one
            // ------------------------------------------------------------
            absorber.Replace(doc.Pages[1], absorbedTable, newTable);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table updated and saved to '{outputPath}'.");
    }
}
