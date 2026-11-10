using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string csvPath = "data.csv";      // CSV version of the Excel data
        const string pdfTemplate = "template.pdf";
        const string outputDir = "FilledPdfs";

        // Verify input files
        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"CSV file not found: {csvPath}");
            return;
        }
        if (!File.Exists(pdfTemplate))
        {
            Console.Error.WriteLine($"PDF template not found: {pdfTemplate}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Stream the CSV file line‑by‑line to keep memory usage low
        using (var reader = new StreamReader(csvPath))
        {
            // Read header line and build a DataTable schema
            string headerLine = reader.ReadLine();
            if (headerLine == null)
            {
                Console.WriteLine("CSV file is empty.");
                return;
            }

            string[] headers = SplitCsvLine(headerLine);
            DataTable schemaTable = BuildSchemaTable(headers);

            int rowIndex = 1; // start counting after header
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] fields = SplitCsvLine(line);
                // Create a one‑row DataTable that matches the schema
                DataTable rowTable = schemaTable.Clone();
                DataRow dr = rowTable.NewRow();
                for (int i = 0; i < headers.Length; i++)
                {
                    dr[i] = i < fields.Length ? fields[i] : string.Empty;
                }
                rowTable.Rows.Add(dr);

                // Fill PDF for this row using the modern AutoFiller API
                AutoFiller filler = new AutoFiller();
                filler.BindPdf(pdfTemplate);
                // ImportDataTable is the correct method – BindDataSource was removed
                filler.ImportDataTable(rowTable);
                // No Process() method exists in recent versions; Save performs the fill operation
                string outputPath = Path.Combine(outputDir, $"Filled_{rowIndex}.pdf");
                filler.Save(outputPath);

                if (rowIndex % 1000 == 0)
                    Console.WriteLine($"{rowIndex} rows processed...");

                rowIndex++;
            }
        }

        Console.WriteLine("All rows have been processed and PDFs generated.");
    }

    // Helper: builds a DataTable schema where every column is of type string
    private static DataTable BuildSchemaTable(string[] columnNames)
    {
        var dt = new DataTable();
        foreach (var col in columnNames)
        {
            string colName = string.IsNullOrWhiteSpace(col) ? "Column" + (dt.Columns.Count + 1) : col;
            dt.Columns.Add(colName, typeof(string));
        }
        return dt;
    }

    // Very simple CSV splitter – handles commas inside quoted fields
    private static string[] SplitCsvLine(string line)
    {
        var fields = new List<string>();
        bool inQuotes = false;
        var current = new System.Text.StringBuilder();
        for (int i = 0; i < line.Length; i++)
        {
            char c = line[i];
            if (c == '"')
            {
                // Look ahead for escaped quote
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
