using System;
using System.Data;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;

class DynamicTablePdf
{
    static void Main()
    {
        // Output PDF file path
        const string outputPath = "DynamicTable.pdf";

        // Create sample data in a DataTable (dynamic number of rows)
        DataTable data = new DataTable("SampleData");
        data.Columns.Add("ID", typeof(int));
        data.Columns.Add("Name", typeof(string));
        data.Columns.Add("Quantity", typeof(int));
        data.Columns.Add("Price", typeof(decimal));

        // Populate the DataTable with variable number of rows
        for (int i = 1; i <= 15; i++) // change the upper bound to adjust row count
        {
            data.Rows.Add(i, $"Item {i}", i * 2, Math.Round((decimal)(i * 1.99), 2));
        }

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table and set basic appearance
            Table table = new Table
            {
                // Optional: set table border and background
                Border = new BorderInfo(BorderSide.All, 1f, Aspose.Pdf.Color.Black),
                BackgroundColor = Aspose.Pdf.Color.LightGray,
                // Optional: set column widths (percentage of page width)
                ColumnWidths = "20 30 25 25"
            };

            // Import the DataTable into the Aspose.Pdf.Table
            // Parameters: (DataTable, isColumnNamesImported, firstFilledRow, firstFilledColumn)
            table.ImportDataTable(data, true, 0, 0);

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the PDF – guard against missing GDI+ on non‑Windows platforms
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
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF was not saved.");
                }
            }
        }
    }

    // Helper to detect missing native GDI+ library in nested exceptions
    private static bool ContainsDllNotFound(Exception ex)
    {
        Exception? current = ex;
        while (current != null)
        {
            if (current is DllNotFoundException)
                return true;
            current = current.InnerException;
        }
        return false;
    }
}