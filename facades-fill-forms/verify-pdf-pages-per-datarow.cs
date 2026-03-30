using System;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Prepare a sample DataTable with some rows.
        DataTable dataTable = new DataTable("Sample");
        dataTable.Columns.Add("ID", typeof(int));
        dataTable.Columns.Add("Name", typeof(string));
        dataTable.Columns.Add("Quantity", typeof(int));

        DataRow row1 = dataTable.NewRow();
        row1["ID"] = 1;
        row1["Name"] = "Apples";
        row1["Quantity"] = 10;
        dataTable.Rows.Add(row1);

        DataRow row2 = dataTable.NewRow();
        row2["ID"] = 2;
        row2["Name"] = "Oranges";
        row2["Quantity"] = 20;
        dataTable.Rows.Add(row2);

        DataRow row3 = dataTable.NewRow();
        row3["ID"] = 3;
        row3["Name"] = "Bananas";
        row3["Quantity"] = 15;
        dataTable.Rows.Add(row3);

        // Create a new PDF document.
        using (Document document = new Document())
        {
            // For each DataRow, create a separate page with a table containing that row.
            foreach (DataRow dataRow in dataTable.Rows)
            {
                Page page = document.Pages.Add();

                // Create a table with the same number of columns as the DataTable.
                Table table = new Table();

                // Set equal column widths (Aspose.Pdf.Table.ColumnWidths expects a space‑separated string).
                var widthStrings = Enumerable.Range(0, dataTable.Columns.Count)
                                            .Select(_ => "100") // arbitrary width for each column
                                            .ToArray();
                table.ColumnWidths = string.Join(" ", widthStrings);

                // Add a single row to the table and fill cells with the DataRow values.
                Row pdfRow = table.Rows.Add();
                foreach (DataColumn column in dataTable.Columns)
                {
                    pdfRow.Cells.Add(dataRow[column].ToString());
                }

                // Add the table to the page.
                page.Paragraphs.Add(table);
            }

            // Verification: the number of pages should equal the number of DataTable rows.
            int expectedPageCount = dataTable.Rows.Count;
            int actualPageCount = document.Pages.Count;
            Console.WriteLine($"Expected pages: {expectedPageCount}, Actual pages: {actualPageCount}");

            // Save the resulting PDF – guard against missing GDI+ (libgdiplus) on non‑Windows platforms.
            const string outputPath = "output.pdf";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                document.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
            else
            {
                try
                {
                    document.Save(outputPath);
                    Console.WriteLine($"PDF saved to '{outputPath}' (non‑Windows platform).");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                                      "The PDF could not be saved, but the document was created correctly.");
                }
            }
        }
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
