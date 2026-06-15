using System;
using System.IO;
using System.Reflection;
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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TableAbsorber and enable the FlowEngine to get span information
            TableAbsorber absorber = new TableAbsorber
            {
                UseFlowEngine = true
            };

            // Extract tables from the whole document
            absorber.Visit(doc);

            // Iterate over all detected tables
            for (int t = 0; t < absorber.TableList.Count; t++)
            {
                var table = absorber.TableList[t];
                bool hasMergedCells = false;

                // Examine each cell in the table
                foreach (var row in table.RowList)
                {
                    foreach (var cell in row.CellList)
                    {
                        // ColSpan is always available
                        int colSpan = cell.ColSpan;

                        // RowSpan may not be present on AbsorbedCell; use reflection to read it if it exists
                        int rowSpan = 0;
                        PropertyInfo rowSpanProp = cell.GetType().GetProperty("RowSpan");
                        if (rowSpanProp != null && rowSpanProp.PropertyType == typeof(int))
                        {
                            rowSpan = (int)rowSpanProp.GetValue(cell);
                        }

                        // If either span is greater than 1, the cell is merged
                        if (colSpan > 1 || rowSpan > 1)
                        {
                            hasMergedCells = true;
                            break;
                        }
                    }

                    if (hasMergedCells) break;
                }

                // Output the result for the current table
                if (hasMergedCells)
                {
                    Console.WriteLine($"Table {t + 1} on page {table.PageNum} contains merged cells.");
                }
                else
                {
                    Console.WriteLine($"Table {t + 1} on page {table.PageNum} has no merged cells.");
                }
            }
        }
    }
}