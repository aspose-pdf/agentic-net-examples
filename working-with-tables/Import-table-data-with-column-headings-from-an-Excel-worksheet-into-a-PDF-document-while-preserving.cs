using System;
using System.Data;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string csvPath   = "Data.csv";          // CSV file instead of Excel
        const string pdfPath   = "TableFromCsv.pdf";

        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"CSV file not found: {csvPath}");
            return;
        }

        // Load CSV worksheet into a DataTable
        DataTable dataTable = LoadCsvToDataTable(csvPath);

        // Create a new PDF document
        using (Document pdfDoc = new Document())
        {
            // Add a page to host the table
            Page page = pdfDoc.Pages.Add();

            // Create an Aspose.Pdf Table
            Table pdfTable = new Table
            {
                // Optional: set table appearance
                Border = new BorderInfo(BorderSide.All, 0.5f, Color.Black),
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Color.Gray),
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5)
            };

            // Import the DataTable – first row will contain column headings
            pdfTable.ImportDataTable(dataTable, true, 0, 0);

            // Add the table to the page
            page.Paragraphs.Add(pdfTable);

            // Save the PDF
            pdfDoc.Save(pdfPath);
        }

        Console.WriteLine($"PDF created: {pdfPath}");
    }

    // Reads a CSV file into a DataTable. The first line is treated as column headings.
    private static DataTable LoadCsvToDataTable(string csvFilePath)
    {
        var dt = new DataTable();
        using (var reader = new StreamReader(csvFilePath))
        {
            bool isFirstLine = true;
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (line == null) continue;
                // Simple CSV split – handles commas inside quotes minimally
                var fields = ParseCsvLine(line);

                if (isFirstLine)
                {
                    // Create columns from header row
                    foreach (var header in fields)
                    {
                        var columnName = string.IsNullOrWhiteSpace(header) ? "Column" + dt.Columns.Count : header.Trim();
                        dt.Columns.Add(columnName, typeof(string));
                    }
                    isFirstLine = false;
                }
                else
                {
                    // Add data rows
                    var row = dt.NewRow();
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        row[i] = i < fields.Length ? fields[i] : null;
                    }
                    dt.Rows.Add(row);
                }
            }
        }
        return dt;
    }

    // Minimal CSV line parser that respects quoted fields containing commas.
    private static string[] ParseCsvLine(string line)
    {
        var result = new System.Collections.Generic.List<string>();
        bool inQuotes = false;
        var value = string.Empty;
        for (int i = 0; i < line.Length; i++)
        {
            char c = line[i];
            if (c == '"')
            {
                if (inQuotes && i + 1 < line.Length && line[i + 1] == '"')
                {
                    // Escaped quote inside quoted field
                    value += '"';
                    i++; // skip next quote
                }
                else
                {
                    inQuotes = !inQuotes; // toggle state
                }
            }
            else if (c == ',' && !inQuotes)
            {
                result.Add(value);
                value = string.Empty;
            }
            else
            {
                value += c;
            }
        }
        result.Add(value); // add last field
        return result.ToArray();
    }
}
