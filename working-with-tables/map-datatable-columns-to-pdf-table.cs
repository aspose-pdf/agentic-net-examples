using System;
using System.Data;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Prepare a sample DataTable
        DataTable dataTable = new DataTable("Sample");
        dataTable.Columns.Add("Name", typeof(string));
        dataTable.Columns.Add("Age", typeof(int));
        dataTable.Columns.Add("Country", typeof(string));

        dataTable.Rows.Add("Alice", 30, "USA");
        dataTable.Rows.Add("Bob", 25, "Canada");
        dataTable.Rows.Add("Charlie", 35, "UK");

        // Define a mapping: DataTable column name -> target table column index (0‑based)
        // Example: target column 0 <- Name, column 1 <- Country, column 2 <- Age
        var columnMapping = new Dictionary<string, int>
        {
            { "Name", 0 },
            { "Country", 1 },
            { "Age", 2 }
        };

        // Determine the number of columns in the target table (maximum mapped index + 1)
        int targetColumnCount = 0;
        foreach (int idx in columnMapping.Values)
            if (idx + 1 > targetColumnCount) targetColumnCount = idx + 1;

        // Build sourceColumnList ordered by target column index
        int[] sourceColumnList = new int[targetColumnCount];
        foreach (var kvp in columnMapping)
        {
            // Guard against a missing column name – this removes the CS8602 warning.
            if (!dataTable.Columns.Contains(kvp.Key))
                throw new ArgumentException($"Column '{kvp.Key}' does not exist in the source DataTable.");

            int sourceOrdinal = dataTable.Columns[kvp.Key]!.Ordinal; // safe after the Contains check
            sourceColumnList[kvp.Value] = sourceOrdinal;
        }

        // Build sourceRowList to import all rows
        int[] sourceRowList = new int[dataTable.Rows.Count];
        for (int i = 0; i < sourceRowList.Length; i++)
            sourceRowList[i] = i;

        // Create a new PDF document
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document())
        {
            // Add a page
            Aspose.Pdf.Page page = doc.Pages.Add();

            // Create a table and set basic layout
            Aspose.Pdf.Table table = new Aspose.Pdf.Table
            {
                // Example: three equal width columns
                ColumnWidths = "150 150 150",
                // Optional visual styling
                Border = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Black),
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Gray),
                DefaultCellPadding = new MarginInfo { Top = 5, Bottom = 5, Left = 5, Right = 5 },
                DefaultCellTextState = new TextState { FontSize = 12, Font = FontRepository.FindFont("Helvetica") }
            };

            // Import the DataTable using the column mapping
            // showColumnNamesAsFirstRow = true (include column headers)
            // isHtmlSupported = false (plain text)
            table.ImportDataTable(
                dataTable,
                sourceRowList,
                sourceColumnList,
                firstFilledRow: 0,
                firstFilledColumn: 0,
                showColumnNamesAsFirstRow: true,
                isHtmlSupported: false);

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the PDF – guard against missing libgdiplus on macOS/Linux
            string outputPath = "MappedTable.pdf";
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
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF was not saved.");
                }
            }
        }

        Console.WriteLine("PDF generation completed.");
    }

    // Helper to detect a nested DllNotFoundException (e.g., missing libgdiplus)
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
