using System;
using System.Data;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "styled_table.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table and set its position, column widths and default cell border
            Table table = new Table
            {
                Left = 50,
                Top = 700,
                // ColumnWidths must be a space‑separated string
                ColumnWidths = "150 150 150",
                // Use BorderInfo to define cell borders
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Color.Black)
            };

            // Prepare sample data in a DataTable
            DataTable data = new DataTable("Sample");
            data.Columns.Add("Product", typeof(string));
            data.Columns.Add("Quantity", typeof(int));
            data.Columns.Add("Price", typeof(string));
            data.Rows.Add("Apple", 10, "$1.00");
            data.Rows.Add("Banana", 5, "$0.50");
            data.Rows.Add("Cherry", 20, "$2.00");
            data.Rows.Add("Date", 7, "$3.00");
            data.Rows.Add("Elderberry", 3, "$4.00");

            // Import the DataTable into the Aspose.Pdf.Table (including column headers)
            table.ImportDataTable(data, true, 0, 0);

            // Apply alternating background colors to data rows (skip header row at index 0)
            for (int i = 1; i < table.Rows.Count; i++)
            {
                Row row = table.Rows[i];
                row.BackgroundColor = (i % 2 == 0) ? Color.LightGray : Color.White;
            }

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the PDF document – guard against missing GDI+ on non‑Windows platforms
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
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                                      "The PDF could not be saved.");
                }
            }
        }
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
