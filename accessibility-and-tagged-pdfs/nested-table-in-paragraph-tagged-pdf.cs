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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Access tagged content and set basic properties
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Nested Table Example");

            // Root element of the logical structure
            StructureElement root = tagged.RootElement;

            // Create a paragraph that will contain the outer table
            ParagraphElement paragraph = tagged.CreateParagraphElement();
            paragraph.SetText("Paragraph containing a nested table.");
            root.AppendChild(paragraph);

            // Create the outer table and attach it to the paragraph
            TableElement outerTable = tagged.CreateTableElement();
            outerTable.AlternativeText = "Outer table";
            paragraph.AppendChild(outerTable);

            // Build outer table header
            TableTHeadElement outerHead = tagged.CreateTableTHeadElement();
            outerTable.AppendChild(outerHead);
            TableTRElement outerHeaderRow = tagged.CreateTableTRElement();
            outerHead.AppendChild(outerHeaderRow);
            TableTHElement outerHeaderCell = tagged.CreateTableTHElement();
            outerHeaderCell.SetText("Header");
            outerHeaderRow.AppendChild(outerHeaderCell);

            // Build outer table body with a cell that will hold the nested table
            TableTBodyElement outerBody = tagged.CreateTableTBodyElement();
            outerTable.AppendChild(outerBody);
            TableTRElement outerBodyRow = tagged.CreateTableTRElement();
            outerBody.AppendChild(outerBodyRow);
            TableTDElement outerCell = tagged.CreateTableTDElement();
            outerCell.SetText("Cell with nested table:");
            outerBodyRow.AppendChild(outerCell);

            // Create the nested table and attach it to the outer cell
            TableElement nestedTable = tagged.CreateTableElement();
            nestedTable.AlternativeText = "Nested table";
            outerCell.AppendChild(nestedTable);

            // Build nested table body with two cells
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

            // Validate hierarchy by printing element types and their parents
            Console.WriteLine("Tagging hierarchy:");
            PrintElement(paragraph, 0);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
    }

    static void PrintElement(StructureElement element, int depth)
    {
        string indent = new string(' ', depth * 2);
        string parentName = element.ParentElement?.GetType().Name ?? "None";
        Console.WriteLine($"{indent}{element.GetType().Name} (Parent: {parentName})");

        foreach (Element child in element.ChildElements)
        {
            if (child is StructureElement se)
                PrintElement(se, depth + 1);
        }
    }
}