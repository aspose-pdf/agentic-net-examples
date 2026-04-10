using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Aspose.Pdf;                      // Core PDF API
using Aspose.Pdf.Text;                 // For text styling if needed
using Aspose.Pdf.Facades;              // Facade namespace as required

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string outputPdf = "subset.pdf";

        // -----------------------------------------------------------------
        // 1. Load data into a DataTable.
        //    The original example used Aspose.Cells to read an XLSX file, but the
        //    current project does not reference the Aspose.Cells assembly. To keep
        //    the sample self‑contained we generate a DataTable programmatically.
        //    If you need to read a real Excel workbook, add the Aspose.Cells NuGet
        //    package and replace the call to CreateSampleDataTable() with the
        //    original ExportDataTable logic.
        // -----------------------------------------------------------------
        DataTable fullTable = CreateSampleDataTable();

        // -----------------------------------------------------------------
        // 2. Filter rows – example: keep rows where the value in column "Status"
        //    equals "Approved". Adjust the filter logic to your needs.
        // -----------------------------------------------------------------
        const string filterColumn = "Status"; // column name to filter on
        const string filterValue  = "Approved";

        if (!fullTable.Columns.Contains(filterColumn))
        {
            Console.Error.WriteLine($"Column '{filterColumn}' not found in the data table.");
            return;
        }

        var filteredRows = fullTable.AsEnumerable()
            .Where(r => string.Equals(r.Field<string>(filterColumn), filterValue, StringComparison.OrdinalIgnoreCase));

        DataTable filteredTable = fullTable.Clone(); // copies structure (including column names)
        foreach (DataRow row in filteredRows)
            filteredTable.ImportRow(row);

        // -----------------------------------------------------------------
        // 3. Create a PDF document and add a table populated from the filtered DataTable
        // -----------------------------------------------------------------
        using (Document pdfDoc = new Document())
        {
            Page page = pdfDoc.Pages.Add();
            Table table = new Table();

            int columnCount = filteredTable.Columns.Count;
            if (columnCount > 0)
            {
                // Simple equal column widths – 100 points each
                string widths = string.Join(" ", Enumerable.Repeat("100", columnCount));
                table.ColumnWidths = widths;
            }

            table.Border = new BorderInfo(BorderSide.All, 0.5f, Color.Black);
            table.DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Color.Gray);

            // Import the filtered DataTable into the PDF table.
            // Parameters: (DataTable, includeColumnNames, startRow, startColumn)
            table.ImportDataTable(filteredTable, true, 0, 0);

            page.Paragraphs.Add(table);

            // -----------------------------------------------------------------
            // 4. Save the PDF – guard against missing GDI+ on non‑Windows platforms
            // -----------------------------------------------------------------
            try
            {
                pdfDoc.Save(outputPdf);
                Console.WriteLine($"Subset PDF saved to '{outputPdf}'.");
            }
            catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
            {
                Console.WriteLine("GDI+ (libgdiplus) is not available on this platform. PDF save skipped.");
            }
        }
    }

    // ---------------------------------------------------------------------
    // Helper: creates a sample DataTable that mimics data that could come
    // from an Excel worksheet. Replace with real Excel loading when Aspose.Cells
    // is available.
    // ---------------------------------------------------------------------
    private static DataTable CreateSampleDataTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("ID", typeof(int));
        dt.Columns.Add("Name", typeof(string));
        dt.Columns.Add("Status", typeof(string));
        dt.Columns.Add("Amount", typeof(decimal));

        dt.Rows.Add(1, "Alice",   "Approved", 1200.50m);
        dt.Rows.Add(2, "Bob",     "Pending",  850.00m);
        dt.Rows.Add(3, "Charlie", "Approved", 430.75m);
        dt.Rows.Add(4, "Diana",   "Rejected",  0.00m);
        dt.Rows.Add(5, "Eve",     "Approved", 760.20m);

        return dt;
    }

    // Helper to detect a nested DllNotFoundException (e.g., missing libgdiplus)
    private static bool ContainsDllNotFound(Exception ex)
    {
        Exception? current = ex;
        while (current != null)
        {
            if (current is DllNotFoundException)
                return true;
            current = current.InnerException;
        }
        return false;
    }
}
