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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Set document language and title for accessibility
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Nested Table Example");

            // Root of the logical structure tree
            StructureElement root = tagged.RootElement;

            // Create a paragraph element
            ParagraphElement paragraph = tagged.CreateParagraphElement();
            paragraph.SetText("Paragraph containing a table with a nested table.");
            root.AppendChild(paragraph);

            // ----- Outer table -----
            TableElement outerTable = tagged.CreateTableElement();
            outerTable.AlternativeText = "Outer table";
            paragraph.AppendChild(outerTable);

            TableTBodyElement outerTBody = tagged.CreateTableTBodyElement();
            outerTable.AppendChild(outerTBody);

            TableTRElement outerRow = tagged.CreateTableTRElement();
            outerTBody.AppendChild(outerRow);

            TableTDElement cell1 = tagged.CreateTableTDElement();
            cell1.SetText("Cell 1");
            outerRow.AppendChild(cell1);

            TableTDElement cell2 = tagged.CreateTableTDElement();
            cell2.SetText("Cell 2 (contains nested table)");
            outerRow.AppendChild(cell2);

            // ----- Nested table inside cell2 -----
            TableElement nestedTable = tagged.CreateTableElement();
            nestedTable.AlternativeText = "Nested table";
            cell2.AppendChild(nestedTable);

            TableTBodyElement nestedTBody = tagged.CreateTableTBodyElement();
            nestedTable.AppendChild(nestedTBody);

            TableTRElement nestedRow = tagged.CreateTableTRElement();
            nestedTBody.AppendChild(nestedRow);

            TableTDElement nestedCell1 = tagged.CreateTableTDElement();
            nestedCell1.SetText("Nested 1");
            nestedRow.AppendChild(nestedCell1);

            TableTDElement nestedCell2 = tagged.CreateTableTDElement();
            nestedCell2.SetText("Nested 2");
            nestedRow.AppendChild(nestedCell2);

            // Validate hierarchy by walking the structure tree
            Console.WriteLine("Structure hierarchy:");
            Walk(root, 0);

            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
    }

    static void Walk(StructureElement element, int depth)
    {
        string indent = new string(' ', depth * 2);
        string description = element.AlternativeText ?? element.ActualText ?? string.Empty;
        Console.WriteLine($"{indent}{element.GetType().Name}: {description}");
        foreach (Element child in element.ChildElements)
        {
            if (child is StructureElement se)
                Walk(se, depth + 1);
        }
    }
}