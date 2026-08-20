using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing; // retained for Color, Border, etc.

class Program
{
    static void Main()
    {
        // Create a new PDF document inside a using block for proper disposal.
        using (Document doc = new Document())
        {
            // Add a page to the document.
            Page page = doc.Pages.Add();

            // Create a table and add it to the page.
            Table table = new Table();
            // Define column width so the cell can contain the radio buttons.
            table.ColumnWidths = "200"; // width in points
            page.Paragraphs.Add(table);

            // Add a row and set its height.
            Row row = table.Rows.Add();
            row.FixedRowHeight = 100; // height of the cell in points

            // Add a cell to the row.
            Cell cell = row.Cells.Add();
            // (Optional) you can add placeholder text to visualize the cell.
            // cell.Paragraphs.Add(new TextFragment(""));

            // Create a radio button field.
            RadioButtonField radioGroup = new RadioButtonField(page);
            // Set the group name (same name for all options).
            radioGroup.PartialName = "SampleRadioGroup";
            // Allow the group to have no selection (optional).
            radioGroup.NoToggleToOff = false;

            // Add the first option. Position it inside the cell using absolute page coordinates.
            radioGroup.AddOption(
                "Option1",
                new Aspose.Pdf.Rectangle(70, 660, 90, 680)); // x1, y1, x2, y2

            // Add the second option.
            radioGroup.AddOption(
                "Option2",
                new Aspose.Pdf.Rectangle(70, 630, 90, 650));

            // Add the radio button field to the document's form collection (not directly to page annotations).
            doc.Form.Add(radioGroup, page.Number);

            // Save the PDF.
            doc.Save("RadioButtonInCell.pdf");
        }

        Console.WriteLine("PDF with radio button group created successfully.");
    }
}
