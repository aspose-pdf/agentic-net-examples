using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "custom_table_borders.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table and add it to the page
            Table table = new Table();
            page.Paragraphs.Add(table);

            // Define column widths (space‑separated values)
            table.ColumnWidths = "100 150 100";

            // Set a uniform border for the whole table
            table.Border = new BorderInfo();
            table.DefaultCellBorder = new BorderInfo();

            // Add header row
            Row header = table.Rows.Add();
            header.BackgroundColor = Aspose.Pdf.Color.LightGray;
            header.DefaultCellBorder = new BorderInfo();
            AddCell(header, "ID");
            AddCell(header, "Name");
            AddCell(header, "Value");

            // Add data rows with custom border styles based on row index
            var rnd = new Random();
            for (int i = 0; i < 6; i++)
            {
                Row row = table.Rows.Add();

                // Alternate background color for visual distinction
                row.BackgroundColor = (i % 2 == 0) ? Aspose.Pdf.Color.White : Aspose.Pdf.Color.LightGray;

                // Apply different border thickness per row index (using default solid borders)
                row.Border = new BorderInfo();
                row.DefaultCellBorder = new BorderInfo();

                // Populate cells
                AddCell(row, (i + 1).ToString());
                AddCell(row, $"Item {i + 1}");
                AddCell(row, rnd.Next(100, 1000).ToString());
            }

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
                    Console.WriteLine($"PDF saved to '{outputPath}'. (non‑Windows platform with libgdiplus installed)");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF was not saved.");
                }
            }
        }
    }

    // Helper method to add a cell with simple text
    private static void AddCell(Row row, string text)
    {
        Cell cell = new Cell();
        cell.Paragraphs.Add(new TextFragment(text));
        row.Cells.Add(cell);
    }

    // Walk the inner‑exception chain to detect a missing native GDI+ library
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
