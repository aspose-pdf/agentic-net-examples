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
        const string outputPath = "nested_table_tagged.pdf";

        // Ensure a source PDF exists; create a blank one if necessary
        if (!File.Exists(inputPath))
        {
            using (Document empty = new Document())
            {
                empty.Pages.Add();
                empty.Save(inputPath);
            }
        }

        // Load the document (lifecycle rule)
        using (Document doc = new Document(inputPath))
        {
            // Access tagged content (tagged-content-access-via-doc-taggedcontent)
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Nested Table Example");

            // Root element of the logical structure
            StructureElement root = tagged.RootElement;

            // Paragraph that will contain the outer table
            ParagraphElement paragraph = tagged.CreateParagraphElement();
            paragraph.SetText("Paragraph containing a nested table.");
            root.AppendChild(paragraph); // AppendChild(element) – correct usage

            // ----- Outer table -----
            TableElement outerTable = tagged.CreateTableElement();
            outerTable.AlternativeText = "Outer table";
            paragraph.AppendChild(outerTable); // Table inside paragraph

            TableTBodyElement outerBody = outerTable.CreateTBody(); // CreateTBody() auto‑adds to table
            TableTRElement outerRow = outerBody.CreateTR();        // CreateTR() auto‑adds to tbody

            TableTDElement outerCell = tagged.CreateTableTDElement();
            outerCell.SetText("Cell with inner table");
            outerRow.AppendChild(outerCell);

            // ----- Inner table (nested inside the outer cell) -----
            TableElement innerTable = tagged.CreateTableElement();
            innerTable.AlternativeText = "Inner table";
            outerCell.AppendChild(innerTable); // Nested table as child of the outer cell

            TableTBodyElement innerBody = innerTable.CreateTBody();
            TableTRElement innerRow = innerBody.CreateTR();

            TableTDElement innerCell = tagged.CreateTableTDElement();
            innerCell.SetText("Inner cell content");
            innerRow.AppendChild(innerCell);

            // Validate hierarchy by walking the structure tree
            Console.WriteLine("Tagging hierarchy:");
            WalkStructure(root, 0);

            // Save the modified document – guard against missing GDI+ on non‑Windows platforms
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
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF was not saved.");
                }
            }
        }

        Console.WriteLine($"Finished execution. Output path: '{outputPath}'.");
    }

    // Recursive walk that prints element type and key properties
    static void WalkStructure(StructureElement element, int depth)
    {
        string indent = new string(' ', depth * 2);
        string alt = element.AlternativeText ?? "";
        string txt = "";

        if (element is ParagraphElement pe)
            txt = pe.ActualText ?? "";
        else if (element is TableTDElement td)
            txt = td.ActualText ?? "";

        Console.WriteLine($"{indent}{element.GetType().Name}: AltText='{alt}' Text='{txt}'");

        foreach (Element child in element.ChildElements)
        {
            if (child is StructureElement se)
                WalkStructure(se, depth + 1);
        }
    }

    // Helper to detect a missing native GDI+ library in the exception chain
    static bool ContainsDllNotFound(Exception? ex)
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
