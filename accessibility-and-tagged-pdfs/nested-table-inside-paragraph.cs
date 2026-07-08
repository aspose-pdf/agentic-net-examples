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
        const string outputPath = "tagged_nested_table.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF (no special load options needed for a regular PDF)
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content API
            ITaggedContent tagged = doc.TaggedContent;

            // Set language and title for the tagged document
            tagged.SetLanguage("en-US");
            tagged.SetTitle("PDF with nested table");

            // Root element of the logical structure tree
            StructureElement root = tagged.RootElement;

            // -------------------------------------------------
            // 1. Create a paragraph element and add it to root
            // -------------------------------------------------
            ParagraphElement paragraph = tagged.CreateParagraphElement();
            paragraph.SetText("This paragraph contains a table with a nested table.");
            root.AppendChild(paragraph); // Append paragraph to the root

            // -------------------------------------------------
            // 2. Create the outer table and attach it to the paragraph
            // -------------------------------------------------
            TableElement outerTable = tagged.CreateTableElement();
            outerTable.AlternativeText = "Outer table";
            paragraph.AppendChild(outerTable); // Table is child of paragraph

            // Create table head (optional)
            TableTHeadElement outerHead = tagged.CreateTableTHeadElement();
            outerTable.AppendChild(outerHead);
            TableTRElement headRow = tagged.CreateTableTRElement();
            outerHead.AppendChild(headRow);
            TableTHElement headCell = tagged.CreateTableTHElement();
            headCell.SetText("Header");
            headRow.AppendChild(headCell);

            // Create table body
            TableTBodyElement outerBody = tagged.CreateTableTBodyElement();
            outerTable.AppendChild(outerBody);
            TableTRElement bodyRow = tagged.CreateTableTRElement();
            outerBody.AppendChild(bodyRow);

            // First cell of outer table (will contain the nested table)
            TableTDElement outerCellWithNested = tagged.CreateTableTDElement();
            outerCellWithNested.SetText("Cell with nested table:");
            bodyRow.AppendChild(outerCellWithNested);

            // -------------------------------------------------
            // 3. Create the nested table and attach it to the cell
            // -------------------------------------------------
            TableElement innerTable = tagged.CreateTableElement();
            innerTable.AlternativeText = "Inner nested table";
            outerCellWithNested.AppendChild(innerTable); // Nested table is child of the outer cell

            // Inner table head
            TableTHeadElement innerHead = tagged.CreateTableTHeadElement();
            innerTable.AppendChild(innerHead);
            TableTRElement innerHeadRow = tagged.CreateTableTRElement();
            innerHead.AppendChild(innerHeadRow);
            TableTHElement innerHeadCell = tagged.CreateTableTHElement();
            innerHeadCell.SetText("Inner Header");
            innerHeadRow.AppendChild(innerHeadCell);

            // Inner table body
            TableTBodyElement innerBody = tagged.CreateTableTBodyElement();
            innerTable.AppendChild(innerBody);
            TableTRElement innerBodyRow = tagged.CreateTableTRElement();
            innerBody.AppendChild(innerBodyRow);
            TableTDElement innerCell = tagged.CreateTableTDElement();
            innerCell.SetText("Nested cell content");
            innerBodyRow.AppendChild(innerCell);

            // -------------------------------------------------
            // 4. Validation: walk the structure and print hierarchy
            // -------------------------------------------------
            Console.WriteLine("Tagging hierarchy:");
            PrintElement(root, 0);

            // Save the modified PDF
            doc.Save(outputPath);
            Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
        }
    }

    // Recursive helper to display element type and optional text
    static void PrintElement(StructureElement element, int depth)
    {
        string indent = new string(' ', depth * 2);
        string info = element.GetType().Name;

        // Show text for paragraph, table cells, etc.
        if (!string.IsNullOrEmpty(element.ActualText))
            info += $" (Text: \"{element.ActualText}\")";

        Console.WriteLine($"{indent}{info}");

        foreach (Element child in element.ChildElements)
        {
            if (child is StructureElement se)
                PrintElement(se, depth + 1);
        }
    }
}