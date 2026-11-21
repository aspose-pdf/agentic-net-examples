using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing;

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

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least 6 pages
            if (doc.Pages.Count < 6)
            {
                Console.Error.WriteLine("The document does not contain page 6.");
                return;
            }

            // Create a TableAbsorber and visit page 6 (1‑based indexing)
            TableAbsorber absorber = new TableAbsorber();
            absorber.Visit(doc.Pages[6]);

            // Retrieve the first absorbed table (if any)
            if (absorber.TableList.Count == 0)
            {
                Console.Error.WriteLine("No table was detected on page 6.");
                return;
            }

            AbsorbedTable absorbedTable = absorber.TableList[0];

            // Determine the number of columns from the first row of the absorbed table
            int columnCount = absorbedTable.RowList.Count > 0 ? absorbedTable.RowList[0].CellList.Count : 0;
            if (columnCount == 0)
            {
                Console.Error.WriteLine("The detected table has no columns.");
                return;
            }

            // Build a new core Table that will replace the absorbed one
            Table newTable = new Table();
            // Simple equal column widths – you can adjust as needed
            newTable.ColumnWidths = string.Join(" ", new string[columnCount].Select(_ => "100"));
            newTable.Border = new BorderInfo(BorderSide.All, 0.5f, Color.Black);
            newTable.DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Color.Gray);
            newTable.DefaultCellPadding = new MarginInfo(4, 4, 4, 4);

            // (Optional) copy existing rows – omitted for brevity; we only add a new row
            Row newRow = newTable.Rows.Add();
            for (int i = 0; i < columnCount; i++)
            {
                newRow.Cells.Add($"New Cell {i + 1}");
            }

            // Replace the absorbed table with the newly built table (preserves position)
            absorber.Replace(doc.Pages[6], absorbedTable, newTable);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
