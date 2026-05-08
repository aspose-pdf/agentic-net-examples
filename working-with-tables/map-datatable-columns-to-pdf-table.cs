using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing; // for Color, BorderInfo, etc.

class Program
{
    static void Main()
    {
        // Input PDF (existing or new) and output PDF paths
        const string inputPdf  = "template.pdf";   // can be an empty PDF or a template page
        const string outputPdf = "mapped_table.pdf";

        // Sample DataTable with some data
        DataTable dt = new DataTable();
        dt.Columns.Add("ID",   typeof(int));
        dt.Columns.Add("Name", typeof(string));
        dt.Columns.Add("Age",  typeof(int));
        dt.Columns.Add("City", typeof(string));

        dt.Rows.Add(1, "Alice", 30, "London");
        dt.Rows.Add(2, "Bob",   25, "Paris");
        dt.Rows.Add(3, "Carol", 28, "Berlin");

        // Mapping: DataTable column name -> target Table column index (zero‑based)
        // Example: map "Name" to column 0, "Age" to column 2, "City" to column 4
        var columnMap = new Dictionary<string, int>
        {
            { "Name", 0 },
            { "Age",  2 },
            { "City", 4 }
        };

        // Ensure the target table has enough columns.
        // Here we create 5 columns (indices 0‑4) with equal widths.
        int totalTargetColumns = columnMap.Values.Max() + 1;
        var columnWidths = Enumerable.Repeat(100f, totalTargetColumns).ToArray();
        // Aspose.Pdf.Table.ColumnWidths expects a space‑separated string, not a float array.
        string columnWidthsString = string.Join(" ", columnWidths.Select(w => w.ToString(CultureInfo.InvariantCulture)));

        // Load the template PDF if it exists; otherwise create a new empty document.
        Document doc;
        if (File.Exists(inputPdf))
        {
            doc = new Document(inputPdf);
        }
        else
        {
            doc = new Document();
            // Ensure at least one page exists.
            doc.Pages.Add();
        }

        // Create a table and set its column widths
        Table table = new Table
        {
            ColumnWidths = columnWidthsString,
            DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Color.Black) // Aspose.Pdf.Drawing.Color
        };

        // Prepare a list of all source row indices (zero‑based)
        int[] sourceRows = Enumerable.Range(0, dt.Rows.Count).ToArray();

        // Import each mapped column individually
        foreach (var kvp in columnMap)
        {
            string sourceColumnName = kvp.Key;
            int targetColumnIndex   = kvp.Value;

            // Guard against missing columns in the DataTable
            if (!dt.Columns.Contains(sourceColumnName))
                continue; // or throw, depending on requirements

            // Get the zero‑based index of the source column in the DataTable
            int sourceColumnIndex = dt.Columns[sourceColumnName].Ordinal;

            // Import only this column (sourceColumnList contains a single index)
            int[] sourceColumns = new int[] { sourceColumnIndex };

            // Import the column data into the table.
            table.ImportDataTable(
                dt,
                sourceRows,
                sourceColumns,
                firstFilledRow: 0,
                firstFilledColumn: targetColumnIndex,
                showColumnNamesAsFirstRow: false,
                isHtmlSupported: false);
        }

        // Ensure the document has at least one page; if not, add a blank page.
        if (doc.Pages.Count == 0)
            doc.Pages.Add();

        // Add the table to the first page (index 1 in Aspose – pages are 1‑based).
        // Use the null‑forgiving operator because Aspose guarantees a Page object when Count > 0.
        doc.Pages[1]!.Paragraphs.Add(table);

        // Save the resulting PDF – guard against missing libgdiplus on non‑Windows platforms.
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            doc.Save(outputPdf);
        }
        else
        {
            try
            {
                doc.Save(outputPdf);
            }
            catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
            {
                Console.WriteLine("Warning: libgdiplus (GDI+) is not available on this platform. PDF was not saved.");
            }
        }

        Console.WriteLine($"PDF with mapped table processing completed. Output path: '{outputPdf}'.");
    }

    // Helper to detect a nested DllNotFoundException (e.g., missing libgdiplus).
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
