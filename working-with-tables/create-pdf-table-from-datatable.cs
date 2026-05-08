using System;
using System.Data;
using System.Runtime.InteropServices;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // -------------------------------------------------------------------
        // Create an in‑memory DataTable with sample data.
        // -------------------------------------------------------------------
        DataTable dataTable = new DataTable();
        dataTable.Columns.Add("Id", typeof(int));
        dataTable.Columns.Add("Name", typeof(string));
        dataTable.Columns.Add("Quantity", typeof(int));

        dataTable.Rows.Add(1, "Apple", 10);
        dataTable.Rows.Add(2, "Banana", 20);
        dataTable.Rows.Add(3, "Cherry", 15);

        // Create a new PDF document and add a page
        using (Document pdfDocument = new Document())
        {
            Page page = pdfDocument.Pages.Add();

            // Create a table and import the DataTable into it
            Table table = new Table();
            table.ColumnWidths = "100 200 100"; // widths are in points
            table.ImportDataTable(dataTable, true, 0, 0);
            page.Paragraphs.Add(table);

            string outputPath = "output.pdf";

            // -------------------------------------------------------------------
            // Save the PDF – guard the call on non‑Windows platforms where libgdiplus
            // (required by Aspose.Pdf for GDI+) may be missing.
            // -------------------------------------------------------------------
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                pdfDocument.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
            else
            {
                try
                {
                    pdfDocument.Save(outputPath);
                    Console.WriteLine($"PDF saved to '{outputPath}'.");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF was not saved.");
                }
            }
        }

        Console.WriteLine("Program finished.");
    }

    // Helper that walks the inner‑exception chain looking for a DllNotFoundException.
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