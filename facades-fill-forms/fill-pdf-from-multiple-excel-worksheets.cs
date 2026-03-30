using System;
using System.Data;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputFolder = "input";          // Folder containing one CSV per worksheet
        const string templatePdf = "template.pdf";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }
        if (!File.Exists(templatePdf))
        {
            Console.Error.WriteLine($"Template PDF not found: {templatePdf}");
            return;
        }

        // Process every CSV file in the folder – each file represents a worksheet
        foreach (string csvPath in Directory.GetFiles(inputFolder, "*.csv"))
        {
            string sheetName = Path.GetFileNameWithoutExtension(csvPath);
            DataTable dataTable = LoadCsvToDataTable(csvPath, sheetName);

            // Fill PDF for this worksheet
            using (AutoFiller autoFiller = new AutoFiller())
            {
                autoFiller.BindPdf(templatePdf);
                autoFiller.ImportDataTable(dataTable);

                string safeSheetName = sheetName.Replace("$", "").Replace("'", "").Trim();
                string outputFile = $"{safeSheetName}_filled.pdf";
                autoFiller.Save(outputFile);
                Console.WriteLine($"Generated PDF for sheet '{safeSheetName}' -> {outputFile}");
            }
        }
    }

    /// <summary>
    /// Loads a CSV file into a DataTable. The first line is treated as the header row.
    /// </summary>
    /// <param name="csvFilePath">Full path to the CSV file.</param>
    /// <param name="tableName">Name to assign to the DataTable.</param>
    /// <returns>A populated DataTable.</returns>
    private static DataTable LoadCsvToDataTable(string csvFilePath, string tableName)
    {
        DataTable table = new DataTable(tableName);
        using (var reader = new StreamReader(csvFilePath))
        {
            if (reader.EndOfStream)
                return table; // empty file

            // Read header line
            string headerLine = reader.ReadLine();
            string[] headers = SplitCsvLine(headerLine);
            foreach (string header in headers)
            {
                // All columns are treated as string for simplicity – AutoFiller works with string values.
                table.Columns.Add(header.Trim(), typeof(string));
            }

            // Read data rows
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                    continue; // skip empty lines

                string[] fields = SplitCsvLine(line);
                DataRow row = table.NewRow();
                for (int i = 0; i < table.Columns.Count && i < fields.Length; i++)
                {
                    row[i] = fields[i].Trim();
                }
                table.Rows.Add(row);
            }
        }
        return table;
    }

    /// <summary>
    /// Very simple CSV splitter that respects quoted fields.
    /// </summary>
    private static string[] SplitCsvLine(string line)
    {
        var fields = new System.Collections.Generic.List<string>();
        bool inQuotes = false;
        var current = new System.Text.StringBuilder();
        for (int i = 0; i < line.Length; i++)
        {
            char c = line[i];
            if (c == '"')
            {
                // Toggle quote state, but handle escaped double quotes "" inside a quoted field
                if (inQuotes && i + 1 < line.Length && line[i + 1] == '"')
                {
                    current.Append('"');
                    i++; // skip the escaped quote
                }
                else
                {
                    inQuotes = !inQuotes;
                }
            }
            else if (c == ',' && !inQuotes)
            {
                fields.Add(current.ToString());
                current.Clear();
            }
            else
            {
                current.Append(c);
            }
        }
        fields.Add(current.ToString());
        return fields.ToArray();
    }
}
