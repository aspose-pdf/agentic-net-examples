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
        const string inputPath = "input.pdf";
        const string outputPath = "output_tagged.pdf";

        // Create a simple PDF if the input does not exist
        if (!File.Exists(inputPath))
        {
            using (Document doc = new Document())
            {
                doc.Pages.Add();
                doc.Save(inputPath);
            }
        }

        using (Document doc = new Document(inputPath))
        {
            // Access tagged content (no casting needed)
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Nested Table Example");

            // Root element of the logical structure
            StructureElement root = tagged.RootElement;

            // Paragraph that will contain the outer table
            ParagraphElement para = tagged.CreateParagraphElement();
            para.SetText("Paragraph containing a table:");
            root.AppendChild(para);

            // Outer table
            TableElement outerTable = tagged.CreateTableElement();
            outerTable.AlternativeText = "Outer table";
            para.AppendChild(outerTable);

            // Header for outer table
            TableTHeadElement thead = tagged.CreateTableTHeadElement();
            outerTable.AppendChild(thead);
            TableTRElement headerRow = tagged.CreateTableTRElement();
            thead.AppendChild(headerRow);
            TableTHElement th1 = tagged.CreateTableTHElement();
            th1.SetText("Header 1");
            headerRow.AppendChild(th1);
            TableTHElement th2 = tagged.CreateTableTHElement();
            th2.SetText("Header 2");
            headerRow.AppendChild(th2);

            // Body for outer table
            TableTBodyElement tbody = tagged.CreateTableTBodyElement();
            outerTable.AppendChild(tbody);
            TableTRElement bodyRow = tagged.CreateTableTRElement();
            tbody.AppendChild(bodyRow);

            // First cell with simple text
            TableTDElement td1 = tagged.CreateTableTDElement();
            td1.SetText("Cell 1");
            bodyRow.AppendChild(td1);

            // Second cell will host the nested table
            TableTDElement td2 = tagged.CreateTableTDElement();
            td2.SetText("Nested table:");
            bodyRow.AppendChild(td2);

            // Nested table inside the second cell
            TableElement nestedTable = tagged.CreateTableElement();
            nestedTable.AlternativeText = "Nested table";
            td2.AppendChild(nestedTable);

            // Body for nested table (no header)
            TableTBodyElement nestedBody = tagged.CreateTableTBodyElement();
            nestedTable.AppendChild(nestedBody);
            TableTRElement nestedRow = tagged.CreateTableTRElement();
            nestedBody.AppendChild(nestedRow);
            TableTDElement nestedTd1 = tagged.CreateTableTDElement();
            nestedTd1.SetText("Inner 1");
            nestedRow.AppendChild(nestedTd1);
            TableTDElement nestedTd2 = tagged.CreateTableTDElement();
            nestedTd2.SetText("Inner 2");
            nestedRow.AppendChild(nestedTd2);

            // Validate hierarchy by walking the structure tree
            Console.WriteLine("Tagging hierarchy:");
            WalkStructure(root, 0);

            // Save the tagged PDF – guard against missing libgdiplus on non‑Windows platforms
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
            }
            else
            {
                try
                {
                    doc.Save(outputPath);
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF saved without rendering-dependent features.");
                }
            }
        }

        Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
    }

    static void WalkStructure(StructureElement element, int depth)
    {
        string indent = new string(' ', depth * 2);
        Console.WriteLine($"{indent}{element.GetType().Name}: AltText='{element.AlternativeText}' Text='{element.ActualText}'");
        foreach (Element child in element.ChildElements)
        {
            if (child is StructureElement se)
                WalkStructure(se, depth + 1);
        }
    }

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
