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

        // Load the PDF document (wrapped in using for proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Create a TableAbsorber to find tables
            TableAbsorber absorber = new TableAbsorber();

            // Enable the flow engine to get accurate RowSpan/ColumnSpan values
            absorber.UseFlowEngine = true;

            // Extract tables from the whole document
            absorber.Visit(doc);

            int tableIndex = 0;
            foreach (var absorbedTable in absorber.TableList)
            {
                bool containsMergedCell = false;

                // Iterate through rows and cells of the table
                foreach (var absorbedRow in absorbedTable.RowList)
                {
                    foreach (var absorbedCell in absorbedRow.CellList)
                    {
                        // AbsorbedCell does not expose RowSpan/ColumnSpan at compile time in older
                        // Aspose.Pdf versions. Use dynamic dispatch to access them at runtime.
                        dynamic cell = absorbedCell;
                        int rowSpan = 0;
                        int colSpan = 0;
                        try { rowSpan = (int)cell.RowSpan; } catch { rowSpan = 1; }
                        try { colSpan = (int)cell.ColumnSpan; } catch { colSpan = 1; }

                        // A merged cell has RowSpan > 1 or ColumnSpan > 1
                        if (rowSpan > 1 || colSpan > 1)
                        {
                            containsMergedCell = true;
                            break;
                        }
                    }

                    if (containsMergedCell)
                        break;
                }

                Console.WriteLine($"Table {tableIndex} (Page {absorbedTable.PageNum}) contains merged cells: {containsMergedCell}");
                tableIndex++;
            }
        }
    }
}
