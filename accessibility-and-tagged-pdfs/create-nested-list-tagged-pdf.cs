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
        const string validationReport = "validation_report.xml";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Access tagged content API
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Nested List Example");

            // Root element of the logical structure
            StructureElement root = tagged.RootElement;

            // -------------------------------------------------
            // Create a top‑level bullet list (L)
            // -------------------------------------------------
            ListElement bulletList = tagged.CreateListElement();
            root.AppendChild(bulletList); // Append list to the root

            // First bullet item
            ListLIElement bulletItem1 = tagged.CreateListLIElement();
            bulletList.AppendChild(bulletItem1, true);

            // Label for the bullet (optional – the list attribute defines the bullet style)
            ListLblElement bulletLbl1 = tagged.CreateListLblElement();
            bulletLbl1.ActualText = ""; // empty – bullet rendered by list attribute
            bulletItem1.AppendChild(bulletLbl1, true);

            // Body of the bullet item
            ListLBodyElement bulletBody1 = tagged.CreateListLBodyElement();
            bulletBody1.ActualText = "Bullet item 1";
            bulletItem1.AppendChild(bulletBody1, true);

            // -------------------------------------------------
            // Nested numbered list inside the first bullet item
            // -------------------------------------------------
            ListElement numberedList = tagged.CreateListElement();
            bulletBody1.AppendChild(numberedList, true); // attach sub‑list to the body

            // First numbered item
            ListLIElement numItem1 = tagged.CreateListLIElement();
            numberedList.AppendChild(numItem1, true);

            ListLblElement numLbl1 = tagged.CreateListLblElement();
            numLbl1.ActualText = "1."; // explicit number label
            numItem1.AppendChild(numLbl1, true);

            ListLBodyElement numBody1 = tagged.CreateListLBodyElement();
            numBody1.ActualText = "Numbered sub‑item A";
            numItem1.AppendChild(numBody1, true);

            // Second numbered item
            ListLIElement numItem2 = tagged.CreateListLIElement();
            numberedList.AppendChild(numItem2, true);

            ListLblElement numLbl2 = tagged.CreateListLblElement();
            numLbl2.ActualText = "2."; // explicit number label
            numItem2.AppendChild(numLbl2, true);

            ListLBodyElement numBody2 = tagged.CreateListLBodyElement();
            numBody2.ActualText = "Numbered sub‑item B";
            numItem2.AppendChild(numBody2, true);

            // -------------------------------------------------
            // Second bullet item (same level as the first)
            // -------------------------------------------------
            ListLIElement bulletItem2 = tagged.CreateListLIElement();
            bulletList.AppendChild(bulletItem2, true);

            ListLblElement bulletLbl2 = tagged.CreateListLblElement();
            bulletLbl2.ActualText = "";
            bulletItem2.AppendChild(bulletLbl2, true);

            ListLBodyElement bulletBody2 = tagged.CreateListLBodyElement();
            bulletBody2.ActualText = "Bullet item 2";
            bulletItem2.AppendChild(bulletBody2, true);

            // -------------------------------------------------
            // Save the document
            // -------------------------------------------------
            doc.Save(outputPath);

            // -------------------------------------------------
            // Validate the tagged PDF against PDF/UA (PDF_UA_1)
            // -------------------------------------------------
            doc.Validate(validationReport, PdfFormat.PDF_UA_1);
        }

        Console.WriteLine($"PDF with nested list saved to '{outputPath}'.");
        Console.WriteLine($"Validation report written to '{validationReport}'.");
    }
}