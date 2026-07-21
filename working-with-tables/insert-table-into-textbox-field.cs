using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";          // source PDF
        const string outputPath = "output_with_table.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF (Document lifecycle must be used)
        using (Document doc = new Document(inputPath))
        {
            // -----------------------------------------------------------------
            // 1. Create a TextBoxField that will host the table appearance.
            // -----------------------------------------------------------------
            // Define the rectangle where the field will be placed on page 1.
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 500, 300, 600);
            // Use the constructor that takes a Document and a Rectangle.
            TextBoxField txtField = new TextBoxField(doc, fieldRect)
            {
                Name = "TableField",          // field name
                PartialName = "TableField",   // partial name (optional)
                Multiline = true,             // allow multiline content
                ReadOnly = false
            };

            // Add the field to the form on page 1 (page numbers are 1‑based).
            doc.Form.Add(txtField, 1);

            // -----------------------------------------------------------------
            // 2. Build a tagged TableElement that represents the table data.
            // -----------------------------------------------------------------
            ITaggedContent tagged = doc.TaggedContent;

            // Root element of the tagged structure.
            StructureElement root = tagged.RootElement;

            // Create the table element.
            TableElement table = tagged.CreateTableElement();
            table.AlternativeText = "Sample data table";

            // ----- Table Header -----
            TableTHeadElement thead = tagged.CreateTableTHeadElement();
            table.AppendChild(thead);
            TableTRElement headerRow = tagged.CreateTableTRElement();
            thead.AppendChild(headerRow);

            TableTHElement th1 = tagged.CreateTableTHElement();
            th1.SetText("Product");
            headerRow.AppendChild(th1);

            TableTHElement th2 = tagged.CreateTableTHElement();
            th2.SetText("Quantity");
            headerRow.AppendChild(th2);

            // ----- Table Body -----
            TableTBodyElement tbody = tagged.CreateTableTBodyElement();
            table.AppendChild(tbody);
            TableTRElement bodyRow = tagged.CreateTableTRElement();
            tbody.AppendChild(bodyRow);

            TableTDElement td1 = tagged.CreateTableTDElement();
            td1.SetText("Widget A");
            bodyRow.AppendChild(td1);

            TableTDElement td2 = tagged.CreateTableTDElement();
            td2.SetText("42");
            bodyRow.AppendChild(td2);

            // Attach the table to the document's logical structure.
            root.AppendChild(table);

            // -----------------------------------------------------------------
            // 3. Associate the table appearance with the TextBoxField.
            // -----------------------------------------------------------------
            // Form.AddFieldAppearance adds an additional appearance stream for the field.
            // The rectangle supplied must match the field rectangle.
            doc.Form.AddFieldAppearance(txtField, 1, fieldRect);

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with table inside TextBoxField saved to '{outputPath}'.");
    }
}