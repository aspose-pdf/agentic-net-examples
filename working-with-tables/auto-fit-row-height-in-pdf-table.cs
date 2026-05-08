using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "autoheight_table.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table and configure its appearance
            Table table = new Table
            {
                // Two columns with equal width
                ColumnWidths = "200 200",
                // Table border
                Border = new BorderInfo(BorderSide.All, 1f),
                // Default cell border and padding
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f),
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5),
                // Default text style for cells
                DefaultCellTextState = new TextState
                {
                    Font = FontRepository.FindFont("Helvetica"),
                    FontSize = 12,
                    ForegroundColor = Color.Black
                }
            };

            // Add a row to the table
            Row row = table.Rows.Add();

            // Enable automatic height adjustment.
            // Setting FixedRowHeight to 0 (or leaving it unset) lets the row grow
            // to fit the content of its cells.
            row.FixedRowHeight = 0;

            // Add cells with different amounts of text
            row.Cells.Add("Short text");
            row.Cells.Add("This is a longer piece of text that should cause the row height to increase automatically to fit the content.");

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the document.
            // On non‑Windows platforms the PDF save may require GDI+ (libgdiplus).
            // Wrap the save in a try‑catch to handle missing native libraries gracefully.
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX) ||
                RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                try
                {
                    doc.Save(outputPath);
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Skipping save on this platform: GDI+ (libgdiplus) is not available.");
                }
            }
            else
            {
                doc.Save(outputPath);
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