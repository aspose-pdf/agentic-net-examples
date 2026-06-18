using System;
using System.Data;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Path for output PDF
        const string outputPdfPath = "output_with_table.pdf";

        // ---------------------------------------------------------------
        // Prepare source data (DataTable). In a real scenario this could be
        // loaded from a database, CSV, etc.
        // ---------------------------------------------------------------
        DataTable sourceTable = new DataTable("SampleData");
        sourceTable.Columns.Add("ID",   typeof(int));
        sourceTable.Columns.Add("Name", typeof(string));
        sourceTable.Columns.Add("Score",typeof(double));

        // Populate the DataTable with sample rows
        for (int i = 1; i <= 15; i++)
        {
            sourceTable.Rows.Add(i, $"Student {i}", 60 + i);
        }

        // Determine the number of source records (rows) before import
        int recordCount = sourceTable.Rows.Count;
        Console.WriteLine($"Number of source records: {recordCount}");

        // ---------------------------------------------------------------
        // Create a new PDF document (or load a template) and add a table
        // ---------------------------------------------------------------
        using (Document pdfDoc = new Document())
        {
            // Add a blank page to host the table
            Page page = pdfDoc.Pages.Add();

            // Create and configure the Table instance
            Table table = new Table
            {
                Left = 50,                     // 50 points from the left edge
                Top = 700,                     // 700 points from the bottom edge
                ColumnWidths = "100 200 100",
                Border = new BorderInfo(BorderSide.All, 1f, Aspose.Pdf.Color.Black),
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5),
                DefaultCellTextState = new TextState
                {
                    Font = FontRepository.FindFont("Helvetica"),
                    FontSize = 12,
                    ForegroundColor = Aspose.Pdf.Color.Black
                }
            };

            // Import the DataTable into the PDF Table.
            // true  – import column names as the first row
            // 0,0   – start at first row / first column of the target table
            table.ImportDataTable(sourceTable, true, 0, 0);

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the resulting PDF – guard against missing libgdiplus on non‑Windows platforms
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                pdfDoc.Save(outputPdfPath);
            }
            else
            {
                try
                {
                    pdfDoc.Save(outputPdfPath);
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF saved without GDI‑dependent features.");
                }
            }
        }

        Console.WriteLine($"PDF with table saved to '{outputPdfPath}'.");
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
