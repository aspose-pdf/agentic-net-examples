using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string outputPath = "nested_table.pdf";

        // Create a new PDF document and add a blank page
        using (Document doc = new Document())
        {
            doc.Pages.Add(); // first (and only) page

            // Access the tagged‑content API
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Nested Table Example");

            // Root element of the logical structure tree
            StructureElement root = tagged.RootElement;

            // Create a paragraph element and attach it to the root
            ParagraphElement paragraph = tagged.CreateParagraphElement();
            paragraph.SetText("This paragraph contains a nested table.");
            root.AppendChild(paragraph);

            // Create a table element and attach it to the paragraph (nested table)
            TableElement table = tagged.CreateTableElement();
            table.AlternativeText = "Sample 2x2 table";
            paragraph.AppendChild(table);

            // ----- Build table header -----
            TableTHeadElement thead = tagged.CreateTableTHeadElement();
            table.AppendChild(thead);
            TableTRElement headerRow = tagged.CreateTableTRElement();
            thead.AppendChild(headerRow);
            TableTHElement th1 = tagged.CreateTableTHElement();
            th1.SetText("Header 1");
            headerRow.AppendChild(th1);
            TableTHElement th2 = tagged.CreateTableTHElement();
            th2.SetText("Header 2");
            headerRow.AppendChild(th2);

            // ----- Build table body -----
            TableTBodyElement tbody = tagged.CreateTableTBodyElement();
            table.AppendChild(tbody);
            TableTRElement bodyRow = tagged.CreateTableTRElement();
            tbody.AppendChild(bodyRow);
            TableTDElement td1 = tagged.CreateTableTDElement();
            td1.SetText("Cell 1");
            bodyRow.AppendChild(td1);
            TableTDElement td2 = tagged.CreateTableTDElement();
            td2.SetText("Cell 2");
            bodyRow.AppendChild(td2);

            // ----- Validate hierarchy -----
            Console.WriteLine("Logical Structure Hierarchy:");
            Walk(root, 0);

            // Verify that the table is a child of the paragraph
            bool tableIsNested = false;
            foreach (Element child in paragraph.ChildElements)
            {
                if (child is TableElement)
                {
                    tableIsNested = true;
                    break;
                }
            }
            Console.WriteLine($"Table nested inside paragraph: {tableIsNested}");

            // Save the PDF – guard against missing GDI+ on non‑Windows platforms
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
            else
            {
                try
                {
                    doc.Save(outputPath);
                    Console.WriteLine($"PDF saved to '{outputPath}'.");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF not saved.");
                }
            }
        }
    }

    // Recursive walk to display element types and their actual text
    static void Walk(StructureElement element, int depth)
    {
        string indent = new string(' ', depth * 2);
        string text = element.ActualText ?? string.Empty;
        Console.WriteLine($"{indent}{element.GetType().Name}: {text}");
        foreach (Element child in element.ChildElements)
        {
            if (child is StructureElement se)
                Walk(se, depth + 1);
        }
    }

    // Helper to detect a missing native GDI+ library (libgdiplus) in the exception chain
    private static bool ContainsDllNotFound(Exception? ex)
    {
        while (ex != null)
        {
            if (ex is DllNotFoundException)
                return true;
            ex = ex.InnerException;
        }
        return false;
    }
}
