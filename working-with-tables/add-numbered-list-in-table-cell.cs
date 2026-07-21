using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;
using Aspose.Pdf.Annotations; // for AttributeKey and AttributeName if needed

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content API
            ITaggedContent tagged = doc.TaggedContent;

            // Root element of the structure tree
            StructureElement root = tagged.RootElement;

            // ------------------------------------------------------------
            // Build a table structure with one cell
            // ------------------------------------------------------------
            TableElement table = tagged.CreateTableElement();
            table.AlternativeText = "Sample table with numbered list";
            root.AppendChild(table); // attach table to the document root

            TableTBodyElement tbody = tagged.CreateTableTBodyElement();
            table.AppendChild(tbody);

            TableTRElement row = tagged.CreateTableTRElement();
            tbody.AppendChild(row);

            TableTDElement cell = tagged.CreateTableTDElement();
            cell.AlternativeText = "Cell containing a numbered list";
            row.AppendChild(cell);

            // ------------------------------------------------------------
            // Create a numbered list inside the cell
            // ------------------------------------------------------------
            ListElement list = tagged.CreateListElement();
            // Optional: set list numbering attribute (default is decimal)
            // list.Attributes.CreateAttributes(AttributeOwnerStandard.List)
            //     .SetAttribute(AttributeKey.ListNumbering, AttributeName.ListNumbering_Decimal);
            cell.AppendChild(list);

            // List body container
            ListLBodyElement lBody = tagged.CreateListLBodyElement();
            list.AppendChild(lBody);

            // Add three list items
            for (int i = 1; i <= 3; i++)
            {
                // List item element
                ListLIElement li = tagged.CreateListLIElement();

                // Paragraph element to hold the text of the list item
                ParagraphElement para = tagged.CreateParagraphElement();
                para.SetText($"Item {i}");

                // Assemble the hierarchy: Paragraph -> LI -> LBody
                li.AppendChild(para);
                lBody.AppendChild(li);
            }

            // Save the modified PDF (lifecycle rule: save inside using)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with numbered list saved to '{outputPath}'.");
    }
}