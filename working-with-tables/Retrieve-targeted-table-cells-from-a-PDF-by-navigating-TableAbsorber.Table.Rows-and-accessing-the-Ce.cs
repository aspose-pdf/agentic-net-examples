using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TableAbsorber to locate tables in the document
            TableAbsorber absorber = new TableAbsorber();

            // Extract tables from the whole document
            absorber.Visit(doc);

            // Check that at least one table was found
            if (absorber.TableList.Count == 0)
            {
                Console.WriteLine("No tables detected in the document.");
                return;
            }

            // Example: retrieve the cell at row 2, column 4 of the first table
            // Note: collections are zero‑based, so row index 1 = second row, cell index 3 = fourth cell
            int tableIndex = 0;   // first table
            int rowIndex   = 1;   // second row
            int cellIndex  = 3;   // fourth cell

            // Access the targeted table
            var table = absorber.TableList[tableIndex];

            // Validate row index
            if (rowIndex >= table.RowList.Count)
            {
                Console.WriteLine("Target row index is out of range.");
                return;
            }

            // Access the targeted row
            var row = table.RowList[rowIndex];

            // Validate cell index
            if (cellIndex >= row.CellList.Count)
            {
                Console.WriteLine("Target cell index is out of range.");
                return;
            }

            // Access the targeted cell
            var cell = row.CellList[cellIndex];

            // Each cell may contain multiple TextFragment objects.
            // Iterate them and output their text.
            foreach (TextFragment fragment in cell.TextFragments)
            {
                Console.WriteLine($"Cell [{rowIndex + 1},{cellIndex + 1}] text: {fragment.Text}");
            }
        }
    }
}