using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;   // Facades namespace as requested

class Program
{
    static void Main()
    {
        // Paths for input (template) PDF and output PDF
        const string inputPdfPath  = "template.pdf";   // existing PDF to host the table
        const string outputPdfPath = "output_with_table.pdf";

        // Assume a DataTable named 'dataTable' has been populated elsewhere.
        // For demonstration, we create a simple DataTable with sample data.
        DataTable dataTable = new DataTable("SampleData");
        dataTable.Columns.Add("Product", typeof(string));
        dataTable.Columns.Add("Quantity", typeof(int));
        dataTable.Columns.Add("Price", typeof(decimal));

        dataTable.Rows.Add("Apple",  10, 0.99m);
        dataTable.Rows.Add("Banana", 20, 0.59m);
        dataTable.Rows.Add("Cherry", 15, 2.49m);

        // Ensure the input PDF exists; otherwise create a blank document.
        if (!File.Exists(inputPdfPath))
        {
            // Create a blank PDF with a single page to host the table.
            using (Document blankDoc = new Document())
            {
                blankDoc.Pages.Add();
                blankDoc.Save(inputPdfPath);
            }
        }

        // Load the PDF, add the table, and save the result.
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Add a new page if needed (optional).
            Page page = pdfDoc.Pages[1]; // 1‑based indexing

            // Create a Table instance.
            Table table = new Table();

            // Optional: define column widths (space‑separated values).
            // Adjust the widths according to the number of columns in the DataTable.
            table.ColumnWidths = "150 100 100";

            // Import the DataTable into the Table.
            // Parameters: (DataTable, includeColumnNames, firstRow, firstColumn)
            table.ImportDataTable(dataTable, true, 0, 0);

            // Add the populated table to the page's paragraph collection.
            page.Paragraphs.Add(table);

            // Save the modified PDF.
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with table saved to '{outputPdfPath}'.");
    }
}