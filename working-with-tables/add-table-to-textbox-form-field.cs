using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;
using Aspose.Pdf.Annotations; // Added for Border class

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_table_field.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the source PDF
        using (Document doc = new Document(inputPath))
        {
            // ------------------------------------------------------------
            // 1. Create a TextBoxField that will host the table appearance
            // ------------------------------------------------------------
            // Define the rectangle where the field will be placed (coordinates in points)
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 500, 400, 700);
            TextBoxField txtField = new TextBoxField(doc, fieldRect)
            {
                // Optional visual settings
                Color = Aspose.Pdf.Color.LightGray, // Use Aspose.Pdf.Color
                Multiline = true,
                Name = "TableField"
            };

            // Set the border after the TextBoxField instance has been created
            txtField.Border = new Border(txtField) { Width = 1 };
            // Border color is controlled by the annotation's Color property (already set above)

            // Add the field to the document's form (page number is 1‑based)
            doc.Form.Add(txtField, 1);

            // ------------------------------------------------------------
            // 2. Build a tagged Table structure (will appear as a separate
            //    element in the PDF's logical structure)
            // ------------------------------------------------------------
            ITaggedContent tagged = doc.TaggedContent;

            // Ensure the document is marked as tagged (presence of TaggedContent implies tagging)
            StructureElement root = tagged.RootElement;

            // Create the table element
            TableElement table = tagged.CreateTableElement();
            table.AlternativeText = "Sample data table";

            // Create table header (THead) with two columns
            TableTHeadElement thead = tagged.CreateTableTHeadElement();
            table.AppendChild(thead);
            TableTRElement headerRow = tagged.CreateTableTRElement();
            thead.AppendChild(headerRow);

            TableTHElement th1 = tagged.CreateTableTHElement();
            th1.SetText("Product");
            headerRow.AppendChild(th1);

            TableTHElement th2 = tagged.CreateTableTHElement();
            th2.SetText("Price");
            headerRow.AppendChild(th2);

            // Create table body (TBody) with one data row
            TableTBodyElement tbody = tagged.CreateTableTBodyElement();
            table.AppendChild(tbody);
            TableTRElement dataRow = tagged.CreateTableTRElement();
            tbody.AppendChild(dataRow);

            TableTDElement td1 = tagged.CreateTableTDElement();
            td1.SetText("Widget A");
            dataRow.AppendChild(td1);

            TableTDElement td2 = tagged.CreateTableTDElement();
            td2.SetText("$123.45");
            dataRow.AppendChild(td2);

            // Attach the table to the root of the logical structure
            root.AppendChild(table);

            // ------------------------------------------------------------
            // 3. Add an additional appearance for the TextBoxField that
            //    visually aligns with the table rectangle (optional)
            // ------------------------------------------------------------
            // The appearance rectangle can be the same as the field rectangle
            doc.Form.AddFieldAppearance(txtField, 1, fieldRect);

            // ------------------------------------------------------------
            // 4. Save the modified PDF
            // ------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with TextBoxField and embedded table: {outputPath}");
    }
}
