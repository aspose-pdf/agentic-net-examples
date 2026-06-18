using System;
using System.Data;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    // Loads a CSV file line‑by‑line into a DataTable.
    // The first line is expected to contain column headers.
    static DataTable LoadCsvToDataTable(string csvPath)
    {
        var table = new DataTable();

        using (var reader = new StreamReader(csvPath))
        {
            bool firstLine = true;
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                string[] values = line.Split(',');

                if (firstLine)
                {
                    // Create columns based on header names.
                    foreach (string header in values)
                        table.Columns.Add(header.Trim(), typeof(string));

                    firstLine = false;
                }
                else
                {
                    // Add a new row with the parsed values.
                    var row = table.NewRow();
                    int colCount = Math.Min(values.Length, table.Columns.Count);
                    for (int i = 0; i < colCount; i++)
                        row[i] = values[i].Trim();

                    table.Rows.Add(row);
                }
            }
        }

        return table;
    }

    static void Main()
    {
        const string csvPath        = "data.csv";          // CSV exported from the large XLSX file
        const string templatePdfPath = "template.pdf";      // PDF with form fields matching column names
        const string outputPdfPath   = "merged_output.pdf"; // Resulting merged PDF

        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"CSV file not found: {csvPath}");
            return;
        }

        if (!File.Exists(templatePdfPath))
        {
            Console.Error.WriteLine($"Template PDF not found: {templatePdfPath}");
            return;
        }

        // Load CSV data into a DataTable using streaming to keep memory usage low.
        DataTable dataTable = LoadCsvToDataTable(csvPath);

        // Use AutoFiller to bind the template and import the DataTable.
        using (var autoFiller = new Aspose.Pdf.Facades.AutoFiller())
        {
            // Bind the PDF template file.
            autoFiller.BindPdf(templatePdfPath);

            // Import the data. Column names in the DataTable must match field names in the PDF.
            autoFiller.ImportDataTable(dataTable);

            // Save the merged PDF containing one filled page per data row.
            autoFiller.Save(outputPdfPath);
        }

        Console.WriteLine($"Merged PDF saved to '{outputPdfPath}'.");
    }
}