using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class CsvToPdfGenerator
{
    static void Main()
    {
        // Input CSV file path
        const string csvPath = "input.csv";
        // Output PDF file path
        const string pdfPath = "output.pdf";

        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"CSV file not found: {csvPath}");
            return;
        }

        // Load CSV data into a DataTable
        DataTable csvTable = LoadCsvIntoDataTable(csvPath);

        // Create a new PDF document
        using (Document pdfDoc = new Document())
        {
            // Add a page to the document
            Page page = pdfDoc.Pages.Add();

            // Create a table that will hold the CSV data
            Table pdfTable = new Table
            {
                // Optional: set table border and column widths
                Border = new BorderInfo(BorderSide.All, 0.5f, Color.Black)
            };

            // Import the DataTable into the PDF table.
            // Parameters:
            //   csvTable               – source data
            //   true                   – import column names as the first row
            //   0                      – start importing at the first row of the PDF table
            //   0                      – start importing at the first column of the PDF table
            pdfTable.ImportDataTable(csvTable, true, 0, 0);

            // Add the table to the page's paragraphs collection
            page.Paragraphs.Add(pdfTable);

            // Save the PDF document
            pdfDoc.Save(pdfPath);
        }

        Console.WriteLine($"PDF generated successfully at '{pdfPath}'.");
    }

    // Reads a CSV file and returns a populated DataTable.
    // Simple CSV parsing assumes commas as separators and no quoted fields.
    private static DataTable LoadCsvIntoDataTable(string filePath)
    {
        DataTable table = new DataTable();

        using (var reader = new StreamReader(filePath))
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
                    // Create columns based on the first line
                    foreach (string header in fields)
                    {
                        table.Columns.Add(header.Trim());
                    }
                    isFirstLine = false;
                }
                else
                {
                    // Add data rows
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