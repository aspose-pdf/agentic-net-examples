using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string outputPath = "nested_list_tagged.pdf";

        using (Document doc = new Document())
        {
            // A page is required for the structure tree
            Page page = doc.Pages.Add();

            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Nested List Example");

            // Root of the logical structure tree
            StructureElement root = tagged.RootElement;

            // ----- Top‑level bullet list -----
            ListElement bulletList = tagged.CreateListElement();
            // Optional: specify bullet list type
            // bulletList.ListType = ListType.Bullet;
            root.AppendChild(bulletList);

            // First bullet item
            ListLIElement li1 = tagged.CreateListLIElement();
            bulletList.AppendChild(li1);
            ListLBodyElement lbody1 = tagged.CreateListLBodyElement();
            li1.AppendChild(lbody1);
            ParagraphElement p1 = tagged.CreateParagraphElement();
            p1.SetText("Bullet item 1");
            lbody1.AppendChild(p1);

            // Second bullet item that will contain a numbered sub‑list
            ListLIElement li2 = tagged.CreateListLIElement();
            bulletList.AppendChild(li2);
            ListLBodyElement lbody2 = tagged.CreateListLBodyElement();
            li2.AppendChild(lbody2);
            ParagraphElement p2 = tagged.CreateParagraphElement();
            p2.SetText("Bullet item with sub‑list");
            lbody2.AppendChild(p2);

            // ----- Numbered sub‑list -----
            ListElement numberedList = tagged.CreateListElement();
            // Optional: specify numbered list type (numbers are added manually in the text)
            // numberedList.ListType = ListType.Number;
            // Append the sub‑list to the LBody of the parent LI, not directly to the LI
            lbody2.AppendChild(numberedList);

            // Sub‑list item 1
            ListLIElement subLi1 = tagged.CreateListLIElement();
            numberedList.AppendChild(subLi1);
            ListLBodyElement subLBody1 = tagged.CreateListLBodyElement();
            subLi1.AppendChild(subLBody1);
            ParagraphElement subP1 = tagged.CreateParagraphElement();
            subP1.SetText("1. First sub‑item");
            subLBody1.AppendChild(subP1);

            // Sub‑list item 2
            ListLIElement subLi2 = tagged.CreateListLIElement();
            numberedList.AppendChild(subLi2);
            ListLBodyElement subLBody2 = tagged.CreateListLBodyElement();
            subLi2.AppendChild(subLBody2);
            ParagraphElement subP2 = tagged.CreateParagraphElement();
            subP2.SetText("2. Second sub‑item");
            subLBody2.AppendChild(subP2);

            // ----- Save the tagged PDF (guarded for platforms without GDI+) -----
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"Tagged PDF with nested list saved to '{outputPath}'.");
            }
            else
            {
                try
                {
                    doc.Save(outputPath);
                    Console.WriteLine($"Tagged PDF with nested list saved to '{outputPath}'. (saved on non‑Windows platform)");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF was not saved.");
                }
            }
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
