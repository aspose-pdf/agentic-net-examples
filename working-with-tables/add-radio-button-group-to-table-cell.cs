using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;               // <-- needed for TextFragment
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "RadioButtonTable.pdf";

        // Create a new PDF document inside a using block (ensures disposal)
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table with one column
            Table table = new Table
            {
                ColumnWidths = "200", // width of the single column
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Color.Black)
            };

            // Add a row to the table
            Row row = table.Rows.Add();

            // Add a cell to the row
            Cell cell = row.Cells.Add();

            // Insert some placeholder text into the cell
            cell.Paragraphs.Add(new TextFragment("Choose an option:"));

            // Add the table to the page
            page.Paragraphs.Add(table);

            // ------------------------------------------------------------
            // Create a RadioButtonField group inside the cell.
            // All options share the same PartialName (group name).
            // ------------------------------------------------------------

            // Define the group name (same for all options)
            const string groupName = "SampleRadioGroup";

            // Determine rectangles for the radio button options (coordinates are page‑relative)
            Aspose.Pdf.Rectangle option1Rect = new Aspose.Pdf.Rectangle(100, 700, 115, 715);
            Aspose.Pdf.Rectangle option2Rect = new Aspose.Pdf.Rectangle(100, 680, 115, 695);
            Aspose.Pdf.Rectangle option3Rect = new Aspose.Pdf.Rectangle(100, 660, 115, 675);

            // Instantiate the RadioButtonField using the page‑constructor (no rectangle needed here)
            RadioButtonField radioField = new RadioButtonField(page)
            {
                PartialName = groupName,               // group name
                AlternateName = "Select one option"
            };

            // Add the individual options – each option gets its own rectangle
            radioField.AddOption("Option A", option1Rect);
            radioField.AddOption("Option B", option2Rect);
            radioField.AddOption("Option C", option3Rect);

            // Optionally set the default selected option (index starts at 1)
            radioField.Selected = 1; // selects "Option A" by default

            // Add the radio button field to the document's form collection
            doc.Form.Add(radioField);

            // Save the document (PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with radio button group saved to '{outputPath}'.");
    }
}
