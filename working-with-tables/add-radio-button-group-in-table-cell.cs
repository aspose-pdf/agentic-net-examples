using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text; // Added for TextFragment

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_radio.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // ---------------------------------------------------------------------
            // 1. Create a simple table with a single cell. The table is added to the
            //    first page as a regular paragraph element – no logical‑structure API
            //    (ITaggedContent) is used because it is not available in the current
            //    Aspose.Pdf version.
            // ---------------------------------------------------------------------
            Page page = doc.Pages[1];

            Table table = new Table();
            table.ColumnWidths = "200";               // one column, 200 points wide
            table.DefaultCellBorder = new BorderInfo(BorderSide.All, 1f, Color.Black);
            table.Border = new BorderInfo(BorderSide.All, 1f, Color.Black);

            // Add a row with a single cell that will host the radio buttons
            Row row = table.Rows.Add();
            Cell cell = row.Cells.Add();
            cell.Paragraphs.Add(new TextFragment(" ")); // placeholder to keep the cell height

            // Position the table on the page (optional – you can adjust as needed)
            table.Margin = new MarginInfo { Top = 550, Left = 100 };
            page.Paragraphs.Add(table);

            // ---------------------------------------------------------------------
            // 2. Define the rectangle that corresponds to the visual area of the cell.
            //    The coordinates are chosen so that the radio buttons appear inside
            //    the cell created above.
            // ---------------------------------------------------------------------
            double llx = 100; // left
            double lly = 600; // lower
            double urx = 300; // right
            double ury = 650; // upper
            Rectangle cellRect = new Rectangle(llx, lly, urx, ury);

            // ---------------------------------------------------------------------
            // 3. Create two radio‑button *option* fields that share the same PartialName
            //    (group name).  The constructor that works across Aspose.Pdf versions is
            //    the one that takes a Page and a Rectangle.
            // ---------------------------------------------------------------------
            const string groupName = "ChoiceGroup";

            // First option
            RadioButtonOptionField radio1 = new RadioButtonOptionField(page, cellRect)
            {
                PartialName = groupName,
                Value = "Option1"
            };
            // Set visual appearance (border and colour)
            radio1.Border = new Border(radio1) { Width = 1 };
            radio1.Color = Color.Black;
            // Set properties that may not exist in all versions via dynamic dispatch
            SetOptionalRadioProperties(radio1, isChecked: true, backgroundColor: Color.White);
            doc.Form.Add(radio1);

            // Second option – shifted down by 30 points so it appears below the first
            Rectangle cellRect2 = new Rectangle(llx, lly - 30, urx, ury - 30);
            RadioButtonOptionField radio2 = new RadioButtonOptionField(page, cellRect2)
            {
                PartialName = groupName,
                Value = "Option2"
            };
            radio2.Border = new Border(radio2) { Width = 1 };
            radio2.Color = Color.Black;
            SetOptionalRadioProperties(radio2, isChecked: false, backgroundColor: Color.White);
            doc.Form.Add(radio2);

            // ---------------------------------------------------------------------
            // 4. Save the modified PDF.
            // ---------------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved PDF with radio button group to '{outputPath}'.");
    }

    // Helper that uses dynamic dispatch to set properties that may be missing in some Aspose.Pdf versions.
    private static void SetOptionalRadioProperties(RadioButtonOptionField field, bool isChecked, Color backgroundColor)
    {
        // "Checked" property – present on some versions of RadioButtonOptionField.
        try
        {
            dynamic dyn = field;
            dyn.Checked = isChecked;
        }
        catch { /* Property not available – ignore */ }

        // "BackgroundColor" property – present on some versions.
        try
        {
            dynamic dyn = field;
            dyn.BackgroundColor = backgroundColor;
        }
        catch { /* Property not available – ignore */ }
    }
}
