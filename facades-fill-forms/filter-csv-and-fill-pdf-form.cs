using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string csvPath = "input.csv";       // generated CSV file
        const string pdfTemplate = "template.pdf"; // generated PDF template with form fields
        const string outputPdf = "subset.pdf";    // resulting PDF

        // -------------------------------------------------
        // 0. Prepare sample input files (CSV + PDF template)
        // -------------------------------------------------
        CreateSampleCsv(csvPath);
        CreatePdfTemplate(pdfTemplate);

        // -------------------------------------------------
        // 1. Load the CSV file into a DataTable
        // -------------------------------------------------
        DataTable sourceTable = LoadCsvToDataTable(csvPath);

        // -------------------------------------------------
        // 2. Filter rows (example: keep rows where Amount > 1000)
        // -------------------------------------------------
        DataTable filteredTable = sourceTable.Clone(); // same schema, no rows
        foreach (DataRow row in sourceTable.Rows)
        {
            if (sourceTable.Columns.Contains("Amount") &&
                Decimal.TryParse(row["Amount"].ToString(), out decimal amount) &&
                amount > 1000)
            {
                filteredTable.ImportRow(row);
            }
        }

        // -------------------------------------------------
        // 3. Import the filtered DataTable into a PDF using Facades
        // -------------------------------------------------
        using (AutoFiller autoFiller = new AutoFiller())
        {
            // Bind the PDF template (must contain form fields whose names match column names)
            autoFiller.BindPdf(pdfTemplate);

            // Import the filtered data; each column name must match a field name in the template
            autoFiller.ImportDataTable(filteredTable);

            // Save the generated PDF (single merged document)
            autoFiller.Save(outputPdf);
        }

        Console.WriteLine($"Subset PDF created at: {outputPdf}");
    }

    /// <summary>
    /// Generates a tiny CSV file used for the demo.
    /// </summary>
    private static void CreateSampleCsv(string path)
    {
        string[] lines = new[]
        {
            "Name,Amount,Date",
            "Alice,1500,2023-01-01",
            "Bob,900,2023-01-02",
            "Charlie,2000,2023-01-03"
        };
        File.WriteAllLines(path, lines);
    }

    /// <summary>
    /// Generates a simple PDF containing form fields whose names match the CSV column headers.
    /// </summary>
    private static void CreatePdfTemplate(string path)
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Define a rectangle helper to position fields (left, bottom, right, top)
            // We'll place three fields vertically spaced.
            float left = 100f, width = 200f, height = 20f;
            float top = 700f;

            // Name field
            TextBoxField nameField = new TextBoxField(page, new Aspose.Pdf.Rectangle(left, top, left + width, top + height))
            {
                PartialName = "Name",
                Value = ""
            };
            doc.Form.Add(nameField);

            // Amount field
            top -= 40f; // move down
            TextBoxField amountField = new TextBoxField(page, new Aspose.Pdf.Rectangle(left, top, left + width, top + height))
            {
                PartialName = "Amount",
                Value = ""
            };
            doc.Form.Add(amountField);

            // Date field
            top -= 40f;
            TextBoxField dateField = new TextBoxField(page, new Aspose.Pdf.Rectangle(left, top, left + width, top + height))
            {
                PartialName = "Date",
                Value = ""
            };
            doc.Form.Add(dateField);

            // Save the template
            doc.Save(path);
        }
    }

    /// <summary>
    /// Reads a CSV file and returns a DataTable. The first line is treated as the header row.
    /// </summary>
    private static DataTable LoadCsvToDataTable(string csvFilePath)
    {
        var dt = new DataTable();
        using (var reader = new StreamReader(csvFilePath))
        {
            bool isFirstLine = true;
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var values = line.Split(',');
                if (isFirstLine)
                {
                    // Create columns based on header names
                    foreach (var header in values)
                    {
                        var columnName = header.Trim();
                        dt.Columns.Add(columnName, typeof(string));
                    }
                    isFirstLine = false;
                }
                else
                {
                    var row = dt.NewRow();
                    for (int i = 0; i < values.Length && i < dt.Columns.Count; i++)
                    {
                        row[i] = values[i].Trim();
                    }
                    dt.Rows.Add(row);
                }
            }
        }
        return dt;
    }
}
