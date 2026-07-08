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
            // Create a TableAbsorber and enable the FlowEngine to detect merged cells
            TableAbsorber absorber = new TableAbsorber
            {
                UseFlowEngine = true
            };

            // Extract tables from the entire document
            absorber.Visit(doc);

            int tableIndex = 0;
            foreach (var table in absorber.TableList)
            {
                tableIndex++;
                int rowIndex = 0;
                foreach (var row in table.RowList)
                {
                    rowIndex++;
                    int colIndex = 0;
                    foreach (var cell in row.CellList)
                    {
                        colIndex++;

                        // Default span values
                        int rowSpan = 1;
                        int colSpan = cell.ColSpan; // ColSpan is always available

                        // RowSpan may not exist on AbsorbedCell; retrieve via reflection if present
                        PropertyInfo rowSpanProp = cell.GetType().GetProperty("RowSpan");
                        if (rowSpanProp != null && rowSpanProp.PropertyType == typeof(int))
                        {
                            rowSpan = (int)rowSpanProp.GetValue(cell);
                        }

                        // Report cells that span more than one row or column
                        if (rowSpan > 1 || colSpan > 1)
                        {
                            Console.WriteLine(
                                $"Merged cell detected: Table {tableIndex}, Page {table.PageNum}, Row {rowIndex}, Column {colIndex}, RowSpan={rowSpan}, ColSpan={colSpan}");
                        }
                    }
                }
            }
        }
    }
}