using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Step 1: Create a sample PDF that contains a simple table.
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();
            Table table = new Table();
            // Define a single column with a width of 200 points.
            table.ColumnWidths = "200";
            Row row = table.Rows.Add();
            Cell cell = row.Cells.Add("");
            // Add the table to the page.
            page.Paragraphs.Add(table);
            // Save the intermediate PDF.
            doc.Save("input.pdf");
        }

        // Step 2: Reopen the PDF and insert a checkbox form field inside the cell.
        using (Document doc = new Document("input.pdf"))
        {
            Page page = doc.Pages[1];
            // Define a rectangle that fits inside the previously created cell.
            // Coordinates are (llx, lly, urx, ury) in points.
            Rectangle checkboxRect = new Rectangle(55, 705, 75, 725);
            CheckboxField checkbox = new CheckboxField(page, checkboxRect);
            checkbox.PartialName = "CheckBox1";
            checkbox.Style = BoxStyle.Check;
            // Add the checkbox to the document's form collection.
            doc.Form.Add(checkbox);
            // Save the final PDF.
            doc.Save("output.pdf");
        }
    }
}