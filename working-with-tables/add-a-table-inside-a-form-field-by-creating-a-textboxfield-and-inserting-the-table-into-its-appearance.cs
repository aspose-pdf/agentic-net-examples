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
        const string outputPath = "TableInFormField.pdf";

        // Create a new PDF document and add a blank page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Define the rectangle where the TextBoxField will be placed
            // Fully qualified to avoid ambiguity with System.Drawing
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 500, 400, 700);

            // Create a TextBoxField on the page
            TextBoxField textBox = new TextBoxField(page, fieldRect)
            {
                Name = "TableField",          // field name
                PartialName = "TableField",   // partial name (optional)
                Multiline = true,             // allow multiple lines (helps with larger content)
                ReadOnly = false
            };

            // Add the field to the document's form (page number is 1‑based)
            doc.Form.Add(textBox, 1);

            // ------------------------------------------------------------
            // Build a simple table using the tagged content API
            // ------------------------------------------------------------
            ITaggedContent tagged = doc.TaggedContent;

            // Create the root table element
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

            // ------------------------------------------------------------
            // Attach the table as an additional appearance of the field
            // The appearance is placed on the same page and rectangle as the field
            // ------------------------------------------------------------
            doc.Form.AddFieldAppearance(textBox, 1, fieldRect);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with table inside a TextBoxField saved to '{outputPath}'.");
    }
}