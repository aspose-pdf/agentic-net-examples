using System;
using System.Data;
using System.Runtime.InteropServices;
using Aspose.Pdf;

namespace AsposePdfExamples
{
    class ImportDataViewExample
    {
        static void Main(string[] args)
        {
            const string outputPath = "output.pdf";

            // Create a new PDF document
            using (Document doc = new Document())
            {
                // Add a page to the document
                Page page = doc.Pages.Add();

                // Create a table with two columns
                Table table = new Table();
                // ColumnWidths expects a space‑separated string, not a float array
                table.ColumnWidths = "150 150";

                // Prepare source data in a DataTable
                DataTable dataTable = new DataTable("Sample");
                dataTable.Columns.Add("ID", typeof(int));
                dataTable.Columns.Add("Name", typeof(string));
                dataTable.Rows.Add(1, "Alice");
                dataTable.Rows.Add(2, "Bob");
                dataTable.Rows.Add(3, "Charlie");
                dataTable.Rows.Add(4, "Diana");

                // Create a DataView with a filter to include only rows where ID > 2
                DataView dataView = new DataView(dataTable);
                dataView.RowFilter = "ID > 2";

                // Import the filtered data into the table
                // Parameters: sourceDataView, isColumnNamesImported, firstFilledRow, firstFilledColumn, maxRows, maxColumns
                table.ImportDataView(dataView, true, 0, 0, dataView.Count, dataView.Table.Columns.Count);

                // Add the table to the page
                page.Paragraphs.Add(table);

                // Save the PDF document – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
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
                        Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                                          "The PDF was generated without GDI+ dependent features.");
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
}
