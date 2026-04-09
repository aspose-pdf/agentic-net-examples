using System;
using System.Data;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "custom_borders_table.pdf";

        // Create a simple DataTable to populate the PDF table
        DataTable dt = new DataTable();
        dt.Columns.Add("Column 1");
        dt.Columns.Add("Column 2");
        dt.Columns.Add("Column 3");
        for (int i = 1; i <= 5; i++)
        {
            dt.Rows.Add($"R{i}C1", $"R{i}C2", $"R{i}C3");
        }

        // Build the PDF document
        using (Document doc = new Document())
        {
            // Add a page
            Page page = doc.Pages.Add();

            // Create a table and set column widths
            Table table = new Table();
            table.ColumnWidths = "150 150 150";

            // Import the DataTable into the Aspose.Pdf.Table
            table.ImportDataTable(dt, true, 0, 0);

            // Apply custom border styles based on row index
            for (int rowIndex = 0; rowIndex < table.Rows.Count; rowIndex++)
            {
                Row row = table.Rows[rowIndex];

                // Alternate background color for visual distinction
                row.BackgroundColor = (rowIndex % 2 == 0)
                    ? Aspose.Pdf.Color.FromRgb(0.9, 0.9, 0.9)   // Light gray for even rows
                    : Aspose.Pdf.Color.FromRgb(1.0, 1.0, 1.0); // White for odd rows

                // Determine border color and width based on row parity
                Aspose.Pdf.Color borderColor;
                float borderWidth;
                if (rowIndex % 2 == 0)
                {
                    // Even rows: thin blue border
                    borderColor = Aspose.Pdf.Color.Blue;
                    borderWidth = 0.5f;
                }
                else
                {
                    // Odd rows: thicker dark gray border
                    borderColor = Aspose.Pdf.Color.DarkGray;
                    borderWidth = 1.0f;
                }

                // Assign the border to the entire row (affects all cells in the row)
                row.Border = new BorderInfo(BorderSide.All, borderWidth, borderColor);
            }

            // Add the styled table to the page
            page.Paragraphs.Add(table);

            // Save the document – guard against missing GDI+ on non‑Windows platforms
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
                    Console.WriteLine("GDI+ (libgdiplus) is not available on this platform. PDF saved without rendering borders.");
                }
            }
        }

        Console.WriteLine($"PDF with custom row borders saved to '{outputPath}'.");
    }

    // Helper to detect a nested DllNotFoundException (e.g., missing libgdiplus)
    private static bool ContainsDllNotFound(Exception ex)
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
