using System;
using System.Data;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPdf = "ExcelTable.pdf";

        // Sample DataTable representing Excel worksheet data
        DataTable dt = new DataTable("Sample");
        dt.Columns.Add("Product", typeof(string));
        dt.Columns.Add("Quantity", typeof(int));
        dt.Columns.Add("Price", typeof(decimal));

        dt.Rows.Add("Widget A", 10, 9.99m);
        dt.Rows.Add("Widget B", 5, 19.95m);
        dt.Rows.Add("Gadget C", 2, 99.50m);

        // Create a new PDF document
        using (Document pdfDoc = new Document())
        {
            // Add a page to the document
            Page page = pdfDoc.Pages.Add();

            // Create and configure a table
            Table table = new Table
            {
                ColumnAdjustment = ColumnAdjustment.AutoFitToWindow,
                Border = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Black),
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Gray),
                DefaultCellPadding = new MarginInfo { Top = 5, Bottom = 5, Left = 5, Right = 5 },
                ColumnWidths = "150 80 80"
            };

            // Import the DataTable into the table (first row will be column names)
            table.ImportDataTable(dt, true, 0, 0);

            // Style the header row
            Row headerRow = table.Rows[0];
            foreach (Cell cell in headerRow.Cells)
            {
                cell.BackgroundColor = Aspose.Pdf.Color.LightGray;
                cell.DefaultCellTextState = new TextState
                {
                    Font = FontRepository.FindFont("Helvetica"),
                    FontSize = 12,
                    FontStyle = FontStyles.Bold,
                    ForegroundColor = Aspose.Pdf.Color.Black,
                    HorizontalAlignment = HorizontalAlignment.Center
                };
            }

            // Style the data rows
            for (int i = 1; i < table.Rows.Count; i++)
            {
                Row dataRow = table.Rows[i];
                foreach (Cell cell in dataRow.Cells)
                {
                    cell.DefaultCellTextState = new TextState
                    {
                        Font = FontRepository.FindFont("Helvetica"),
                        FontSize = 11,
                        ForegroundColor = Aspose.Pdf.Color.Black,
                        HorizontalAlignment = HorizontalAlignment.Left
                    };
                }
            }

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the PDF document
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with imported Excel table saved to '{outputPdf}'.");
    }
}