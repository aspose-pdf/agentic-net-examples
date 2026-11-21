using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Sample data – create a DataTable and a DataView
        DataTable dataTable = new DataTable();
        dataTable.Columns.Add("ID", typeof(int));
        dataTable.Columns.Add("Name", typeof(string));
        dataTable.Columns.Add("Quantity", typeof(int));

        dataTable.Rows.Add(1, "Apples", 10);
        dataTable.Rows.Add(2, "Bananas", 20);
        dataTable.Rows.Add(3, "Cherries", 15);

        DataView view = new DataView(dataTable);

        // Load the existing PDF
        using (Document doc = new Document(inputPdfPath))
        {
            // Create a table and set column adjustment to AutoFitToContent
            Aspose.Pdf.Table table = new Aspose.Pdf.Table
            {
                ColumnAdjustment = ColumnAdjustment.AutoFitToContent,
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f) // optional visual aid
            };

            // Add header row
            Aspose.Pdf.Row headerRow = table.Rows.Add();
            foreach (DataColumn col in dataTable.Columns)
            {
                Aspose.Pdf.Cell headerCell = headerRow.Cells.Add(col.ColumnName);
                // Make header text bold
                TextFragment tf = new TextFragment(col.ColumnName)
                {
                    TextState = { Font = FontRepository.FindFont("Helvetica-Bold") }
                };
                headerCell.Paragraphs.Add(tf);
            }

            // Add data rows from the DataView
            foreach (DataRowView drv in view)
            {
                Aspose.Pdf.Row dataRow = table.Rows.Add();
                foreach (DataColumn col in dataTable.Columns)
                {
                    string cellText = drv[col.ColumnName]?.ToString() ?? string.Empty;
                    dataRow.Cells.Add(cellText);
                }
            }

            // Add the table to the first page (or create a new page if none)
            Page page;
            if (doc.Pages.Count > 0)
                page = doc.Pages[1];
            else
                page = doc.Pages.Add();

            page.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Table added and PDF saved to '{outputPdfPath}'.");
    }
}