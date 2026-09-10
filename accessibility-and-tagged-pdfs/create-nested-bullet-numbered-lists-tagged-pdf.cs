using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "nested_list.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page (required for visible content)
            Page page = doc.Pages.Add();

            // Add some visible text so the PDF is not empty
            TextFragment title = new TextFragment("Nested List Example");
            title.TextState.FontSize = 16;
            title.TextState.Font = FontRepository.FindFont("Helvetica");
            title.Position = new Position(50, 800);
            page.Paragraphs.Add(title);

            // Access the tagged content API
            ITaggedContent tagged = doc.TaggedContent;

            // Set language and title for the tagged PDF
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Nested List PDF");

            // Get the root element of the logical structure
            StructureElement root = tagged.RootElement;

            // -------------------------------------------------
            // Create a bullet list (disc bullet) with one item
            // -------------------------------------------------
            ListElement bulletList = tagged.CreateListElement();
            // Optional: set bullet style via attribute (disc bullet)
            // The attribute API may vary; this demonstrates intent
            // bulletList.Attributes.CreateAttributes(AttributeOwnerStandard.List)
            //          .Add(AttributeKey.ListNumbering, AttributeName.ListNumbering_Disc);
            root.AppendChild(bulletList); // attach list to root

            // First list item
            ListLIElement bulletItem = tagged.CreateListLIElement();
            bulletList.AppendChild(bulletItem);

            // List body for the first item
            ListLBodyElement bulletBody = tagged.CreateListLBodyElement();
            bulletItem.AppendChild(bulletBody);

            // Add visible text for the bullet item
            ParagraphElement bulletPara = tagged.CreateParagraphElement();
            bulletPara.SetText("Bullet list item");
            bulletBody.AppendChild(bulletPara);

            // -------------------------------------------------
            // Create a numbered list with a nested bullet sub‑list
            // -------------------------------------------------
            ListElement numberedList = tagged.CreateListElement();
            // No ListNumbering attribute means default auto‑numbering
            root.AppendChild(numberedList);

            // First numbered item
            ListLIElement numberItem1 = tagged.CreateListLIElement();
            numberedList.AppendChild(numberItem1);

            ListLBodyElement numberBody1 = tagged.CreateListLBodyElement();
            numberItem1.AppendChild(numberBody1);

            ParagraphElement numberPara1 = tagged.CreateParagraphElement();
            numberPara1.SetText("Numbered list item 1");
            numberBody1.AppendChild(numberPara1);

            // Second numbered item that contains a nested bullet list
            ListLIElement numberItem2 = tagged.CreateListLIElement();
            numberedList.AppendChild(numberItem2);

            ListLBodyElement numberBody2 = tagged.CreateListLBodyElement();
            numberItem2.AppendChild(numberBody2);

            ParagraphElement numberPara2 = tagged.CreateParagraphElement();
            numberPara2.SetText("Numbered list item 2 (with sub‑list)");
            numberBody2.AppendChild(numberPara2);

            // Nested bullet sub‑list inside the second numbered item
            ListElement subBulletList = tagged.CreateListElement();
            // Optional: set bullet style for sub‑list
            // subBulletList.Attributes.CreateAttributes(AttributeOwnerStandard.List)
            //               .Add(AttributeKey.ListNumbering, AttributeName.ListNumbering_Square);
            numberBody2.AppendChild(subBulletList);

            // Sub‑list item
            ListLIElement subBulletItem = tagged.CreateListLIElement();
            subBulletList.AppendChild(subBulletItem);

            ListLBodyElement subBulletBody = tagged.CreateListLBodyElement();
            subBulletItem.AppendChild(subBulletBody);

            ParagraphElement subBulletPara = tagged.CreateParagraphElement();
            subBulletPara.SetText("Nested bullet item");
            subBulletBody.AppendChild(subBulletPara);

            // -------------------------------------------------
            // Validation: ensure the root contains the two top‑level lists
            // -------------------------------------------------
            Console.WriteLine($"Root has {root.ChildElements.Count} top‑level elements.");
            foreach (Element child in root.ChildElements)
            {
                Console.WriteLine($"- Child type: {child.GetType().Name}");
            }

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with nested list saved to '{outputPath}'.");
    }
}