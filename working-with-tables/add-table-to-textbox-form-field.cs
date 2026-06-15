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
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_table_field.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access the form object (creates one if it does not exist)
            Form form = doc.Form;

            // Define the rectangle where the TextBoxField will be placed (page 1)
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 500, 400, 700);

            // Create a TextBoxField that will host the table appearance
            TextBoxField txtField = new TextBoxField(doc, fieldRect);
            txtField.Name = "TableField";
            txtField.Multiline = true;   // allow multiline content
            txtField.ReadOnly = true;    // make it read‑only because we display a table

            // Add the field to the form on page 1
            form.Add(txtField, 1);

            // -------------------------------------------------
            // Build a tagged table that will be shown in the PDF
            // -------------------------------------------------
            ITaggedContent tagged = doc.TaggedContent;
            StructureElement root = tagged.RootElement;

            // Create the table element
            TableElement table = tagged.CreateTableElement();
            table.AlternativeText = "Sample data table";

            // ----- Table header -----
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

            // ----- Table body -----
            TableTBodyElement tbody = tagged.CreateTableTBodyElement();
            table.AppendChild(tbody);

            // First data row
            TableTRElement row1 = tagged.CreateTableTRElement();
            tbody.AppendChild(row1);
            TableTDElement td1 = tagged.CreateTableTDElement();
            td1.SetText("Widget A");
            row1.AppendChild(td1);
            TableTDElement td2 = tagged.CreateTableTDElement();
            td2.SetText("$10");
            row1.AppendChild(td2);

            // Second data row
            TableTRElement row2 = tagged.CreateTableTRElement();
            tbody.AppendChild(row2);
            TableTDElement td3 = tagged.CreateTableTDElement();
            td3.SetText("Widget B");
            row2.AppendChild(td3);
            TableTDElement td4 = tagged.CreateTableTDElement();
            td4.SetText("$15");
            row2.AppendChild(td4);

            // Attach the table to the document's logical structure
            root.AppendChild(table);

            // -------------------------------------------------
            // Add an additional appearance for the textbox field
            // that covers the same rectangle as the table.
            // -------------------------------------------------
            form.AddFieldAppearance(txtField, 1, fieldRect);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}