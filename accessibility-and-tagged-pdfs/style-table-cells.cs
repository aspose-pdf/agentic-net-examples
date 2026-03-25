using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing; // for Color, BorderInfo, MarginInfo, etc.

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

            // Create a table with two columns
            Table table = new Table
            {
                // Column widths are defined as a space‑separated string
                ColumnWidths = "150 150",
                // Default padding for all cells
                DefaultCellPadding = new MarginInfo(2, 2, 2, 2)
            };

            // First row
            Row row1 = table.Rows.Add();
            Cell cell11 = row1.Cells.Add();
            cell11.BackgroundColor = Color.LightGray;
            cell11.Border = new BorderInfo(BorderSide.All, 1f, Color.Black);
            cell11.Margin = new MarginInfo(5, 5, 5, 5);
            cell11.Paragraphs.Add(new TextFragment("Cell 1"));

            Cell cell12 = row1.Cells.Add();
            cell12.BackgroundColor = Color.LightBlue;
            cell12.Border = new BorderInfo(BorderSide.All, 2f, Color.DarkBlue);
            cell12.Margin = new MarginInfo(5, 5, 5, 5);
            cell12.Paragraphs.Add(new TextFragment("Cell 2"));

            // Second row
            Row row2 = table.Rows.Add();
            Cell cell21 = row2.Cells.Add();
            cell21.BackgroundColor = Color.LightGreen;
            cell21.Border = new BorderInfo(BorderSide.All, 1.5f, Color.Green);
            cell21.Margin = new MarginInfo(5, 5, 5, 5);
            cell21.Paragraphs.Add(new TextFragment("Cell 3"));

            Cell cell22 = row2.Cells.Add();
            cell22.BackgroundColor = Color.LightCoral;
            cell22.Border = new BorderInfo(BorderSide.All, 1f, Color.Red);
            cell22.Margin = new MarginInfo(5, 5, 5, 5);
            cell22.Paragraphs.Add(new TextFragment("Cell 4"));

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the PDF – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
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
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF generation was skipped.");
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
