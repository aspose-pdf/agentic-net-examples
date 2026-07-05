using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string outputPath = "nested_list.pdf";

        // Create a new PDF document and add a blank page
        using (Document doc = new Document())
        {
            // Add a page (required for visual content)
            Page page = doc.Pages.Add();

            // Access the tagged content API
            ITaggedContent tagged = doc.TaggedContent;

            // Set document language and title (optional)
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Nested List Example");

            // Get the root element of the logical structure
            StructureElement root = tagged.RootElement;

            // -------------------------------------------------
            // Create an unordered (bullet) list
            // -------------------------------------------------
            ListElement bulletList = tagged.CreateListElement();

            // First bullet item
            ListLIElement bulletItem1 = tagged.CreateListLIElement();
            ListLBodyElement bulletBody1 = tagged.CreateListLBodyElement();
            ParagraphElement bulletPara1 = tagged.CreateParagraphElement();
            bulletPara1.SetText("Bullet item 1");
            bulletBody1.AppendChild(bulletPara1, true);
            bulletItem1.AppendChild(bulletBody1, true);
            bulletList.AppendChild(bulletItem1, true);

            // Second bullet item
            ListLIElement bulletItem2 = tagged.CreateListLIElement();
            ListLBodyElement bulletBody2 = tagged.CreateListLBodyElement();
            ParagraphElement bulletPara2 = tagged.CreateParagraphElement();
            bulletPara2.SetText("Bullet item 2");
            bulletBody2.AppendChild(bulletPara2, true);
            bulletItem2.AppendChild(bulletBody2, true);
            bulletList.AppendChild(bulletItem2, true);

            // Append the bullet list to the root
            root.AppendChild(bulletList, true);

            // -------------------------------------------------
            // Create an ordered (numbered) list
            // -------------------------------------------------
            ListElement numberedList = tagged.CreateListElement();

            // First numbered item
            ListLIElement numberItem1 = tagged.CreateListLIElement();
            ListLBodyElement numberBody1 = tagged.CreateListLBodyElement();
            ParagraphElement numberPara1 = tagged.CreateParagraphElement();
            numberPara1.SetText("Numbered item 1");
            numberBody1.AppendChild(numberPara1, true);
            numberItem1.AppendChild(numberBody1, true);
            numberedList.AppendChild(numberItem1, true);

            // Second numbered item
            ListLIElement numberItem2 = tagged.CreateListLIElement();
            ListLBodyElement numberBody2 = tagged.CreateListLBodyElement();
            ParagraphElement numberPara2 = tagged.CreateParagraphElement();
            numberPara2.SetText("Numbered item 2");
            numberBody2.AppendChild(numberPara2, true);
            numberItem2.AppendChild(numberBody2, true);
            numberedList.AppendChild(numberItem2, true);

            // Append the numbered list to the root
            root.AppendChild(numberedList, true);

            // Save the PDF (no PreSave needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with nested lists saved to '{outputPath}'.");
    }
}