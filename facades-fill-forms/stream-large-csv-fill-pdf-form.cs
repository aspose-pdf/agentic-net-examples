using System;
using System.Data;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    // Streams a CSV file line‑by‑line into a DataTable.
    // This avoids loading the entire file into memory at once.
    static DataTable LoadCsvToDataTable(string csvFilePath)
    {
        var table = new DataTable("Data");
        using (var reader = new StreamReader(csvFilePath))
        {
            bool firstLine = true;
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (line == null) continue;

                // Simple CSV split – assumes no commas inside quoted fields.
                string[] values = line.Split(',');

                if (firstLine)
                {
                    // Create columns from header row.
                    foreach (string header in values)
                        table.Columns.Add(header.Trim(), typeof(string));
                    firstLine = false;
                }
                else
                {
                    // Add a new row.
                    DataRow row = table.NewRow();
                    for (int i = 0; i < values.Length && i < table.Columns.Count; i++)
                        row[i] = values[i].Trim();
                    table.Rows.Add(row);
                }
            }
        }
        return table;
    }

    static void Main()
    {
        const string csvPath       = "data.csv";      // CSV exported from the large XLSX file.
        const string templatePdf   = "template.pdf";  // PDF with form fields matching column names.
        const string outputPdfPath = "filled_output.pdf";

        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"CSV file not found: {csvPath}");
            return;
        }

        if (!File.Exists(templatePdf))
        {
            Console.Error.WriteLine($"Template PDF not found: {templatePdf}");
            return;
        }

        // Load data in a streaming fashion.
        DataTable dataTable = LoadCsvToDataTable(csvPath);

        // Use AutoFiller to merge the data into the PDF.
        using (AutoFiller filler = new AutoFiller())
        {
            // Bind the PDF template (obsolete InputFileName property is avoided).
            filler.BindPdf(templatePdf);

            // Import the DataTable; column names must match PDF field names (case‑sensitive).
            filler.ImportDataTable(dataTable);

            // Save the merged PDF.
            filler.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF generated successfully: {outputPdfPath}");
    }
}