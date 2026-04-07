using System;
using System.Data;
using System.Runtime.InteropServices;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Output PDF path
        const string outputPdfPath = "ProductsTable.pdf";

        // ------------------------------------------------------------
        // Create an in‑memory DataTable with sample data.
        // ------------------------------------------------------------
        DataTable dataTable = new DataTable();
        dataTable.Columns.Add("Id", typeof(int));
        dataTable.Columns.Add("Name", typeof(string));
        dataTable.Columns.Add("Quantity", typeof(int));

        // Sample rows – replace or extend as needed for your scenario
        dataTable.Rows.Add(1, "Apple", 50);
        dataTable.Rows.Add(2, "Banana", 30);
        dataTable.Rows.Add(3, "Orange", 20);

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table and set basic appearance
            Table table = new Table
            {
                // ColumnWidths expects a space‑separated string, not a float array
                ColumnWidths = "100 200 100",
                Border = new BorderInfo(BorderSide.All, 0.5f),
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5)
            };

            // Import the DataTable into the Aspose.Pdf.Table
            // Parameters: (DataTable, import column names as first row, first row index, first column index)
            table.ImportDataTable(dataTable, true, 0, 0);

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the PDF document – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPdfPath);
                Console.WriteLine($"PDF saved to '{outputPdfPath}'.");
            }
            else
            {
                try
                {
                    doc.Save(outputPdfPath);
                    Console.WriteLine($"PDF saved to '{outputPdfPath}'.");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                                      "The PDF was generated in memory but could not be saved to disk.");
                }
            }
        }
    }

    // Helper that walks the inner‑exception chain looking for a DllNotFoundException
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
