using System;
using System.Data;
using System.Runtime.InteropServices;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Create a sample DataTable with three rows.
        DataTable dataTable = new DataTable("Sample");
        dataTable.Columns.Add("ID", typeof(int));
        dataTable.Columns.Add("Name", typeof(string));
        dataTable.Columns.Add("Value", typeof(double));

        DataRow row1 = dataTable.NewRow();
        row1["ID"] = 1;
        row1["Name"] = "First";
        row1["Value"] = 10.5;
        dataTable.Rows.Add(row1);

        DataRow row2 = dataTable.NewRow();
        row2["ID"] = 2;
        row2["Name"] = "Second";
        row2["Value"] = 20.75;
        dataTable.Rows.Add(row2);

        DataRow row3 = dataTable.NewRow();
        row3["ID"] = 3;
        row3["Name"] = "Third";
        row3["Value"] = 30.0;
        dataTable.Rows.Add(row3);

        // Create a PDF document where each DataTable row occupies its own page.
        using (Document doc = new Document())
        {
            int columnCount = dataTable.Columns.Count;
            double[] columnWidths = new double[columnCount];
            for (int c = 0; c < columnCount; c++)
            {
                columnWidths[c] = 100; // fixed width for simplicity
            }

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                Page page = doc.Pages.Add();
                Table table = new Table();
                // Table.ColumnWidths expects a space‑separated string.
                table.ColumnWidths = string.Join(" ", columnWidths);

                int[] sourceRows = new int[] { i };
                int[] sourceColumns = new int[columnCount];
                for (int c = 0; c < columnCount; c++)
                {
                    sourceColumns[c] = c;
                }

                // Import only the current row into the table.
                table.ImportDataTable(dataTable, sourceRows, sourceColumns, 0, 0, false, false);
                page.Paragraphs.Add(table);
            }

            string outputPath = "output.pdf";

            // Guard Document.Save against missing GDI+ (libgdiplus) on non‑Windows platforms.
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
                    Console.WriteLine($"PDF saved to '{outputPath}'. (non‑Windows platform, libgdiplus present)");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF was not saved.");
                }
            }

            // Verify that the number of pages matches the number of DataTable rows.
            int expectedPages = dataTable.Rows.Count;
            int actualPages = doc.Pages.Count;
            if (actualPages == expectedPages)
            {
                Console.WriteLine("Verification succeeded: PDF contains {0} pages as expected.", actualPages);
            }
            else
            {
                Console.WriteLine("Verification failed: PDF contains {0} pages, expected {1}.", actualPages, expectedPages);
            }
        }
    }

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
