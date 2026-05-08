using System;
using System.Data;
using System.IO;
using System.Runtime.InteropServices; // for OS check
using Aspose.Pdf;
using Aspose.Pdf.Text; // for TextState if needed

class Program
{
    static void Main()
    {
        // Paths (adjust as needed)
        const string outputPdfPath = "output.pdf";

        // Sample data creation
        DataTable dt = new DataTable();
        dt.Columns.Add("Id", typeof(int));
        dt.Columns.Add("Name", typeof(string));
        dt.Columns.Add("Score", typeof(double));

        dt.Rows.Add(1, "Alice", 85.5);
        dt.Rows.Add(2, "Bob", 92.0);
        dt.Rows.Add(3, "Charlie", 78.0);
        dt.Rows.Add(4, "David", 88.5);
        dt.Rows.Add(5, "Eve", 91.0);

        // Create a DataView and apply a filter (e.g., only scores >= 90)
        DataView view = new DataView(dt);
        view.RowFilter = "Score >= 90";

        // Create a new PDF document (wrapped in using for proper disposal)
        using (Document doc = new Document())
        {
            // Add a page to host the table
            Page page = doc.Pages.Add();

            // Create a table instance
            Table table = new Table();

            // Define column widths (optional – here we create as many columns as the source)
            // Ensure the table has at least one row with the required number of cells
            // This satisfies the requirement that the target column must exist before import.
            Row headerRow = table.Rows.Add();

            // Guard against a possible null DataView.Table (the warning CS8602)
            int columnCount = view.Table?.Columns?.Count ?? 0;
            for (int i = 0; i < columnCount; i++)
            {
                // Add empty cells to establish column count
                headerRow.Cells.Add(string.Empty);
            }

            // Import the filtered DataView into the table
            // Parameters:
            //   sourceDataView          : view (filtered)
            //   isColumnNamesImported  : true (import column names as first row)
            //   firstFilledRow         : 0 (start at first row of the table)
            //   firstFilledColumn      : 0 (start at first column)
            //   maxRows                : view.Count (import all filtered rows)
            //   maxColumns             : view.Table?.Columns?.Count ?? 0 (import all columns)
            table.ImportDataView(view, true, 0, 0, view.Count, columnCount);

            // Add the table to the page's paragraphs collection
            page.Paragraphs.Add(table);

            // Save the resulting PDF – guard against missing libgdiplus on non‑Windows platforms
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPdfPath);
                Console.WriteLine($"PDF with filtered table saved to '{outputPdfPath}'.");
            }
            else
            {
                // Attempt to save and handle the GDI+ (libgdiplus) issue gracefully
                try
                {
                    doc.Save(outputPdfPath);
                    Console.WriteLine($"PDF saved (non‑Windows platform) to '{outputPdfPath}'.");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                                      "The PDF could not be saved, but the rest of the program executed correctly.");
                }
            }
        }
    }

    // Helper to walk the exception chain and detect a missing native GDI+ library
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
