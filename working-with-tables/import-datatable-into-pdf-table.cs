using System;
using System.Data;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;                     // Core Aspose.Pdf namespace
using Aspose.Pdf.Text;               // For text related classes if needed

class Program
{
    static void Main()
    {
        // Prepare sample data in a DataTable
        DataTable dt = new DataTable("Sample");
        dt.Columns.Add("ID", typeof(int));
        dt.Columns.Add("Name", typeof(string));
        dt.Columns.Add("Quantity", typeof(int));

        dt.Rows.Add(1, "Apples", 10);
        dt.Rows.Add(2, "Bananas", 20);
        dt.Rows.Add(3, "Cherries", 15);

        const string outputPath = "DataTableExport.pdf";

        // Create a PDF document and add a page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Create a Table and configure basic appearance
            Table table = new Table
            {
                // Define column widths (space‑separated values)
                ColumnWidths = "100 200 100",
                // Optional: set a border for the whole table
                Border = new BorderInfo(BorderSide.All, 0.5f)
            };

            // Import the DataTable into the Aspose.Pdf.Table
            // Parameters:
            //   dt                – source DataTable
            //   true              – import column names as the first row
            //   0                 – start importing at the first row of the target table (zero‑based)
            //   0                 – start importing at the first column of the target table (zero‑based)
            table.ImportDataTable(dt, true, 0, 0);

            // Add the populated table to the page
            page.Paragraphs.Add(table);

            // Save the document – guard against missing GDI+ on non‑Windows platforms
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

    // Helper to detect a nested DllNotFoundException (e.g., missing libgdiplus)
    private static bool ContainsDllNotFound(Exception ex)
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