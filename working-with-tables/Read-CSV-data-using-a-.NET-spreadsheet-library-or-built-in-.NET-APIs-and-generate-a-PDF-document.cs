using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class CsvToPdfGenerator
{
    static void Main()
    {
        // Paths – adjust as needed
        const string csvPath = "input.csv";
        const string pdfPath = "output.pdf";

        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"CSV file not found: {csvPath}");
            return;
        }

        // Load CSV into a DataTable
        DataTable csvTable = new DataTable();
        using (var reader = new StreamReader(csvPath))
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
                    // First line contains column headers
                    foreach (string header in fields)
                        csvTable.Columns.Add(header.Trim());
                    isFirstLine = false;
                }
                else
                {
                    // Subsequent lines contain data rows
                    csvTable.Rows.Add(fields);
                }
            }
        }

        // Create PDF document and add a table with the CSV data
        using (Document pdfDoc = new Document())
        {
            // Add a new page
            Page page = pdfDoc.Pages.Add();

            // Create a table and import the DataTable content
            Table table = new Table();

            // Import data: include column names, start at first row/column (0‑based)
            table.ImportDataTable(csvTable, true, 0, 0);

            // Optional: set column widths (adjust as needed)
            // table.ColumnWidths = "100 150 200";

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the PDF
            pdfDoc.Save(pdfPath);
        }

        Console.WriteLine($"PDF generated successfully at '{pdfPath}'.");
    }
}