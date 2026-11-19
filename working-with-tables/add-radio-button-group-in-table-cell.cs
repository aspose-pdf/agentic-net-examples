using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "ButtonGroupInCell.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table with two columns
            Table table = new Table
            {
                ColumnWidths = "250 250", // width of each column
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f) // optional visual aid
            };

            // Add a single row
            Row row = table.Rows.Add();

            // First cell (just a label)
            Cell labelCell = row.Cells.Add();
            labelCell.Paragraphs.Add(new TextFragment("Choose an option:"));

            // Second cell – this will contain the radio button group
            Cell buttonCell = row.Cells.Add();

            // Add the table to the page
            page.Paragraphs.Add(table);

            // -----------------------------------------------------------------
            // Build a radio‑button group inside the second cell.
            // Aspose.Pdf.Forms.RadioButtonField has only two constructors:
            //   new RadioButtonField(Page) or new RadioButtonField(Document).
            // Options are added via AddOption(...). The group is added to the
            // document's AcroForm with doc.Form.Add(...).
            // -----------------------------------------------------------------
            RadioButtonField radioGroup = new RadioButtonField(page)
            {
                PartialName = "OptionGroup" // the name that groups the buttons
            };

            // Add each option with its own rectangle (position inside the cell).
            // The rectangle coordinates are expressed in points (1/72 inch).
            radioGroup.AddOption("Option1", new Rectangle(300, 600, 320, 620));
            radioGroup.AddOption("Option2", new Rectangle(300, 560, 320, 580));

            // Select the first option by default (Selected is 1‑based).
            radioGroup.Selected = 1;

            // Register the field with the document.
            doc.Form.Add(radioGroup);

            // Optional: add textual labels next to the radio buttons.
            TextFragment label1 = new TextFragment("Option 1")
            {
                Position = new Position(330, 605)
            };
            page.Paragraphs.Add(label1);

            TextFragment label2 = new TextFragment("Option 2")
            {
                Position = new Position(330, 565)
            };
            page.Paragraphs.Add(label2);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with radio button group saved to '{outputPath}'.");
    }
}
