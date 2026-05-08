using System;
using System.Data;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text; // for TextState if needed

class Program
{
    static void Main()
    {
        // Paths
        const string outputPath = "table_output.pdf";

        // Prepare source data (DataTable) – in real scenarios this could come from a database
        DataTable sourceTable = new DataTable("SampleData");
        sourceTable.Columns.Add("ID", typeof(int));
        sourceTable.Columns.Add("Name", typeof(string));
        sourceTable.Columns.Add("Quantity", typeof(int));

        // Populate the DataTable with sample rows (dynamic row count)
        for (int i = 1; i <= 15; i++)
        {
            sourceTable.Rows.Add(i, $"Item {i}", i * 10);
        }

        // Determine number of source records (rows) before import
        int recordCount = sourceTable.Rows.Count;
        Console.WriteLine($"Source records to import: {recordCount}");

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page
            Page page = doc.Pages.Add();

            // Create a Table instance
            Table table = new Table
            {
                Border = new BorderInfo(BorderSide.All, 0.5f),
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f),
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5),
                // Three equal columns (adjust widths as needed)
                ColumnWidths = "100 200 100"
            };

            // Import the DataTable into the Aspose.Pdf.Table
            //   sourceTable – the DataTable to import
            //   true        – import column names as the first row
            //   0           – start importing at the first row of the PDF table (zero‑based)
            //   0           – start importing at the first column of the PDF table (zero‑based)
            table.ImportDataTable(sourceTable, true, 0, 0);

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the PDF – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF with table saved to '{outputPath}'.");
            }
            else
            {
                try
                {
                    doc.Save(outputPath);
                    Console.WriteLine($"PDF with table saved to '{outputPath}'.");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                                      "The PDF was generated in memory but could not be saved to disk.");
                }
            }
        }
    }

    // Helper that walks the exception chain looking for a DllNotFoundException (e.g., libgdiplus)
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
