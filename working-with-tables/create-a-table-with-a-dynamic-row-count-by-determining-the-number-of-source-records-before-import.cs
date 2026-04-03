using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // for TextState if needed

class Program
{
    static void Main()
    {
        // Paths
        const string inputPdfPath  = "template.pdf";   // existing PDF to add the table to
        const string outputPdfPath = "output_with_table.pdf";

        // Ensure the template exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Template PDF not found: {inputPdfPath}");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Load the source PDF (using rule: document-disposal-with-using)
        // -----------------------------------------------------------------
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // -------------------------------------------------------------
            // 2. Prepare source data (DataTable) – could be from DB, CSV, etc.
            // -------------------------------------------------------------
            DataTable sourceTable = GetSampleDataTable();

            // Determine number of records (rows) before import
            int recordCount = sourceTable.Rows.Count;
            Console.WriteLine($"Number of source records: {recordCount}");

            // -------------------------------------------------------------
            // 3. Create a Table and import the DataTable
            // -------------------------------------------------------------
            Table table = new Table
            {
                // Position the table on the page (example values)
                // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
                // Left, Bottom, Width, Height are optional; setting Left/Top is enough
                // Here we set Left and Top; Width will be auto‑calculated
                Left = 50,
                Top  = 700
            };

            // Import the entire DataTable.
            // Parameters:
            //   sourceTable,
            //   isColumnNamesImported = true (first row will contain column names),
            //   firstFilledRow = 0 (start at first row of the table),
            //   firstFilledColumn = 0 (start at first column)
            table.ImportDataTable(sourceTable, true, 0, 0);

            // -------------------------------------------------------------
            // 4. Add the table to the first page of the PDF
            // -------------------------------------------------------------
            Page firstPage = pdfDoc.Pages[1];
            firstPage.Paragraphs.Add(table);

            // -------------------------------------------------------------
            // 5. Save the modified PDF (using rule: document-disposal-with-using)
            // -------------------------------------------------------------
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with table saved to '{outputPdfPath}'.");
    }

    // Helper method to create a sample DataTable.
    // In real scenarios replace this with actual data retrieval logic.
    private static DataTable GetSampleDataTable()
    {
        DataTable dt = new DataTable("SampleData");

        // Define columns
        dt.Columns.Add("ID",   typeof(int));
        dt.Columns.Add("Name", typeof(string));
        dt.Columns.Add("Score", typeof(double));

        // Add rows (example data)
        dt.Rows.Add(1, "Alice",   85.5);
        dt.Rows.Add(2, "Bob",     92.0);
        dt.Rows.Add(3, "Charlie", 78.3);
        dt.Rows.Add(4, "Diana",   88.8);
        dt.Rows.Add(5, "Ethan",   91.2);

        return dt;
    }
}