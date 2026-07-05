using System;
using System.Data;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // ------------------------------------------------------------
        // Prepare a sample DataTable (replace with your own source data)
        // ------------------------------------------------------------
        DataTable dt = new DataTable("Sample");
        dt.Columns.Add("ID", typeof(int));
        dt.Columns.Add("Name", typeof(string));
        dt.Columns.Add("Value", typeof(double));

        dt.Rows.Add(1, "Alpha", 10.5);
        dt.Rows.Add(2, "Beta", 20.0);
        dt.Rows.Add(3, "Gamma", 30.75);

        const string outputPath = "output.pdf";

        // ------------------------------------------------------------
        // Create a PDF where each DataRow occupies its own page
        // ------------------------------------------------------------
        using (Document doc = new Document())
        {
            foreach (DataRow dr in dt.Rows)
            {
                // Add a new page for the current row
                Page page = doc.Pages.Add();

                // Create a table with three columns (matching the DataTable)
                Aspose.Pdf.Table table = new Aspose.Pdf.Table
                {
                    // Optional: set column widths (in points)
                    ColumnWidths = "100 200 100"
                };

                // Header row (optional, makes the PDF clearer)
                Row header = table.Rows.Add();
                header.Cells.Add("ID");
                header.Cells.Add("Name");
                header.Cells.Add("Value");
                // Make header text bold
                header.DefaultCellTextState = new TextState
                {
                    FontSize = 12,
                    FontStyle = FontStyles.Bold
                };

                // Data row – one cell per column
                Row dataRow = table.Rows.Add();
                dataRow.Cells.Add(dr["ID"].ToString());
                dataRow.Cells.Add(dr["Name"].ToString());
                dataRow.Cells.Add(dr["Value"].ToString());

                // Add the table to the page's paragraph collection
                page.Paragraphs.Add(table);
            }

            // Save the generated PDF
            doc.Save(outputPath);
        }

        // ------------------------------------------------------------
        // Verification: ensure the PDF has exactly one page per DataRow
        // ------------------------------------------------------------
        using (Document result = new Document(outputPath))
        {
            int expectedPages = dt.Rows.Count;
            int actualPages   = result.Pages.Count;

            Console.WriteLine($"Expected pages: {expectedPages}, Actual pages: {actualPages}");
            if (expectedPages == actualPages)
                Console.WriteLine("Verification succeeded: one page per DataTable row.");
            else
                Console.WriteLine("Verification failed.");
        }
    }
}