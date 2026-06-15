using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string outputPath = "table_with_border.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table
            Table table = new Table();

            // Define column widths (optional)
            table.ColumnWidths = "100 100 100";

            // Set a default cell border (optional, for visual clarity)
            table.DefaultCellBorder = new BorderInfo(
                Aspose.Pdf.BorderSide.All,
                0.5f,
                Aspose.Pdf.Color.LightGray);

            // Apply a solid black border to the entire table
            table.Border = new BorderInfo(
                Aspose.Pdf.BorderSide.All,
                1f,
                Aspose.Pdf.Color.Black);

            // Include the border in column width calculations
            table.IsBordersIncluded = true;

            // Add first row with three cells
            Row row1 = table.Rows.Add();
            row1.Cells.Add("Cell 1");
            row1.Cells.Add("Cell 2");
            row1.Cells.Add("Cell 3");

            // Add second row with three cells
            Row row2 = table.Rows.Add();
            row2.Cells.Add("Cell 4");
            row2.Cells.Add("Cell 5");
            row2.Cells.Add("Cell 6");

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the document.
            // On non‑Windows platforms GDI+ may be missing; handle gracefully.
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
                    Console.WriteLine("GDI+ (libgdiplus) is not available on this platform; PDF cannot be saved.");
                }
            }
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }

    // Helper to detect a nested DllNotFoundException (e.g., missing libgdiplus)
    static bool ContainsDllNotFound(Exception ex)
    {
        while (ex != null)
        {
            if (ex is DllNotFoundException) return true;
            ex = ex.InnerException;
        }
        return false;
    }
}