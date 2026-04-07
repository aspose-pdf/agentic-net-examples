using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Input and output PDF paths
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // ------------------------------------------------------------
            // Create a simple table with one cell where the radio buttons
            // will be visually placed. This demonstrates adding the field
            // inside a cell; the actual field rectangle is defined explicitly.
            // ------------------------------------------------------------
            Table table = new Table
            {
                // Optional: set column widths (percentage of page width)
                ColumnWidths = "100%"
            };

            // Add a single row and a single cell
            Row row = table.Rows.Add();
            Cell cell = row.Cells.Add();

            // Set some visual properties for the cell (optional)
            cell.BackgroundColor = Aspose.Pdf.Color.LightGray;
            cell.Margin = new MarginInfo(5, 5, 5, 5);

            // Add the table to the page
            page.Paragraphs.Add(table);

            // ------------------------------------------------------------
            // Create a RadioButtonField that will act as a group.
            // All options added to this field share the same name,
            // thus forming a button group.
            // ------------------------------------------------------------
            RadioButtonField radioGroup = new RadioButtonField(page)
            {
                // The field name identifies the group; use the same name for all options
                Name = "SampleRadioGroup",
                // Optional: set the appearance color
                Color = Aspose.Pdf.Color.Blue,
                // Ensure at least one option can be selected (default is true)
                NoToggleToOff = true
            };

            // Define rectangles for each option inside the cell.
            // Coordinates are in points; adjust as needed.
            // Here we place two options vertically stacked.
            Aspose.Pdf.Rectangle optionRect1 = new Aspose.Pdf.Rectangle(100, 500, 120, 520);
            Aspose.Pdf.Rectangle optionRect2 = new Aspose.Pdf.Rectangle(100, 470, 120, 490);

            // Add options to the radio group.
            // The AddOption method creates a sub‑field (RadioButtonOptionField)
            // with the specified export value and rectangle.
            radioGroup.AddOption("Option A", optionRect1);
            radioGroup.AddOption("Option B", optionRect2);

            // Add the radio button group to the page annotations collection.
            page.Annotations.Add(radioGroup);

            // ------------------------------------------------------------
            // Save the modified PDF (lifecycle rule: use Document.Save)
            // ------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with radio button group saved to '{outputPath}'.");
    }
}