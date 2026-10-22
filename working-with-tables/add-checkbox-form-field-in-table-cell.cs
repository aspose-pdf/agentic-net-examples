using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // Needed for Border class

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_checkbox.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Access tagged content (creates a tagged structure if not present)
            ITaggedContent taggedContent = doc.TaggedContent;
            taggedContent.SetLanguage("en-US");
            taggedContent.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Root element of the structure tree
            StructureElement root = taggedContent.RootElement;

            // Create a table element
            TableElement table = taggedContent.CreateTableElement();
            table.AlternativeText = "Sample table with a checkbox in the first cell";
            root.AppendChild(table);

            // Create table header (optional)
            TableTHeadElement thead = taggedContent.CreateTableTHeadElement();
            table.AppendChild(thead);
            TableTRElement headerRow = taggedContent.CreateTableTRElement();
            thead.AppendChild(headerRow);
            TableTHElement th = taggedContent.CreateTableTHElement();
            th.SetText("Option");
            headerRow.AppendChild(th);

            // Create table body
            TableTBodyElement tbody = taggedContent.CreateTableTBodyElement();
            table.AppendChild(tbody);
            TableTRElement dataRow = taggedContent.CreateTableTRElement();
            tbody.AppendChild(dataRow);

            // Create a cell where the checkbox will appear
            TableTDElement td = taggedContent.CreateTableTDElement();
            td.SetText(" "); // placeholder text; actual visual will be the checkbox
            dataRow.AppendChild(td);

            // Save the document temporarily to force layout so we can obtain page dimensions
            // (Aspose.Pdf does not provide cell coordinates before layout, so we use a fixed rectangle)
            string tempPath = Path.Combine(Path.GetDirectoryName(outputPath) ?? "", "temp.pdf");
            doc.Save(tempPath);

            // Re-open the temporary PDF to add the form field on the first page
            using (Document tempDoc = new Document(tempPath))
            {
                // Define a rectangle where the checkbox will be placed.
                // Adjust coordinates (llx, lly, urx, ury) as needed for your layout.
                // Here we place it near the top‑left of the page for demonstration.
                Aspose.Pdf.Rectangle checkboxRect = new Aspose.Pdf.Rectangle(100, 700, 120, 720);

                // Create the checkbox field
                CheckboxField checkBox = new CheckboxField(tempDoc.Pages[1], checkboxRect);
                checkBox.PartialName = "SampleCheckBox";
                checkBox.Checked = false; // default state
                // Set border after construction (Border requires the parent annotation instance)
                checkBox.Border = new Border(checkBox) { Width = 1 };
                // Note: BackgroundColor is not available on CheckboxField in this version; omitted.

                // Add the field to the document's form collection
                tempDoc.Form.Add(checkBox);

                // Save final output
                tempDoc.Save(outputPath);
            }

            // Clean up temporary file
            if (File.Exists(tempPath))
                File.Delete(tempPath);
        }

        Console.WriteLine($"PDF with checkbox saved to '{outputPath}'.");
    }
}
