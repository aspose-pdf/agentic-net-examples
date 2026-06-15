using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;

namespace AddRadioButtonGroupInTableCell
{
    class Program
    {
        static void Main(string[] args)
        {
            // Step 1: Create a sample PDF (self‑contained example)
            using (Document sampleDoc = new Document())
            {
                sampleDoc.Pages.Add();
                sampleDoc.Save("input.pdf");
            }

            // Step 2: Open the sample PDF and add a table with a radio‑button group inside a cell
            using (Document doc = new Document("input.pdf"))
            {
                Page page = doc.Pages[1];

                // Create a table with a single column
                Table table = new Table();
                table.ColumnWidths = "200";

                // Add a row and a cell
                Row row = table.Rows.Add();
                Cell cell = row.Cells.Add();

                // Create a radio‑button field that will act as a group
                RadioButtonField radioGroup = new RadioButtonField(page);
                radioGroup.PartialName = "SampleGroup";

                // Define rectangles for each option (coordinates are page‑relative)
                Rectangle rect1 = new Rectangle(50, 700, 70, 720);
                Rectangle rect2 = new Rectangle(50, 680, 70, 700);
                Rectangle rect3 = new Rectangle(50, 660, 70, 680);

                // Add three options to the same radio‑button group
                radioGroup.AddOption("Option A", rect1);
                radioGroup.AddOption("Option B", rect2);
                radioGroup.AddOption("Option C", rect3);

                // Register the field with the document form
                doc.Form.Add(radioGroup);

                // Place the radio‑button group inside the table cell
                cell.Paragraphs.Add(radioGroup);

                // Add the table to the page
                page.Paragraphs.Add(table);

                // Save the modified PDF
                doc.Save("output.pdf");
            }
        }
    }
}