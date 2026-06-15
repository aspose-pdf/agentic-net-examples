using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "TableWithHeaderAndRepeatingColumn.pdf";

        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table with repeating header row and first column
            Table table = new Table
            {
                ColumnWidths = "100 100 100",
                RepeatingRowsCount = 1,   // repeat header row on each new page
                RepeatingColumnsCount = 1 // repeat first column on each new page
            };

            // ----- Header row (will be repeated) -----
            Row headerRow = table.Rows.Add();
            headerRow.Cells.Add("Header 1");
            headerRow.Cells.Add("Header 2");
            headerRow.Cells.Add("Header 3");

            // Style the header cells
            foreach (Cell cell in headerRow.Cells)
            {
                cell.DefaultCellTextState = new TextState
                {
                    FontSize = 12,
                    FontStyle = FontStyles.Bold,
                    ForegroundColor = Color.Blue
                };
                cell.BackgroundColor = Color.LightGray;
            }

            // ----- Data rows (will flow across pages) -----
            for (int i = 1; i <= 30; i++)
            {
                Row dataRow = table.Rows.Add();
                dataRow.Cells.Add($"Row {i} - Col 1");
                dataRow.Cells.Add($"Row {i} - Col 2");
                dataRow.Cells.Add($"Row {i} - Col 3");
            }

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Guard Document.Save for platforms without libgdiplus (e.g., macOS/Linux)
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

        Console.WriteLine("PDF creation completed.");
    }

    // Helper to detect a nested DllNotFoundException (libgdiplus missing)
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