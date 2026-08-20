using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Prepare sample DataTable
        DataTable dataTable = new DataTable();
        dataTable.Columns.Add("Id", typeof(int));
        dataTable.Columns.Add("Name", typeof(string));
        dataTable.Columns.Add("Age", typeof(int));
        dataTable.Columns.Add("Country", typeof(string));

        dataTable.Rows.Add(1, "Alice", 30, "USA");
        dataTable.Rows.Add(2, "Bob",   25, "UK");
        dataTable.Rows.Add(3, "Carol", 28, "Canada");

        // Mapping: DataTable column name -> target Table column index
        // Example: map "Name" to column 0, "Age" to column 1, "Country" to column 2
        var columnMapping = new Dictionary<string, int>
        {
            { "Name",    0 },
            { "Age",     1 },
            { "Country", 2 }
        };

        // Determine the number of target columns (max index + 1)
        int targetColumnCount = columnMapping.Values.Max() + 1;

        // Build source column list ordered by target column index
        int[] sourceColumnList = columnMapping
                                    .OrderBy(kv => kv.Value)                     // sort by target index
                                    .Select(kv => dataTable.Columns[kv.Key]!.Ordinal) // get DataTable column ordinal (null‑forgiving)
                                    .ToArray();

        // Build source row list (all rows)
        int[] sourceRowList = Enumerable.Range(0, dataTable.Rows.Count).ToArray();

        // Create PDF document
        using (Document doc = new Document())
        {
            // Add a page
            Page page = doc.Pages.Add();

            // Create a table and define column widths (optional)
            Table table = new Table();
            // Table.ColumnWidths expects a string like "100 100 100" – create a simple equal‑width definition
            table.ColumnWidths = string.Join(" ", Enumerable.Repeat("100", targetColumnCount));

            // Import data using the mapping
            // Parameters:
            //   dataTable          - source DataTable
            //   sourceRowList      - rows to import
            //   sourceColumnList   - columns to import in the order defined by mapping
            //   firstFilledRow     - start at first row of the table (0‑based)
            //   firstFilledColumn  - start at first column of the table (0‑based)
            //   showColumnNamesAsFirstRow - import column names as header row
            //   isHtmlSupported    - false (plain text)
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

            // Save the PDF
            doc.Save("MappedTable.pdf");
        }

        Console.WriteLine("PDF with mapped table saved as 'MappedTable.pdf'.");
    }
}
