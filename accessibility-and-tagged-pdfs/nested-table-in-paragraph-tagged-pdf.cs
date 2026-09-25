using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_tagged.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Set language and title for the tagged PDF
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Root of the structure tree
            StructureElement root = tagged.RootElement;

            // Paragraph that will contain the nested table
            ParagraphElement paragraph = tagged.CreateParagraphElement();
            paragraph.SetText("This paragraph contains a nested table:");
            root.AppendChild(paragraph);

            // Outer table
            TableElement outerTable = tagged.CreateTableElement();
            outerTable.AlternativeText = "Outer table with nested table inside first cell.";
            paragraph.AppendChild(outerTable); // Attach table to paragraph

            // Header for outer table
            TableTHeadElement thead = tagged.CreateTableTHeadElement();
            outerTable.AppendChild(thead);
            TableTRElement headerRow = tagged.CreateTableTRElement();
            thead.AppendChild(headerRow);
            TableTHElement thHeader = tagged.CreateTableTHElement();
            thHeader.SetText("Header");
            headerRow.AppendChild(thHeader);
            TableTHElement thData = tagged.CreateTableTHElement();
            thData.SetText("Data");
            headerRow.AppendChild(thData);

            // Body of outer table
            TableTBodyElement tbody = tagged.CreateTableTBodyElement();
            outerTable.AppendChild(tbody);
            TableTRElement bodyRow = tagged.CreateTableTRElement();
            tbody.AppendChild(bodyRow);

            // First cell will hold a nested table
            TableTDElement tdNested = tagged.CreateTableTDElement();

            // Nested table
            TableElement nestedTable = tagged.CreateTableElement();
            nestedTable.AlternativeText = "Nested table inside outer table cell.";
            TableTBodyElement nestedBody = tagged.CreateTableTBodyElement();
            nestedTable.AppendChild(nestedBody);
            TableTRElement nestedRow = tagged.CreateTableTRElement();
            nestedBody.AppendChild(nestedRow);
            TableTDElement nestedCell1 = tagged.CreateTableTDElement();
            nestedCell1.SetText("Nested 1");
            nestedRow.AppendChild(nestedCell1);
            TableTDElement nestedCell2 = tagged.CreateTableTDElement();
            nestedCell2.SetText("Nested 2");
            nestedRow.AppendChild(nestedCell2);

            // Attach nested table to the first cell
            tdNested.AppendChild(nestedTable);
            bodyRow.AppendChild(tdNested);

            // Second cell with regular text
            TableTDElement tdRegular = tagged.CreateTableTDElement();
            tdRegular.SetText("Regular cell");
            bodyRow.AppendChild(tdRegular);

            // Validate hierarchy by walking the structure tree
            Console.WriteLine("Tagging hierarchy:");
            WalkStructure(root, 0);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
    }

    // Recursive walk to display element types and hierarchy depth
    static void WalkStructure(StructureElement element, int depth)
    {
        string indent = new string(' ', depth * 2);
        Console.WriteLine($"{indent}{element.GetType().Name}");
        foreach (Element child in element.ChildElements)
        {
            if (child is StructureElement se)
                WalkStructure(se, depth + 1);
        }
    }
}