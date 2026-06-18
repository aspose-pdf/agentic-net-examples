using System;
using System.Data;
using System.Runtime.InteropServices;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        // Prepare sample data
        DataTable dt = new DataTable("Sample");
        dt.Columns.Add("ID", typeof(int));
        dt.Columns.Add("Name", typeof(string));
        dt.Columns.Add("Score", typeof(double));

        dt.Rows.Add(1, "Alice", 85.5);
        dt.Rows.Add(2, "Bob", 92.0);
        dt.Rows.Add(3, "Charlie", 78.0);
        dt.Rows.Add(4, "Diana", 88.5);

        // Create a DataView with a filter (only rows with Score > 80)
        DataView dv = new DataView(dt);
        dv.RowFilter = "Score > 80";

        // Create PDF document (wrapped in using for proper disposal)
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table and configure basic layout
            Table table = new Table
            {
                // Define three column widths (in points)
                ColumnWidths = "100 150 100",
                // Add a thin border around each cell
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f),
                // Add padding inside each cell
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5)
            };

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Import the filtered DataView into the table
            // Parameters:
            //   sourceDataView          : dv
            //   isColumnNamesImported  : true (include column headers)
            //   firstFilledRow          : 0 (start at first row)
            //   firstFilledColumn       : 0 (start at first column)
            //   maxRows                 : int.MaxValue (no row limit)
            //   maxColumns              : int.MaxValue (no column limit)
            table.ImportDataView(dv, true, 0, 0, int.MaxValue, int.MaxValue);

            // Save the PDF document – guard against missing libgdiplus on non‑Windows platforms
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
                    Console.WriteLine($"PDF saved to '{outputPath}'. (non‑Windows platform, libgdiplus may be required)");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF was not saved.");
                }
            }
        }
    }

    // Helper to detect a nested DllNotFoundException (e.g., missing libgdiplus)
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

// Suppress known NuGet vulnerability warning (NU1903) for Microsoft.Bcl.Memory 9.0.4
#pragma warning disable NU1903