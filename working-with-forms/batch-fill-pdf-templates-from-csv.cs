using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

// Alias to disambiguate the Table class from Aspose.Pdf.Text (if present)
using AsposePdfTable = Aspose.Pdf.Table;

class BatchPdfFiller
{
    static void Main()
    {
        // Paths – adjust as needed
        const string csvPath = "data.csv";               // CSV with data rows
        const string templatePath = "template.pdf";      // PDF template to fill
        const string outputDir = "FilledOutputs";        // Directory for results

        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"CSV file not found: {csvPath}");
            return;
        }
        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template PDF not found: {templatePath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Load CSV into a DataTable
        DataTable csvTable = LoadCsvIntoDataTable(csvPath);

        // Iterate over each data row and generate a filled PDF
        for (int i = 0; i < csvTable.Rows.Count; i++)
        {
            DataRow row = csvTable.Rows[i];

            // Create a DataTable containing only the current row (including column names)
            DataTable singleRowTable = csvTable.Clone(); // copies schema
            singleRowTable.ImportRow(row);

            // Load the template PDF inside a using block (ensures disposal)
            using (Document doc = new Document(templatePath))
            {
                // Ensure the document has at least one page
                Page page = doc.Pages.Count > 0 ? doc.Pages[1] : doc.Pages.Add();

                // Create a table and import the data (include column names as first row)
                AsposePdfTable table = new AsposePdfTable();
                table.ImportDataTable(singleRowTable, true, 0, 0);

                // Optional: style the table via GraphInfo (example: set border)
                table.Border = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Black);

                // Auto‑fit columns to content (optional, replaces removed ColumnCount usage)
                table.ColumnAdjustment = ColumnAdjustment.AutoFitToContent;

                // Add the table to the page
                page.Paragraphs.Add(table);

                // Build output file name
                string outputPath = Path.Combine(outputDir, $"filled_{i + 1}.pdf");

                // Save the filled PDF
                doc.Save(outputPath);
                Console.WriteLine($"Saved: {outputPath}");
            }
        }
    }

    // Helper: reads a CSV file (comma‑separated, first line = headers) into a DataTable
    private static DataTable LoadCsvIntoDataTable(string csvFilePath)
    {
        DataTable table = new DataTable();

        using (StreamReader reader = new StreamReader(csvFilePath))
        {
            bool isFirstLine = true;
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                string[] fields = line.Split(',');

                if (isFirstLine)
                {
                    // Create columns from header row
                    foreach (string header in fields)
                    {
                        table.Columns.Add(header.Trim());
                    }
                    isFirstLine = false;
                }
                else
                {
                    // Add data row
                    DataRow dataRow = table.NewRow();
                    for (int i = 0; i < fields.Length && i < table.Columns.Count; i++)
                    {
                        dataRow[i] = fields[i].Trim();
                    }
                    table.Rows.Add(dataRow);
                }
            }
        }

        return table;
    }
}
