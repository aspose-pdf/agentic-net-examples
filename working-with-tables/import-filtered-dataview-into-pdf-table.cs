using System;
using System.Data;
using System.Runtime.InteropServices;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Prepare sample data in a DataTable
        DataTable dataTable = new DataTable("Sample");
        dataTable.Columns.Add("ID", typeof(int));
        dataTable.Columns.Add("Name", typeof(string));
        dataTable.Columns.Add("Score", typeof(int));

        dataTable.Rows.Add(1, "Alice", 85);
        dataTable.Rows.Add(2, "Bob", 92);
        dataTable.Rows.Add(3, "Charlie", 78);
        dataTable.Rows.Add(4, "Diana", 90);

        // Create a DataView with a filter (e.g., Score >= 80)
        DataView dataView = new DataView(dataTable)
        {
            RowFilter = "Score >= 80"
        };

        // Create a new PDF document and add a page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Create a table and import the filtered DataView
            Table table = new Table();

            // Optionally set column widths (example: three equal columns)
            table.ColumnWidths = "100 150 100";

            // Import the DataView:
            // - include column names as the first row
            // - start at row 0, column 0 of the target table
            // - import all rows and columns from the DataView
            table.ImportDataView(
                sourceDataView: dataView,
                isColumnNamesImported: true,
                firstFilledRow: 0,
                firstFilledColumn: 0,
                maxRows: dataView.Count,
                maxColumns: dataView.Table?.Columns.Count ?? 0);

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the PDF – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
            string outputPath = "DataViewTable.pdf";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
            else
            {
                try
                {
                    doc.Save(outputPath);
                    Console.WriteLine($"PDF saved to '{outputPath}'.");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF was not saved.");
                }
            }
        }

        Console.WriteLine("Program finished.");
    }

    private static bool ContainsDllNotFound(Exception? ex)
    {
        while (ex != null)
        {
            if (ex is DllNotFoundException)
                return true;
            ex = ex.InnerException;
        }
        return false;
    }
}
