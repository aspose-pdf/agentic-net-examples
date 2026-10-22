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
        const string outputPath = "output_tagged.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content API
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Root of the logical structure tree
            StructureElement root = tagged.RootElement;

            // Create a table element and attach it to the root
            TableElement table = tagged.CreateTableElement();
            table.AlternativeText = "Sample table with numbered list";
            root.AppendChild(table);

            // Create table body
            TableTBodyElement tbody = tagged.CreateTableTBodyElement();
            table.AppendChild(tbody);

            // Create a single row
            TableTRElement row = tagged.CreateTableTRElement();
            tbody.AppendChild(row);

            // Create a single cell (TD)
            TableTDElement cell = tagged.CreateTableTDElement();
            cell.AlternativeText = "Cell containing a numbered list";
            row.AppendChild(cell);

            // Create a List element (ordered/numbered list)
            ListElement list = tagged.CreateListElement();
            // By default the list is ordered (numbered)
            cell.AppendChild(list);

            // Sample list items
            string[] items = new[]
            {
                "First item of the list",
                "Second item of the list",
                "Third item of the list"
            };

            // For each item, create a Paragraph element and add it directly to the List.
            // The List element will treat each Paragraph as a list item and render numbers automatically.
            foreach (string text in items)
            {
                ParagraphElement para = tagged.CreateParagraphElement();
                para.SetText(text);
                // Append the paragraph directly to the list – no explicit ListItemElement needed.
                list.AppendChild(para);
            }

            // Save the modified PDF (no PreSave required)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF with numbered list saved to '{outputPath}'.");
    }
}
