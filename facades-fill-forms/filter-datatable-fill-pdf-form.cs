using System;
using System.Data;
using System.Globalization;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string csvPath = "input.csv";          // source CSV file (converted from Excel)
        const string pdfTemplate = "template.pdf";   // PDF with form fields matching column names
        const string outputPdf = "subset_output.pdf";

        // Ensure sample files exist so the program can run without external resources
        EnsureSampleCsv(csvPath);
        EnsureSamplePdf(pdfTemplate);

        // 1. Load the CSV into a DataTable (cross‑platform, no OleDb)
        DataTable fullTable = LoadCsvToDataTable(csvPath);

        // 2. Filter rows – example: keep rows where the "Amount" column > 1000
        DataTable filteredTable = fullTable.Clone(); // copy structure
        foreach (DataRow row in fullTable.Rows)
        {
            if (decimal.TryParse(row["Amount"]?.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out var amount) && amount > 1000m)
                filteredTable.ImportRow(row);
        }

        // 3. Use AutoFiller to import the filtered DataTable into the PDF template
        using (AutoFiller filler = new AutoFiller())
        {
            filler.BindPdf(pdfTemplate);               // load the template PDF
            filler.ImportDataTable(filteredTable);      // map DataTable columns to form fields
            filler.Save(outputPdf);                     // generate the subset PDF
        }

        Console.WriteLine($"Subset PDF saved to '{outputPdf}'.");
    }

    // Helper: creates a minimal CSV file if it does not exist (for demo / testing purposes)
    private static void EnsureSampleCsv(string path)
    {
        if (File.Exists(path))
            return;

        var lines = new[]
        {
            "Id,Name,Amount",
            "1,Alpha,500",
            "2,Beta,1500",
            "3,Gamma,2500",
            "4,Delta,750"
        };
        File.WriteAllLines(path, lines);
    }

    // Helper: creates a minimal PDF with form fields if it does not exist (for demo / testing purposes)
    private static void EnsureSamplePdf(string path)
    {
        if (File.Exists(path))
            return;

        // Create a new PDF document
        Document doc = new Document();
        Page page = doc.Pages.Add();

        // Define positions for the three fields (simple layout)
        // Field: Id
        TextBoxField idField = new TextBoxField(page, new Rectangle(50, 750, 150, 770))
        {
            PartialName = "Id",
            Value = ""
        };
        page.Paragraphs.Add(idField);

        // Field: Name
        TextBoxField nameField = new TextBoxField(page, new Rectangle(200, 750, 350, 770))
        {
            PartialName = "Name",
            Value = ""
        };
        page.Paragraphs.Add(nameField);

        // Field: Amount
        TextBoxField amountField = new TextBoxField(page, new Rectangle(400, 750, 500, 770))
        {
            PartialName = "Amount",
            Value = ""
        };
        page.Paragraphs.Add(amountField);

        // Save the template PDF
        doc.Save(path);
    }

    // Helper: reads a CSV file into a DataTable (first line = column headers)
    static DataTable LoadCsvToDataTable(string csvFilePath)
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
                    foreach (var header in values)
                        dt.Columns.Add(header.Trim());
                    isFirstLine = false;
                }
                else
                {
                    var row = dt.NewRow();
                    for (int i = 0; i < values.Length && i < dt.Columns.Count; i++)
                        row[i] = values[i].Trim();
                    dt.Rows.Add(row);
                }
            }
        }
        return dt;
    }
}
