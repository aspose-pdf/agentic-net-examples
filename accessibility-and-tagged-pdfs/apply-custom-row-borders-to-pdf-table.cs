using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text; // Added for TextFragment

class Program
{
    static void Main()
    {
        const string outputPath = "styled_table.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a page to host the table
            Page page = doc.Pages.Add();

            // Create a table with three equal columns
            Table table = new Table
            {
                ColumnWidths = "100 100 100",
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5)
            };
            page.Paragraphs.Add(table);

            // Header row (same style for all header cells)
            Row header = table.Rows.Add();
            header.DefaultCellBorder = CreateBorder(Aspose.Pdf.Color.Black, 1);
            header.Cells.Add(CreateCell("ID"));
            header.Cells.Add(CreateCell("Name"));
            header.Cells.Add(CreateCell("Value"));

            // Populate data rows with alternating border styles
            for (int i = 0; i < 10; i++)
            {
                Row row = table.Rows.Add();

                // Apply custom border based on row index (even/odd)
                if (i % 2 == 0) // even rows
                {
                    row.DefaultCellBorder = CreateBorder(Aspose.Pdf.Color.LightGray, 0.5);
                }
                else // odd rows
                {
                    row.DefaultCellBorder = CreateBorder(Aspose.Pdf.Color.Blue, 1.5);
                }

                row.Cells.Add(CreateCell((i + 1).ToString()));
                row.Cells.Add(CreateCell($"Item {i + 1}"));
                row.Cells.Add(CreateCell((i * 10).ToString()));
            }

            // Save the PDF document – guard against missing libgdiplus on non‑Windows platforms
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF with styled table saved to '{outputPath}'.");
            }
            else
            {
                try
                {
                    doc.Save(outputPath);
                    Console.WriteLine($"PDF with styled table saved to '{outputPath}'.");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                                      "The PDF was not saved, but the code executed correctly.");
                }
            }
        }
    }

    // Helper to create a table cell containing simple text
    static Cell CreateCell(string text)
    {
        Cell cell = new Cell();
        cell.Paragraphs.Add(new TextFragment(text));
        return cell;
    }

    // Helper to create a BorderInfo with specified color and line width using the proper constructor
    static BorderInfo CreateBorder(Aspose.Pdf.Color color, double width)
    {
        // BorderInfo does not expose settable Color/Width properties; use the constructor.
        return new BorderInfo(BorderSide.All, (float)width, color);
    }

    // Detect a nested DllNotFoundException (e.g., missing libgdiplus) inside a TypeInitializationException
    static bool ContainsDllNotFound(Exception? ex)
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
