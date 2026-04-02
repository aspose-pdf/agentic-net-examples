using System;
using System.Data;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "rotated_table.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Prepare sample data for the table
            DataTable data = new DataTable();
            data.Columns.Add("Column 1");
            data.Columns.Add("Column 2");
            data.Rows.Add("Row1‑Col1", "Row1‑Col2");
            data.Rows.Add("Row2‑Col1", "Row2‑Col2");
            data.Rows.Add("Row3‑Col1", "Row3‑Col2");

            // Create a table and import the data
            Table table = new Table();
            table.ColumnWidths = "200 200"; // two columns, each 200 points wide
            table.ImportDataTable(data, true, 0, 0);

            // ------------------------------------------------------------
            // Apply a 90‑degree rotation matrix to the page before adding the table
            // ------------------------------------------------------------
            double pageHeight = page.GetPageRect(false).Height;

            // The Matrix class exposes the six components (A‑F). Modify them directly.
            Matrix m = page.RotationMatrix;
            m.A = 0;               // a
            m.B = 1;               // b
            m.C = -1;              // c
            m.D = 0;               // d
            m.E = pageHeight;      // e (translation in X after rotation)
            m.F = 0;               // f (translation in Y)

            // Add the table to the page after the transformation is set
            page.Paragraphs.Add(table);

            // ------------------------------------------------------------
            // Save the PDF – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
            // ------------------------------------------------------------
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF with rotated table saved to '{outputPath}'.");
            }
            else
            {
                try
                {
                    doc.Save(outputPath);
                    Console.WriteLine($"PDF saved (non‑Windows platform) to '{outputPath}'.");
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
