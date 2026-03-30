using System;
using System.IO;
using System.Data;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string templatePath = "template.pdf";
        const string csvPath = "data.csv";
        const string mappingPath = "mapping.txt";
        const string outputPath = "filled.pdf";

        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template PDF not found: {templatePath}");
            return;
        }
        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"CSV data file not found: {csvPath}");
            return;
        }
        if (!File.Exists(mappingPath))
        {
            Console.Error.WriteLine($"Mapping file not found: {mappingPath}");
            return;
        }

        // Load column mapping: ExcelColumnName=PdfFieldName per line
        System.Collections.Generic.Dictionary<string, string> columnMap = new System.Collections.Generic.Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        using (StreamReader mapReader = new StreamReader(mappingPath))
        {
            string line;
            while ((line = mapReader.ReadLine()) != null)
            {
                line = line.Trim();
                if (line.Length == 0 || line.StartsWith("#"))
                    continue; // skip empty or comment lines
                string[] parts = line.Split('=');
                if (parts.Length == 2)
                {
                    string excelCol = parts[0].Trim();
                    string pdfField = parts[1].Trim();
                    if (!columnMap.ContainsKey(excelCol))
                        columnMap.Add(excelCol, pdfField);
                }
            }
        }

        // Read CSV header to determine columns
        DataTable dataTable = new DataTable();
        using (StreamReader csvReader = new StreamReader(csvPath))
        {
            string headerLine = csvReader.ReadLine();
            if (headerLine == null)
            {
                Console.Error.WriteLine("CSV file is empty.");
                return;
            }
            string[] excelColumns = headerLine.Split(',');
            // Create DataTable columns using mapped PDF field names (or original if no mapping)
            foreach (string excelColRaw in excelColumns)
            {
                string excelCol = excelColRaw.Trim();
                string pdfField;
                if (!columnMap.TryGetValue(excelCol, out pdfField))
                {
                    // If no mapping, use the Excel column name as PDF field name
                    pdfField = excelCol;
                }
                // Ensure column name is unique in DataTable
                string uniqueName = pdfField;
                int suffix = 1;
                while (dataTable.Columns.Contains(uniqueName))
                {
                    uniqueName = pdfField + "_" + suffix;
                    suffix++;
                }
                dataTable.Columns.Add(uniqueName, typeof(string));
            }

            // Read data rows
            string dataLine;
            while ((dataLine = csvReader.ReadLine()) != null)
            {
                if (dataLine.Trim().Length == 0)
                    continue; // skip empty lines
                string[] values = dataLine.Split(',');
                DataRow row = dataTable.NewRow();
                for (int i = 0; i < excelColumns.Length && i < values.Length; i++)
                {
                    string excelCol = excelColumns[i].Trim();
                    string pdfField;
                    if (!columnMap.TryGetValue(excelCol, out pdfField))
                        pdfField = excelCol;
                    // Find the actual column name in DataTable (may have suffix)
                    string dtColumnName = null;
                    foreach (DataColumn col in dataTable.Columns)
                    {
                        if (col.ColumnName.Equals(pdfField, StringComparison.OrdinalIgnoreCase) || col.ColumnName.StartsWith(pdfField + "_"))
                        {
                            dtColumnName = col.ColumnName;
                            break;
                        }
                    }
                    if (dtColumnName != null)
                        row[dtColumnName] = values[i].Trim();
                }
                dataTable.Rows.Add(row);
            }
        }

        // Fill PDF form using AutoFiller
        AutoFiller autoFiller = new AutoFiller();
        autoFiller.BindPdf(templatePath);
        autoFiller.ImportDataTable(dataTable);
        autoFiller.Save(outputPath);

        Console.WriteLine($"PDF form filled and saved to '{outputPath}'.");
    }
}
