using System;
using System.Data;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    private const string CsvPath = "data.csv";
    private const string TemplatePdfPath = "template.pdf";
    private const string OutputPdfPath = "merged_output.pdf";
    private const int BatchSize = 1000;

    static void Main()
    {
        // Ensure a minimal PDF template exists – the sandbox starts empty.
        CreateTemplatePdfIfMissing();

        // Create a tiny CSV so the example can run without external files.
        CreateSampleCsvIfMissing();

        using (FileStream outputStream = new FileStream(OutputPdfPath, FileMode.Create, FileAccess.Write))
        using (AutoFiller autoFiller = new AutoFiller())
        {
            autoFiller.BindPdf(TemplatePdfPath);

            int rowsInCurrentBatch = 0;
            DataTable batchTable = new DataTable("Batch");

            using (var reader = new StreamReader(CsvPath))
            {
                // Read header line and build the DataTable schema.
                string? headerLine = reader.ReadLine();
                if (string.IsNullOrEmpty(headerLine))
                    throw new InvalidOperationException("CSV file is empty or header line is missing.");

                string[] headers = headerLine.Split(',');
                foreach (var header in headers)
                    batchTable.Columns.Add(header.Trim(), typeof(string));

                // Process rows one by one, flushing each batch to AutoFiller.
                while (!reader.EndOfStream)
                {
                    string? line = reader.ReadLine();
                    if (string.IsNullOrWhiteSpace(line))
                        continue; // skip empty lines

                    string[] values = line.Split(',');
                    DataRow row = batchTable.NewRow();
                    for (int i = 0; i < headers.Length && i < values.Length; i++)
                        row[i] = values[i].Trim();
                    batchTable.Rows.Add(row);
                    rowsInCurrentBatch++;

                    if (rowsInCurrentBatch >= BatchSize)
                    {
                        autoFiller.ImportDataTable(batchTable);
                        batchTable.Clear(); // keep column definitions, remove rows
                        rowsInCurrentBatch = 0;
                    }
                }
            }

            // Import any leftover rows.
            if (batchTable.Rows.Count > 0)
                autoFiller.ImportDataTable(batchTable);

            // Save the merged PDF directly to the output stream.
            autoFiller.Save(outputStream);
        }

        Console.WriteLine($"Merged PDF saved to '{OutputPdfPath}'.");
    }

    // ---------------------------------------------------------------------
    // Helper: create a very simple PDF template if it does not already exist.
    // ---------------------------------------------------------------------
    private static void CreateTemplatePdfIfMissing()
    {
        if (File.Exists(TemplatePdfPath))
            return;

        using (var doc = new Document())
        {
            var page = doc.Pages.Add();
            // Add a single text box field; in real scenarios the template would already
            // contain the fields that match the CSV column names.
            var field = new TextBoxField(page, new Rectangle(100, 700, 300, 730))
            {
                PartialName = "Field1",
                Value = string.Empty
            };
            page.Paragraphs.Add(field);
            doc.Save(TemplatePdfPath);
        }
    }

    // ---------------------------------------------------------------------
    // Helper: create a tiny CSV file so the demo runs without external data.
    // ---------------------------------------------------------------------
    private static void CreateSampleCsvIfMissing()
    {
        if (File.Exists(CsvPath))
            return;

        var lines = new[]
        {
            "Field1,Field2",
            "Alice,100",
            "Bob,200",
            "Charlie,300"
        };
        File.WriteAllLines(CsvPath, lines);
    }
}
