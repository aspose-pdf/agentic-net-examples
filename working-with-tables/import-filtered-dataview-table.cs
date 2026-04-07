using System;
using System.Data;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a sample DataTable
        DataTable dataTable = new DataTable("Sample");
        dataTable.Columns.Add("ID", typeof(int));
        dataTable.Columns.Add("Name", typeof(string));
        dataTable.Columns.Add("Score", typeof(int));

        dataTable.Rows.Add(1, "Alice", 85);
        dataTable.Rows.Add(2, "Bob", 70);
        dataTable.Rows.Add(3, "Charlie", 92);
        dataTable.Rows.Add(4, "Diana", 60);

        // Create a DataView and filter rows where Score >= 80
        DataView dataView = new DataView(dataTable);
        dataView.RowFilter = "Score >= 80";

        // Create a PDF document
        using (Document pdfDocument = new Document())
        {
            // Add a page to the document
            Page page = pdfDocument.Pages.Add();

            // Create a table with column widths (optional)
            Table table = new Table();
            table.ColumnWidths = "100 200 100";

            // Import the filtered DataView into the table
            // Parameters: sourceDataView, isColumnNamesImported, firstFilledRow, firstFilledColumn, maxRows, maxColumns
            int maxRows = dataView.Count;
            int maxColumns = dataView.Table.Columns.Count;
            table.ImportDataView(dataView, true, 0, 0, maxRows, maxColumns);

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the PDF document – guard against missing GDI+ on non‑Windows platforms
            string outputPath = "output.pdf";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                pdfDocument.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
            else
            {
                try
                {
                    pdfDocument.Save(outputPath);
                    Console.WriteLine($"PDF saved to '{outputPath}'.");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF could not be saved.");
                }
            }
        }
    }

    // Helper that walks the inner‑exception chain looking for a DllNotFoundException
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
