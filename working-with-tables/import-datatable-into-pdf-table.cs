using System;
using System.Data;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Prepare in‑memory data
        DataTable dt = new DataTable("Sample");
        dt.Columns.Add("ID", typeof(int));
        dt.Columns.Add("Name", typeof(string));
        dt.Columns.Add("Price", typeof(decimal));

        dt.Rows.Add(1, "Apple", 0.99m);
        dt.Rows.Add(2, "Banana", 0.59m);
        dt.Rows.Add(3, "Cherry", 2.49m);

        // Create a PDF document
        using (Document doc = new Document())
        {
            // Add a page
            Page page = doc.Pages.Add();

            // Create a table and set its position
            Table table = new Table
            {
                // Position the table on the page (coordinates in points)
                Left = 50,
                Top = 700,
                // Optional visual styling – colour settings are applied only on Windows to avoid GDI+ issues on other OSes
                Border = GetBorderInfo(BorderSide.All, 0.5f),
                DefaultCellBorder = GetBorderInfo(BorderSide.All, 0.5f),
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5)
            };

            // Define column widths (optional)
            table.ColumnWidths = "100 150 100";

            // Import the DataTable into the Aspose.Pdf.Table
            // Parameters: source DataTable, import column names as first row,
            // start at row 0, column 0 (zero‑based indices)
            table.ImportDataTable(dt, true, 0, 0);

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the PDF – guard against missing libgdiplus on non‑Windows platforms
            string outputPath = "DataTableExport.pdf";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
            }
            else
            {
                try
                {
                    doc.Save(outputPath);
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF was not saved.");
                }
            }
        }

        Console.WriteLine("PDF generation completed.");
    }

    // Helper to create BorderInfo – colour is set via constructor only on Windows
    private static BorderInfo GetBorderInfo(BorderSide side, float width)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            // Aspose.Pdf.Color is used for border colour
            return new BorderInfo(side, width, Aspose.Pdf.Color.Black);
        }
        else
        {
            // On non‑Windows platforms we omit the colour argument
            return new BorderInfo(side, width);
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
