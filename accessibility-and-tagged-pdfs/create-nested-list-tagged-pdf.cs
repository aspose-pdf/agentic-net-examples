using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // any existing PDF to start from
        const string outputPdf = "tagged_list.pdf";

        // Ensure the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdf}");
            return;
        }

        // Open the document and obtain the tagged‑content interface
        using (Document doc = new Document(inputPdf))
        {
            ITaggedContent tagged = doc.TaggedContent;

            // Set language and title for the tagged PDF
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Nested List Example");

            // Root element of the logical structure tree
            StructureElement root = tagged.RootElement;

            // -------------------------------------------------
            // Create a top‑level bullet list (disc style)
            // -------------------------------------------------
            ListElement bulletList = tagged.CreateListElement();
            bulletList.SetTag("L");                     // assign list tag
            // (optional) set a custom attribute for bullet style
            // bulletList.Attributes[AttributeKey.ListNumbering] = AttributeName.ListNumbering_Disc;

            // First bullet item
            ListLIElement bulletItem1 = tagged.CreateListLIElement();
            bulletItem1.SetTag("LI");
            ListLBodyElement bulletBody1 = tagged.CreateListLBodyElement();
            // Add visible text to the list body
            ParagraphElement para1 = tagged.CreateParagraphElement();
            para1.SetText("First bullet item");
            bulletBody1.AppendChild(para1);
            bulletItem1.AppendChild(bulletBody1);
            bulletList.AppendChild(bulletItem1);

            // Second bullet item with a nested numbered list
            ListLIElement bulletItem2 = tagged.CreateListLIElement();
            bulletItem2.SetTag("LI");
            ListLBodyElement bulletBody2 = tagged.CreateListLBodyElement();

            // Text for the second bullet item
            ParagraphElement para2 = tagged.CreateParagraphElement();
            para2.SetText("Second bullet item with sublist");
            bulletBody2.AppendChild(para2);

            // -------------------------------------------------
            // Create a nested numbered list (no auto‑numbering)
            // -------------------------------------------------
            ListElement numberedList = tagged.CreateListElement();
            numberedList.SetTag("L");
            // numberedList.Attributes[AttributeKey.ListNumbering] = AttributeName.ListNumbering_None;

            // First numbered sub‑item
            ListLIElement numItem1 = tagged.CreateListLIElement();
            numItem1.SetTag("LI");
            ListLBodyElement numBody1 = tagged.CreateListLBodyElement();
            ParagraphElement numPara1 = tagged.CreateParagraphElement();
            numPara1.SetText("First sub‑item");
            numBody1.AppendChild(numPara1);
            numItem1.AppendChild(numBody1);
            numberedList.AppendChild(numItem1);

            // Second numbered sub‑item
            ListLIElement numItem2 = tagged.CreateListLIElement();
            numItem2.SetTag("LI");
            ListLBodyElement numBody2 = tagged.CreateListLBodyElement();
            ParagraphElement numPara2 = tagged.CreateParagraphElement();
            numPara2.SetText("Second sub‑item");
            numBody2.AppendChild(numPara2);
            numItem2.AppendChild(numBody2);
            numberedList.AppendChild(numItem2);

            // Attach the nested numbered list to the second bullet item's body
            bulletBody2.AppendChild(numberedList);
            bulletItem2.AppendChild(bulletBody2);
            bulletList.AppendChild(bulletItem2);

            // -------------------------------------------------
            // Attach the top‑level list to the document root
            // -------------------------------------------------
            root.AppendChild(bulletList);

            // Simple validation: ensure the root now contains at least one child element
            if (root.ChildElements.Count > 0)
                Console.WriteLine("Structure tree successfully updated with nested list.");
            else
                Console.WriteLine("Failed to add list to the structure tree.");

            // Save the modified PDF (no PreSave call required)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Tagged PDF with nested list saved to '{outputPdf}'.");
    }
}