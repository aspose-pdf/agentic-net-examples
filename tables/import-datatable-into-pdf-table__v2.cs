using System;
using System.Data;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for input and output PDFs
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Create a sample DataTable with headers and some rows
        DataTable tableData = new DataTable();
        tableData.Columns.Add("Product", typeof(string));
        tableData.Columns.Add("Quantity", typeof(int));
        tableData.Columns.Add("Price", typeof(decimal));

        tableData.Rows.Add("Apple",  10, 0.5m);
        tableData.Rows.Add("Banana", 20, 0.3m);
        tableData.Rows.Add("Cherry", 15, 1.2m);

        // Load the existing PDF, add the table, and save
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Use the first page; adjust if needed
            Page page = pdfDoc.Pages[1];

            // Create a Table object
            Table pdfTable = new Table();

            // Optional: set column widths (comma‑separated or space‑separated)
            // pdfTable.ColumnWidths = "100 100 100";

            // Import the DataTable into the PDF table.
            // Parameters: DataTable, hasColumnNames (true), startRow (2), startColumn (1)
            pdfTable.ImportDataTable(tableData, true, 2, 1);

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(pdfTable);

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved to '{outputPdfPath}'.");
    }
}