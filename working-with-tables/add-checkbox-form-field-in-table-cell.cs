using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "TableWithCheckbox.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table with two columns
            Table table = new Table
            {
                // Define column widths (in points)
                ColumnWidths = "200 50"
            };

            // Add a row to the table
            Row row = table.Rows.Add();

            // First cell – contains a label
            Cell labelCell = row.Cells.Add();
            labelCell.Paragraphs.Add(new TextFragment("Agree:"));

            // Second cell – will hold the checkbox
            Cell checkCell = row.Cells.Add();
            // Add an empty paragraph to keep the cell height
            checkCell.Paragraphs.Add(new TextFragment(" "));

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Define the rectangle for the checkbox.
            // Coordinates are in points: lower‑left X, lower‑left Y, upper‑right X, upper‑right Y.
            // Adjust these values to position the checkbox inside the cell as needed.
            Aspose.Pdf.Rectangle chkRect = new Aspose.Pdf.Rectangle(
                llx: 260,   // left
                lly: 750,   // bottom
                urx: 280,   // right
                ury: 770    // top
            );

            // Create the checkbox field on the page at the specified rectangle
            CheckboxField checkbox = new CheckboxField(page, chkRect)
            {
                Name = "AgreeCheck",   // field name
                Checked = false,       // initial state
                Color = Aspose.Pdf.Color.Black, // border color (optional)
                // You can set additional properties such as Style, ExportValue, etc.
            };

            // Add the checkbox to the document's form collection
            doc.Form.Add(checkbox);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with table and checkbox saved to '{outputPath}'.");
    }
}