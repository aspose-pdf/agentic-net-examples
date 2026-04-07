using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths for the source PDF (template) and the resulting PDF
        const string inputPdf = "template.pdf";
        const string outputPdf = "output.pdf";

        // Prepare a sample DataTable to import into the PDF table
        DataTable dt = new DataTable();
        dt.Columns.Add("Name");
        dt.Columns.Add("Age");
        dt.Rows.Add("Alice", 30);
        dt.Rows.Add("Bob", 25);
        dt.Rows.Add("Charlie", 35);

        // Verify that the input PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF, create a table, import data, auto‑fit columns, and save
        using (Document doc = new Document(inputPdf))
        {
            // Create a new table and set its position on the page
            Table table = new Table
            {
                Left = 50,   // X coordinate
                Top = 700,   // Y coordinate
                // Optional visual styling
                Border = new BorderInfo(BorderSide.All, 1, Aspose.Pdf.Color.Black)
            };

            // Import the DataTable into the Aspose.Pdf.Table.
            // Parameters: (DataTable, include column names, first row index, first column index)
            table.ImportDataTable(dt, true, 0, 0);

            // Adjust column widths automatically to fit the content
            table.ColumnAdjustment = ColumnAdjustment.AutoFitToContent;

            // Add the table to the first page of the document
            doc.Pages[1].Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with auto‑fitted table: '{outputPdf}'.");
    }
}
