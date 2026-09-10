using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table and add it to the page
            Table table = new Table
            {
                // Define three equal-width columns
                ColumnWidths = "100 100 100"
            };
            page.Paragraphs.Add(table);

            // ---------- Header row ----------
            // Add the header row
            Row headerRow = table.Rows.Add();

            // Set background color for the entire header row
            headerRow.BackgroundColor = Color.LightGray;

            // Define default text style for cells in the header row
            headerRow.DefaultCellTextState = new TextState
            {
                // Use a bold font to emphasize the header
                Font = FontRepository.FindFont("Helvetica"),
                FontSize = 12,
                FontStyle = FontStyles.Bold,
                // Text color (optional)
                ForegroundColor = Color.Black
            };

            // Add header cells (the text will inherit the style defined above)
            headerRow.Cells.Add("Product");
            headerRow.Cells.Add("Quantity");
            headerRow.Cells.Add("Price");

            // ---------- Data rows (example) ----------
            // Add a sample data row
            Row dataRow = table.Rows.Add();
            dataRow.Cells.Add("Widget A");
            dataRow.Cells.Add("10");
            dataRow.Cells.Add("$5.00");

            // Add another sample data row
            Row dataRow2 = table.Rows.Add();
            dataRow2.Cells.Add("Widget B");
            dataRow2.Cells.Add("7");
            dataRow2.Cells.Add("$7.50");

            // Save the PDF to a file
            doc.Save("styled_table.pdf");
        }

        Console.WriteLine("PDF with styled header row saved as 'styled_table.pdf'.");
    }
}