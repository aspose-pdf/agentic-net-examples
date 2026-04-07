using System;
using System.Data;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Aspose.Pdf;

namespace AsposePdfExamples
{
    class MapDataTableColumnsToPdfTable
    {
        static void Main(string[] args)
        {
            // Create a new PDF document
            using (Document doc = new Document())
            {
                // Add a page to the document
                Page page = doc.Pages.Add();

                // Create a sample DataTable with four columns
                DataTable dt = new DataTable();
                dt.Columns.Add("Id", typeof(int));
                dt.Columns.Add("Name", typeof(string));
                dt.Columns.Add("Age", typeof(int));
                dt.Columns.Add("Country", typeof(string));

                dt.Rows.Add(1, "Alice", 30, "USA");
                dt.Rows.Add(2, "Bob", 25, "UK");
                dt.Rows.Add(3, "Charlie", 28, "Canada");

                // Define a mapping from source column name to target column index in the PDF table
                Dictionary<string, int> columnMapping = new Dictionary<string, int>
                {
                    { "Name", 0 },      // Source "Name" goes to first column
                    { "Country", 1 }    // Source "Country" goes to second column
                };

                // Build the source column list in the order required by the target table
                int targetColumnCount = columnMapping.Count;
                int[] sourceColumnList = new int[targetColumnCount];
                foreach (KeyValuePair<string, int> kvp in columnMapping)
                {
                    int sourceIndex = dt.Columns[kvp.Key].Ordinal;
                    sourceColumnList[kvp.Value] = sourceIndex;
                }

                // Build the source row list (all rows in the DataTable)
                int rowCount = dt.Rows.Count;
                int[] sourceRowList = new int[rowCount];
                for (int i = 0; i < rowCount; i++)
                {
                    sourceRowList[i] = i;
                }

                // Create a PDF table with two columns
                Table table = new Table();
                // ColumnWidths expects a space‑separated string, not a float array
                table.ColumnWidths = "150 150";

                // Import the selected columns according to the mapping
                table.ImportDataTable(dt, sourceRowList, sourceColumnList, 0, 0, false, false);

                // Add the table to the page
                page.Paragraphs.Add(table);

                // Save the PDF document – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
                string outputPath = "output.pdf";
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
                        Console.WriteLine($"PDF saved to '{outputPath}'. (non‑Windows platform, libgdiplus may be required)");
                    }
                    catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                    {
                        Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                                          "The PDF could not be saved, but the rest of the code executed correctly.");
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
