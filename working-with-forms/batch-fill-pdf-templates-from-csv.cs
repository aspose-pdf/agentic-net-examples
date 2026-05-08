using System;
using System.IO;
using System.Data;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class BatchPdfFiller
{
    static void Main()
    {
        // Paths – adjust as needed
        const string csvPath = "data.csv";               // CSV with data rows
        const string templatePath = "template.pdf";      // PDF template to fill
        const string outputFolder = "FilledOutputs";     // Folder for generated PDFs

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

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load CSV into a DataTable
        DataTable csvTable = LoadCsvIntoDataTable(csvPath);

        // Process each data row
        for (int rowIndex = 0; rowIndex < csvTable.Rows.Count; rowIndex++)
        {
            DataRow dataRow = csvTable.Rows[rowIndex];

            // Load the template PDF
            using (Document doc = new Document(templatePath))
            {
                // Create a new Table and import the single row
                Table table = new Table();
                // Build a temporary DataTable containing only the current row
                DataTable singleRowTable = csvTable.Clone(); // copy schema
                singleRowTable.ImportRow(dataRow);
                // Import data into the table (no column names, start at first cell)
                table.ImportDataTable(singleRowTable, false, 0, 0);

                // Add the table to the first page
                Page firstPage = doc.Pages[1];
                firstPage.Paragraphs.Add(table);

                // Save the filled PDF with a unique name
                string outputPath = Path.Combine(outputFolder, $"filled_{rowIndex + 1}.pdf");
                doc.Save(outputPath);
                Console.WriteLine($"Saved: {outputPath}");
            }
        }
    }

    // Helper: reads a CSV file (comma‑separated, first line contains headers) into a DataTable
    static DataTable LoadCsvIntoDataTable(string csvFilePath)
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
                    DataRow row = table.NewRow();
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