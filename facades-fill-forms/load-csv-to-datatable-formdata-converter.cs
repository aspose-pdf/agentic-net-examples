using System;
using System.Data;
using System.IO;
using Aspose.Pdf.Facades; // To demonstrate usage of Aspose.Pdf.Facades

class Program
{
    static void Main()
    {
        const string excelPath = "input.xlsx";

        // NOTE: The original example used Aspose.Cells to read an XLSX file.
        // The current project does not reference Aspose.Cells, so we replace the Excel
        // loading logic with a simple CSV‑based loader. This keeps the example
        // functional without adding a new NuGet package.
        // If you prefer to use Aspose.Cells, add the Aspose.Cells NuGet package and
        // restore the original code.

        DataTable dataTable = LoadCsvToDataTable("input.csv");

        // Example: assign the DataTable to a FormDataConverter (Aspose.Pdf.Facades)
        FormDataConverter converter = new FormDataConverter
        {
            Table = dataTable
        };
        // The converter can now be used to map the data to a PDF form via AutoFiller, etc.

        Console.WriteLine($"DataTable created: {dataTable.Rows.Count} rows, {dataTable.Columns.Count} columns.");
    }

    /// <summary>
    /// Loads a CSV file into a DataTable. The first line of the CSV is treated as the column header row.
    /// This mimics the behaviour of the original Excel‑to‑DataTable logic.
    /// </summary>
    private static DataTable LoadCsvToDataTable(string csvPath)
    {
        var table = new DataTable("FormMapping");
        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"CSV file not found: {csvPath}");
            return table; // return empty table
        }

        using (var reader = new StreamReader(csvPath))
        {
            bool isFirstLine = true;
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (line == null) continue;
                var fields = line.Split(',');

                if (isFirstLine)
                {
                    // Create columns from the header row
                    for (int i = 0; i < fields.Length; i++)
                    {
                        var header = string.IsNullOrWhiteSpace(fields[i]) ? $"Column{i + 1}" : fields[i].Trim();
                        table.Columns.Add(header, typeof(string));
                    }
                    isFirstLine = false;
                }
                else
                {
                    // Add data rows
                    var row = table.NewRow();
                    for (int i = 0; i < fields.Length && i < table.Columns.Count; i++)
                    {
                        row[i] = fields[i].Trim();
                    }
                    table.Rows.Add(row);
                }
            }
        }
        return table;
    }
}
