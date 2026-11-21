using System;
using System.Data;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "template.pdf";
        const string outputPdf = "output.pdf";

        // Build a sample DataTable
        DataTable dt = new DataTable();
        dt.Columns.Add("ID", typeof(int));
        dt.Columns.Add("Name", typeof(string));
        dt.Columns.Add("Score", typeof(double));

        dt.Rows.Add(1, "Alice", 85.5);
        dt.Rows.Add(2, "Bob", 92.0);
        dt.Rows.Add(3, "Charlie", 78.0);
        dt.Rows.Add(4, "Diana", 88.5);

        // Create a DataView that filters rows (e.g., Score >= 80)
        DataView view = new DataView(dt);
        view.RowFilter = "Score >= 80";

        // Ensure the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Add a new page where the table will be placed
            Page page = doc.Pages.Add();

            // Create a table and import the filtered DataView, including column headers
            Table table = new Table
            {
                Border = new BorderInfo(BorderSide.All, 0.5f) // optional visual styling
            };

            // ImportDataView requires the range of cells to fill.
            // We fill from the first row/column (0) to the last row/column of the view.
            int firstFilledRow = 0;
            int firstFilledColumn = 0;
            int lastFilledRow = view.Count - 1; // view.Count is the number of rows after filtering
            int lastFilledColumn = view.Table.Columns.Count - 1;
            table.ImportDataView(view, true, firstFilledRow, firstFilledColumn, lastFilledRow, lastFilledColumn);

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the resulting PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved to '{outputPdf}'.");
    }
}
