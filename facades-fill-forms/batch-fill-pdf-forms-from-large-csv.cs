using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    // Paths are generated at runtime so the example is self‑contained
    private static readonly string CsvPath = Path.Combine(Path.GetTempPath(), "LargeData.csv");
    private static readonly string TemplatePdfPath = Path.Combine(Path.GetTempPath(), "mail_template.pdf");
    private static readonly string OutputFolder = Path.Combine(Path.GetTempPath(), "GeneratedPdfs");

    // Number of rows to process per batch – tweak based on memory constraints
    private const int BatchSize = 2; // small value for demo purposes

    static void Main()
    {
        // ------------------------------------------------------------
        // 1️⃣ Create sample CSV data (simulating a large XLSX converted to CSV)
        // ------------------------------------------------------------
        CreateSampleCsv();

        // ------------------------------------------------------------
        // 2️⃣ Create a PDF template with form fields that match the CSV columns
        // ------------------------------------------------------------
        CreatePdfTemplate();

        // Ensure the output directory exists
        Directory.CreateDirectory(OutputFolder);

        // Open the CSV file for streaming
        using var reader = new StreamReader(CsvPath);
        string headerLine = reader.ReadLine();
        if (string.IsNullOrWhiteSpace(headerLine))
        {
            Console.Error.WriteLine("CSV file is empty or missing a header line.");
            return;
        }

        // Build a DataTable schema that matches the CSV columns (all string type for simplicity)
        var headers = headerLine.Split(','); // headerLine is guaranteed non‑null after the check above
        DataTable schemaTable = new DataTable();
        foreach (var h in headers)
        {
            schemaTable.Columns.Add(h.Trim(), typeof(string));
        }

        DataTable batchTable = schemaTable.Clone(); // empty table with same columns
        int batchIndex = 0;
        int rowCount = 0;

        // Stream rows line‑by‑line, fill batches and process them
        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();
            if (string.IsNullOrWhiteSpace(line))
                continue; // skip empty lines

            var values = line.Split(',');
            var row = batchTable.NewRow();
            for (int i = 0; i < headers.Length && i < values.Length; i++)
            {
                row[i] = values[i].Trim();
            }
            batchTable.Rows.Add(row);
            rowCount++;

            if (batchTable.Rows.Count >= BatchSize)
            {
                ProcessBatch(batchTable, batchIndex);
                batchTable.Clear();
                batchIndex++;
            }
        }

        // Process any remaining rows that didn't fill a complete batch
        if (batchTable.Rows.Count > 0)
        {
            ProcessBatch(batchTable, batchIndex);
        }

        Console.WriteLine($"Finished processing {rowCount} rows in {batchIndex + 1} batch(es).");
    }

    // Uses Aspose.Pdf.Facades.AutoFiller to generate PDFs for a batch of rows
    private static void ProcessBatch(DataTable batchTable, int batchIndex)
    {
        // Create AutoFiller instance (wrapped in using for deterministic disposal)
        using var autoFiller = new AutoFiller();

        // Bind the PDF template (can also use a Stream or Document)
        autoFiller.BindPdf(TemplatePdfPath);

        // Configure output – each batch creates its own set of files
        // In multi‑document (many‑small‑files) mode we set GeneratingPath and BasicFileName
        // but we must NOT pass a path to Save() – calling Save() without arguments triggers the correct mode.
        autoFiller.GeneratingPath = OutputFolder;
        autoFiller.BasicFileName = $"Batch_{batchIndex}_Output";

        // Import the current batch of data
        autoFiller.ImportDataTable(batchTable);

        // Save all generated PDFs (many‑small‑files mode). No argument is required.
        autoFiller.Save();
    }

    // ---------------------------------------------------------------------
    // Helper: create a tiny CSV file with a few rows (simulating a large file)
    // ---------------------------------------------------------------------
    private static void CreateSampleCsv()
    {
        var csvLines = new[]
        {
            "Name,Email",
            "John Doe,john.doe@example.com",
            "Jane Smith,jane.smith@example.com",
            "Bob Johnson,bob.johnson@example.com",
            "Alice Brown,alice.brown@example.com"
        };
        File.WriteAllLines(CsvPath, csvLines);
    }

    // ---------------------------------------------------------------------
    // Helper: create a PDF template with form fields that match the CSV headers
    // ---------------------------------------------------------------------
    private static void CreatePdfTemplate()
    {
        // Simple template with two text fields: Name and Email
        using var doc = new Document();
        var page = doc.Pages.Add();

        // Define positions for the fields (just for demo purposes)
        var nameField = new TextBoxField(page, new Rectangle(100, 700, 400, 720))
        {
            PartialName = "Name",
            Value = string.Empty
        };
        var emailField = new TextBoxField(page, new Rectangle(100, 660, 400, 680))
        {
            PartialName = "Email",
            Value = string.Empty
        };

        doc.Form.Add(nameField);
        doc.Form.Add(emailField);

        doc.Save(TemplatePdfPath);
    }
}
