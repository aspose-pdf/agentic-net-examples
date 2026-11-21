using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";          // existing PDF (can be empty)
        const string outputPath = "nested_table_tagged.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the document and work with its tagged content
        using (Document doc = new Document(inputPath))
        {
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Nested Table Example");

            // Root of the tagging tree
            StructureElement root = tagged.RootElement;

            // -------------------------------------------------
            // 1. Create a paragraph element
            // -------------------------------------------------
            ParagraphElement paragraph = tagged.CreateParagraphElement();
            paragraph.SetText("Paragraph containing a table:");
            root.AppendChild(paragraph); // attach paragraph to root

            // -------------------------------------------------
            // 2. Create the outer table and attach it to the paragraph
            // -------------------------------------------------
            TableElement outerTable = tagged.CreateTableElement();
            outerTable.AlternativeText = "Outer table";
            paragraph.AppendChild(outerTable);

            // Header row for outer table
            TableTHeadElement outerHead = tagged.CreateTableTHeadElement();
            outerTable.AppendChild(outerHead);
            TableTRElement outerHeaderRow = tagged.CreateTableTRElement();
            outerHead.AppendChild(outerHeaderRow);
            TableTHElement outerTh1 = tagged.CreateTableTHElement();
            outerTh1.SetText("Header 1");
            outerHeaderRow.AppendChild(outerTh1);
            TableTHElement outerTh2 = tagged.CreateTableTHElement();
            outerTh2.SetText("Header 2");
            outerHeaderRow.AppendChild(outerTh2);

            // Body row for outer table
            TableTBodyElement outerBody = tagged.CreateTableTBodyElement();
            outerTable.AppendChild(outerBody);
            TableTRElement outerBodyRow = tagged.CreateTableTRElement();
            outerBody.AppendChild(outerBodyRow);
            TableTDElement outerTd1 = tagged.CreateTableTDElement();
            outerTd1.SetText("Cell 1");
            outerBodyRow.AppendChild(outerTd1);
            TableTDElement outerTd2 = tagged.CreateTableTDElement();
            outerTd2.SetText("Cell 2 with nested table");
            outerBodyRow.AppendChild(outerTd2);

            // -------------------------------------------------
            // 3. Create a nested table inside the second cell (outerTd2)
            // -------------------------------------------------
            TableElement nestedTable = tagged.CreateTableElement();
            nestedTable.AlternativeText = "Nested table";
            outerTd2.AppendChild(nestedTable);

            // Header for nested table
            TableTHeadElement nestedHead = tagged.CreateTableTHeadElement();
            nestedTable.AppendChild(nestedHead);
            TableTRElement nestedHeaderRow = tagged.CreateTableTRElement();
            nestedHead.AppendChild(nestedHeaderRow);
            TableTHElement nestedTh = tagged.CreateTableTHElement();
            nestedTh.SetText("NHeader");
            nestedHeaderRow.AppendChild(nestedTh);

            // Body for nested table
            TableTBodyElement nestedBody = tagged.CreateTableTBodyElement();
            nestedTable.AppendChild(nestedBody);
            TableTRElement nestedBodyRow = tagged.CreateTableTRElement();
            nestedBody.AppendChild(nestedBodyRow);
            TableTDElement nestedTd = tagged.CreateTableTDElement();
            nestedTd.SetText("Nested cell");
            nestedBodyRow.AppendChild(nestedTd);

            // -------------------------------------------------
            // 4. Save the modified document
            // -------------------------------------------------
            doc.Save(outputPath);
        }

        // -------------------------------------------------
        // 5. Validation: walk the tagging hierarchy and
        //    verify that we have exactly two tables, the
        //    outer one under the paragraph and the inner
        //    one under a table cell.
        // -------------------------------------------------
        using (Document doc = new Document(outputPath))
        {
            ITaggedContent tagged = doc.TaggedContent;
            StructureElement root = tagged.RootElement;

            // Count all TableElement instances
            var allTables = root.FindElements<TableElement>(true);
            Console.WriteLine($"Total tables found: {allTables.Count}");
            if (allTables.Count != 2)
            {
                Console.WriteLine("Validation failed: expected 2 tables.");
                return;
            }

            // Identify outer and inner tables by their parent types
            TableElement outer = null;
            TableElement inner = null;
            foreach (var tbl in allTables)
            {
                // Parent can be obtained via the ParentElement property (available on Element)
                var parent = tbl.ParentElement;
                if (parent is ParagraphElement)
                    outer = tbl;
                else if (parent is TableTDElement)
                    inner = tbl;
            }

            if (outer == null || inner == null)
            {
                Console.WriteLine("Validation failed: could not locate outer or inner table correctly.");
                return;
            }

            Console.WriteLine("Validation succeeded: outer table is child of ParagraphElement, inner table is child of TableTDElement.");

            // Optional: print the full hierarchy for visual inspection
            Console.WriteLine("\nTagging hierarchy:");
            PrintStructure(root, 0);
        }
    }

    // Recursive helper to print the structure tree
    static void PrintStructure(StructureElement element, int depth)
    {
        string indent = new string(' ', depth * 2);
        Console.WriteLine($"{indent}{element.GetType().Name}: {element.ActualText ?? "(no text)"}");
        foreach (Element child in element.ChildElements)
        {
            if (child is StructureElement se)
                PrintStructure(se, depth + 1);
        }
    }
}