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

        // Load the source PDF
        using (Document doc = new Document(inputPath))
        {
            // Access tagged content (creates a tagged structure if none exists)
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle("PDF with List in Table Cell");

            // Root element of the structure tree
            var root = tagged.RootElement;

            // Create a table and add it to the root
            TableElement table = tagged.CreateTableElement();
            table.AlternativeText = "Sample table with a list in a cell";
            root.AppendChild(table);

            // Create a table row (header)
            TableTRElement headerRow = tagged.CreateTableTRElement();
            table.AppendChild(headerRow);

            // Header cell
            TableTHElement th = tagged.CreateTableTHElement();
            th.SetText("Items");
            headerRow.AppendChild(th);

            // Create a table row for the data cell
            TableTRElement dataRow = tagged.CreateTableTRElement();
            table.AppendChild(dataRow);

            // Data cell where the list will be placed
            TableTDElement td = tagged.CreateTableTDElement();
            dataRow.AppendChild(td);

            // Add three list items as paragraphs with bullet markers
            string[] items = { "First item", "Second item", "Third item" };
            foreach (string item in items)
            {
                // Create a paragraph for each list item and prepend a bullet character
                ParagraphElement para = tagged.CreateParagraphElement();
                para.SetText($"\u2022 {item}"); // \u2022 is the bullet (•) character
                td.AppendChild(para);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
