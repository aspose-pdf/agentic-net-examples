using System;
using System.Data;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string templatePdfPath = "template.pdf";
        const string csvFolderPath = "sheets"; // Folder containing one CSV per worksheet
        const string outputFolder = "output";

        if (!File.Exists(templatePdfPath))
        {
            Console.Error.WriteLine($"Template PDF not found: {templatePdfPath}");
            return;
        }
        if (!Directory.Exists(csvFolderPath))
        {
            Console.Error.WriteLine($"CSV folder not found: {csvFolderPath}");
            return;
        }
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Process each CSV file – each file represents one worksheet.
        foreach (string csvFilePath in Directory.GetFiles(csvFolderPath, "*.csv"))
        {
            string sheetName = Path.GetFileNameWithoutExtension(csvFilePath);
            DataTable worksheetTable = LoadCsvToDataTable(csvFilePath);

            // Use AutoFiller to generate a PDF for the current worksheet.
            using (AutoFiller autoFiller = new AutoFiller())
            {
                autoFiller.BindPdf(templatePdfPath);
                autoFiller.GeneratingPath = outputFolder + Path.DirectorySeparatorChar;
                autoFiller.BasicFileName = sheetName;
                autoFiller.ImportDataTable(worksheetTable);
                autoFiller.Save();
            }
        }

        Console.WriteLine("PDF generation completed.");
    }

    /// <summary>
    /// Loads a CSV file into a DataTable. The first line is treated as the header row.
    /// </summary>
    private static DataTable LoadCsvToDataTable(string csvFilePath)
    {
        var table = new DataTable(Path.GetFileNameWithoutExtension(csvFilePath));
        using (var reader = new StreamReader(csvFilePath))
        {
            // Read header line
            if (reader.EndOfStream)
                return table; // empty file

            string headerLine = reader.ReadLine();
            string[] headers = SplitCsvLine(headerLine);
            foreach (string header in headers)
            {
                // Use string type for all columns – AutoFiller works with string values.
                table.Columns.Add(header.Trim(), typeof(string));
            }

            // Read data rows
            while (!reader.EndOfStream)
            {
                string dataLine = reader.ReadLine();
                string[] fields = SplitCsvLine(dataLine);
                var row = table.NewRow();
                for (int i = 0; i < headers.Length && i < fields.Length; i++)
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
        if (string.IsNullOrEmpty(line))
            return new string[0];

        var fields = new System.Collections.Generic.List<string>();
        int i = 0;
        while (i < line.Length)
        {
            if (line[i] == '"')
            {
                // Quoted field
                i++; // skip opening quote
                int start = i;
                while (i < line.Length)
                {
                    if (line[i] == '"')
                    {
                        // Look ahead for escaped quote
                        if (i + 1 < line.Length && line[i + 1] == '"')
                        {
                            i += 2; // skip escaped quote
                        }
                        else
                        {
                            break; // end of quoted field
                        }
                    }
                    else
                    {
                        i++;
                    }
                }
                // Replace double double‑quotes with a single double‑quote
                string field = line.Substring(start, i - start).Replace("\"\"", "\"");
                fields.Add(field);
                // Skip closing quote
                i++;
                // Skip following delimiter, if any
                if (i < line.Length && line[i] == ',') i++;
            }
            else
            {
                // Unquoted field
                int start = i;
                while (i < line.Length && line[i] != ',') i++;
                string field = line.Substring(start, i - start);
                fields.Add(field);
                if (i < line.Length && line[i] == ',') i++;
            }
        }
        return fields.ToArray();
    }
}
