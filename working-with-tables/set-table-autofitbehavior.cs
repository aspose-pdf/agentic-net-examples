using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Create a sample PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table
            Table table = new Table();
            // Make the table stretch to the page width
            table.ColumnAdjustment = Aspose.Pdf.ColumnAdjustment.AutoFitToWindow;
            // Define three columns (values will be stretched to fill the page)
            table.ColumnWidths = "100 100 100";

            // Add a header row
            Row headerRow = table.Rows.Add();
            headerRow.Cells.Add("Header 1");
            headerRow.Cells.Add("Header 2");
            headerRow.Cells.Add("Header 3");

            // Add three data rows (evaluation mode limits collections to 4 elements)
            for (int i = 1; i <= 3; i++)
            {
                Row dataRow = table.Rows.Add();
                dataRow.Cells.Add("Row " + i + " Col 1");
                dataRow.Cells.Add("Row " + i + " Col 2");
                dataRow.Cells.Add("Row " + i + " Col 3");
            }

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the PDF document
            doc.Save("output.pdf");
        }
    }
}