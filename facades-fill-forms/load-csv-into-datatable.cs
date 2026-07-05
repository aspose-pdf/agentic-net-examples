using System;
using System.Data;
using System.IO;

class Program
{
    static void Main()
    {
        const string csvPath = "input.csv";

        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"File not found: {csvPath}");
            return;
        }

        DataTable result = LoadCsvToDataTable(csvPath);

        // Example output: list column names and first data row.
        Console.WriteLine("Columns:");
        foreach (DataColumn col in result.Columns)
            Console.Write($"{col.ColumnName}\t");
        Console.WriteLine();

        if (result.Rows.Count > 0)
        {
            Console.WriteLine("First data row:");
            foreach (var item in result.Rows[0].ItemArray)
                Console.Write($"{item}\t");
            Console.WriteLine();
        }

        // The DataTable 'result' is now ready for form mapping (e.g., AutoFiller.ImportDataTable).
        // DataTable formMappingTable = result;
        // Further processing can be done here.
    }

    private static DataTable LoadCsvToDataTable(string csvFilePath)
    {
        var table = new DataTable();
        using (var reader = new StreamReader(csvFilePath))
        {
            bool isFirstLine = true;
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                var fields = line.Split(',');

                if (isFirstLine)
                {
                    // Use the first line as column headers.
                    foreach (var header in fields)
                    {
                        string colName = string.IsNullOrWhiteSpace(header) ? $"Column{table.Columns.Count}" : header.Trim();
                        // Ensure column names are unique.
                        string uniqueName = colName;
                        int suffix = 1;
                        while (table.Columns.Contains(uniqueName))
                        {
                            uniqueName = $"{colName}_{suffix++}";
                        }
                        table.Columns.Add(uniqueName, typeof(string));
                    }
                    isFirstLine = false;
                }
                else
                {
                    var row = table.NewRow();
                    for (int i = 0; i < table.Columns.Count && i < fields.Length; i++)
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