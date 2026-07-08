using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.Annotations; // needed for Border
using Aspose.Pdf.LogicalStructure; // <-- added for table‑related classes

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_table_field.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF (no special load options needed for a regular PDF)
        using (Document doc = new Document(inputPath))
        {
            // ------------------------------------------------------------
            // 1. Create a TextBoxField on page 1
            // ------------------------------------------------------------
            // Define the rectangle where the field will be placed (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 500, 400, 600);
            TextBoxField txtField = new TextBoxField(doc.Pages[1], fieldRect)
            {
                Name = "TableField",
                PartialName = "TableField",
                Multiline = true,               // allow multiple lines (useful for table‑like text)
                Color = Aspose.Pdf.Color.LightGray // background color of the field
            };

            // Border must be created after the field instance because the Border ctor
            // requires the parent annotation (the field) as an argument.
            txtField.Border = new Border(txtField) { Width = 1 };

            // Add the field to the document's form
            doc.Form.Add(txtField);

            // ------------------------------------------------------------
            // 2. Create a Table structure element (tagged PDF) that will
            //    serve as the visual representation of the table.
            // ------------------------------------------------------------
            ITaggedContent tagged = doc.TaggedContent;
            StructureElement root = tagged.RootElement;

            // Create the table element
            TableElement table = tagged.CreateTableElement();
            table.AlternativeText = "Sample data table";
            root.AppendChild(table);

            // Table header (THead) with a single row and two header cells
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

            // Table body (TBody) with one data row
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

            // ------------------------------------------------------------
            // 3. Associate the table appearance with the TextBoxField.
            //    The Form.AddFieldAppearance method adds an additional
            //    appearance stream for the field on a specific page.
            //    We use the same rectangle as the field itself.
            // ------------------------------------------------------------
            doc.Form.AddFieldAppearance(txtField, 1, fieldRect);

            // ------------------------------------------------------------
            // 4. Save the modified PDF
            // ------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
