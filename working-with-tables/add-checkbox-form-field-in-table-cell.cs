using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // for widget annotations
using Aspose.Pdf.Tagged;      // ITaggedContent
using Aspose.Pdf.LogicalStructure; // structure element types
using Aspose.Pdf.Forms;       // form field types (CheckboxField, etc.)

class Program
{
    static void Main()
    {
        const string outputPath = "checkbox_in_table.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page (required for placing the form field)
            Page page = doc.Pages.Add();

            // ----- Build a simple table using the tagged PDF API -----
            ITaggedContent tagged = doc.TaggedContent;
            // Set language and title for accessibility (optional)
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Table with Checkbox");

            // Root element of the structure tree
            StructureElement root = tagged.RootElement;

            // Create a table element and attach it to the root
            TableElement table = tagged.CreateTableElement();
            table.AlternativeText = "Sample data table with a checkbox in the first cell.";
            root.AppendChild(table);

            // Create table body, a single row, and a single cell
            TableTBodyElement tbody = tagged.CreateTableTBodyElement();
            table.AppendChild(tbody);

            TableTRElement row = tagged.CreateTableTRElement();
            tbody.AppendChild(row);

            TableTDElement cell = tagged.CreateTableTDElement();
            // The cell can contain placeholder text; the checkbox will be positioned over it
            cell.SetText(" "); // empty space to reserve the cell
            row.AppendChild(cell);

            // ----- Add a Checkbox form field positioned inside the cell -----
            // For demonstration we use fixed coordinates that match the cell location.
            // In a real scenario you would calculate the cell's rectangle via layout APIs.
            Aspose.Pdf.Rectangle checkboxRect = new Aspose.Pdf.Rectangle(
                llx: 100, // lower‑left X
                lly: 700, // lower‑left Y
                urx: 120, // upper‑right X
                ury: 720  // upper‑right Y
            );

            // Create the checkbox field on the first page
            CheckboxField checkBox = new CheckboxField(page, checkboxRect)
            {
                PartialName = "CheckBox1", // field name
                Checked = false            // default state (unchecked). Use true to check.
            };

            // Add the checkbox to the document's form collection
            doc.Form.Add(checkBox, 1);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with checkbox saved to '{outputPath}'.");
    }
}
