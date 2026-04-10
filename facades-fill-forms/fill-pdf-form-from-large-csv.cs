using System;
using System.Data;
using System.IO;

class Program
{
    // Loads a CSV file line‑by‑line into a DataTable.
    // First line is treated as header (column names).
    private static DataTable LoadCsvToDataTable(string csvPath)
    {
        var table = new DataTable();

        using (var reader = new StreamReader(csvPath))
        {
            // Read header line
            string headerLine = reader.ReadLine();
            if (headerLine == null)
                throw new InvalidOperationException("CSV file is empty.");

            string[] headers = headerLine.Split(',');

            // Create columns (all as string for simplicity)
            foreach (string header in headers)
                table.Columns.Add(header.Trim(), typeof(string));

            // Read data rows
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                    continue; // skip empty lines

                string[] values = line.Split(',');
                var row = table.NewRow();

                // Fill row values (truncate if fewer columns)
                int colCount = Math.Min(values.Length, headers.Length);
                for (int i = 0; i < colCount; i++)
                    row[i] = values[i].Trim();

                table.Rows.Add(row);
            }
        }

        return table;
    }

    static void Main()
    {
        const string csvPath        = "data.csv";          // CSV derived from the large XLSX
        const string templatePdf    = "template.pdf";      // PDF with form fields matching column names
        const string outputPdf      = "filled_output.pdf";

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

        try
        {
            // Stream CSV into a DataTable (memory‑efficient row‑by‑row reading)
            DataTable dataTable = LoadCsvToDataTable(csvPath);

            // Use AutoFiller (facade) to merge data into the PDF template
            using (Aspose.Pdf.Facades.AutoFiller filler = new Aspose.Pdf.Facades.AutoFiller())
            {
                // Bind the PDF template
                filler.BindPdf(templatePdf);

                // Import the DataTable; column names must match PDF field names (case‑sensitive)
                filler.ImportDataTable(dataTable);

                // Save the merged PDF to a file
                filler.Save(outputPdf);
            }

            Console.WriteLine($"PDF generated successfully: {outputPdf}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}