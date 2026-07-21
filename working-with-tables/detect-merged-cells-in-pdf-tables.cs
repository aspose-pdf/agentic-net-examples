using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Create a TableAbsorber instance
            TableAbsorber absorber = new TableAbsorber();

            // Enable the FlowEngine to get accurate RowSpan/ColSpan information
            absorber.UseFlowEngine = true;

            // Extract tables from the whole document
            absorber.Visit(doc);

            // Iterate over all detected tables
            for (int t = 0; t < absorber.TableList.Count; t++)
            {
                var table = absorber.TableList[t];
                bool tableHasMergedCell = false;

                // Iterate rows
                for (int r = 0; r < table.RowList.Count; r++)
                {
                    var row = table.RowList[r];

                    // Iterate cells in the row
                    for (int c = 0; c < row.CellList.Count; c++)
                    {
                        var cell = row.CellList[c];

                        // Check for merged cells (RowSpan > 1 or ColSpan > 1)
                        // AbsorbedCell provides ColSpan; RowSpan is also available when FlowEngine is used
                        int colSpan = cell.ColSpan;
                        int rowSpan = 0;
                        // RowSpan property may not exist on AbsorbedCell in older versions;
                        // use reflection as a safe fallback
                        var rowSpanProp = cell.GetType().GetProperty("RowSpan");
                        if (rowSpanProp != null)
                        {
                            rowSpan = (int)rowSpanProp.GetValue(cell);
                        }

                        if (colSpan > 1 || rowSpan > 1)
                        {
                            tableHasMergedCell = true;
                            Console.WriteLine($"Table {t + 1}, Row {r + 1}, Cell {c + 1} is merged (RowSpan={rowSpan}, ColSpan={colSpan})");
                        }
                    }
                }

                if (!tableHasMergedCell)
                {
                    Console.WriteLine($"Table {t + 1} contains no merged cells.");
                }
            }
        }
    }
}