using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        // Verify the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Create a TableAbsorber and enable the FlowEngine to detect merged cells
            TableAbsorber absorber = new TableAbsorber
            {
                UseFlowEngine = true
            };

            // Extract tables from the entire document
            absorber.Visit(doc);

            int tableIndex = 0;

            // Iterate over each detected table
            foreach (var table in absorber.TableList)
            {
                bool hasMergedCells = false;

                // Examine each row and each cell within the row
                foreach (var row in table.RowList)
                {
                    foreach (var cell in row.CellList)
                    {
                        // ColSpan > 1 indicates a column merge
                        if (cell.ColSpan > 1)
                            hasMergedCells = true;

                        // RowSpan may not be present on AbsorbedCell; use reflection to check safely
                        var rowSpanProp = cell.GetType().GetProperty("RowSpan");
                        if (rowSpanProp != null)
                        {
                            int rowSpan = (int)rowSpanProp.GetValue(cell);
                            if (rowSpan > 1)
                                hasMergedCells = true;
                        }
                    }
                }

                Console.WriteLine(
                    $"Table {++tableIndex} on page {table.PageNum}: " +
                    (hasMergedCells ? "contains merged cells" : "no merged cells"));
            }
        }
    }
}