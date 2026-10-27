using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "list_in_cell.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the source PDF
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content API
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle("PDF with List Inside Table Cell");

            // Root element of the structure tree
            var root = tagged.RootElement;

            // Create a table element and attach it to the root
            TableElement table = tagged.CreateTableElement();
            table.AlternativeText = "Sample table containing a list";
            root.AppendChild(table);

            // Create a table body with one row
            TableTBodyElement tbody = tagged.CreateTableTBodyElement();
            table.AppendChild(tbody);

            TableTRElement row = tagged.CreateTableTRElement();
            tbody.AppendChild(row);

            // Create a table cell (TD) where the list will be placed
            TableTDElement cell = tagged.CreateTableTDElement();
            // Placeholder text to ensure the cell exists – it will be replaced by the list
            cell.SetText(" ");
            row.AppendChild(cell);

            // ----- Add list items inside the cell -----
            // Try to create a proper ListElement with ListItemElements. If the API version does not
            // expose ListItemElement, fall back to simple ParagraphElements prefixed with a bullet.
            string[] items = { "First item", "Second item", "Third item" };

            // Create the list container
            var list = tagged.CreateListElement();
            cell.AppendChild(list);

            // Use reflection to see whether CreateListItemElement is available
            var createListItemMethod = tagged.GetType().GetMethod("CreateListItemElement");
            bool hasListItem = createListItemMethod != null;

            foreach (string item in items)
            {
                if (hasListItem)
                {
                    // ListItem element (available in newer library versions)
                    var listItem = (StructureElement)createListItemMethod.Invoke(tagged, null);
                    list.AppendChild(listItem);

                    // Paragraph inside the list item
                    ParagraphElement para = tagged.CreateParagraphElement();
                    para.SetText(item);
                    listItem.AppendChild(para);
                }
                else
                {
                    // Fallback: simple paragraph with a bullet character
                    ParagraphElement para = tagged.CreateParagraphElement();
                    para.SetText("\u2022 " + item); // • bullet
                    list.AppendChild(para);
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
