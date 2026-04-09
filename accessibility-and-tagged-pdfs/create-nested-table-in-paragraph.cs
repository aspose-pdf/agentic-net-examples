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

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Access tagged‑content API
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Root element of the logical structure tree
            StructureElement root = tagged.RootElement;

            // -------------------------------------------------
            // 1. Create a paragraph element and add some text
            // -------------------------------------------------
            ParagraphElement paragraph = tagged.CreateParagraphElement();
            paragraph.SetText("This paragraph contains a nested table.");
            root.AppendChild(paragraph); // attach paragraph to root

            // -------------------------------------------------
            // 2. Create a table element (nested inside the paragraph)
            // -------------------------------------------------
            TableElement table = tagged.CreateTableElement();
            table.AlternativeText = "Sample nested table";
            paragraph.AppendChild(table); // attach table to paragraph

            // -------------------------------------------------
            // 3. Build table header (THead) with two columns
            // -------------------------------------------------
            TableTHeadElement thead = tagged.CreateTableTHeadElement();
            table.AppendChild(thead);

            TableTRElement headerRow = tagged.CreateTableTRElement();
            thead.AppendChild(headerRow);

            TableTHElement th1 = tagged.CreateTableTHElement();
            th1.SetText("Column A");
            headerRow.AppendChild(th1);

            TableTHElement th2 = tagged.CreateTableTHElement();
            th2.SetText("Column B");
            headerRow.AppendChild(th2);

            // -------------------------------------------------
            // 4. Build table body (TBody) with one data row
            // -------------------------------------------------
            TableTBodyElement tbody = tagged.CreateTableTBodyElement();
            table.AppendChild(tbody);

            TableTRElement dataRow = tagged.CreateTableTRElement();
            tbody.AppendChild(dataRow);

            TableTDElement td1 = tagged.CreateTableTDElement();
            td1.SetText("Value 1");
            dataRow.AppendChild(td1);

            TableTDElement td2 = tagged.CreateTableTDElement();
            td2.SetText("Value 2");
            dataRow.AppendChild(td2);

            // -------------------------------------------------
            // 5. Validate the tagging hierarchy
            // -------------------------------------------------
            Console.WriteLine("=== Tagging Hierarchy Validation ===");
            ValidateStructure(root);
            // -------------------------------------------------
            // Save the modified PDF (no PreSave required)
            // -------------------------------------------------
            doc.Save(outputPath);
            Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
        }
    }

    // Recursively walk the structure tree and print element types
    static void ValidateStructure(StructureElement element, int depth = 0)
    {
        string indent = new string(' ', depth * 2);
        Console.WriteLine($"{indent}{element.GetType().Name}");

        foreach (Element child in element.ChildElements)
        {
            if (child is StructureElement se)
                ValidateStructure(se, depth + 1);
        }
    }
}