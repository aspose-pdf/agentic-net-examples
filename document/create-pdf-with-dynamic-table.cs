using System;
using System.Data;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

#pragma warning disable NU1903 // Suppress known vulnerability warning for Microsoft.Bcl.Memory

class DynamicTablePdf
{
    static void Main()
    {
        // Prepare sample data in a DataTable.
        DataTable data = new DataTable();
        data.Columns.Add("ID", typeof(int));
        data.Columns.Add("Name", typeof(string));
        data.Columns.Add("Quantity", typeof(int));

        // Populate the table with a variable number of rows.
        for (int i = 1; i <= 15; i++)
        {
            data.Rows.Add(i, $"Item {i}", i * 10);
        }

        // Create a new PDF document inside a using block (ensures proper disposal).
        using (Document doc = new Document())
        {
            // Add a page to the document.
            Page page = doc.Pages.Add();

            // Create a table and configure its appearance.
            Table table = new Table
            {
                // Define column widths (space‑separated string, values are in points).
                ColumnWidths = "80 200 100",

                // Set a simple border for all cells using BorderInfo.
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Color.Black),

                // Optional: set padding for better readability.
                DefaultCellPadding = new MarginInfo { Top = 5, Bottom = 5, Left = 5, Right = 5 }
            };

            // Import the DataTable into the Aspose.Pdf.Table.
            // Parameters:
            //   data               – source DataTable
            //   true               – import column names as the first row
            //   0                  – start importing at the first row of the table (zero‑based)
            //   0                  – start importing at the first column of the table (zero‑based)
            table.ImportDataTable(data, true, 0, 0);

            // Add the populated table to the page's paragraph collection.
            page.Paragraphs.Add(table);

            // Save the PDF document – guard against missing libgdiplus on non‑Windows platforms.
            string outputPath = "DynamicTable.pdf";
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
                    Console.WriteLine("Warning: libgdiplus is not available on this platform. PDF was not saved.");
                }
            }
        }

        Console.WriteLine("PDF with dynamic table created successfully.");
    }

    // Helper method to detect a nested DllNotFoundException (e.g., missing libgdiplus).
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

#pragma warning restore NU1903