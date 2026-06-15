using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // existing PDF to tag
        const string outputPath = "tagged_list.pdf";
        const string reportPath = "validation_report.xml";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF (core API) and work with its tagged content
        using (Document doc = new Document(inputPath))
        {
            // Access tagged content (no Facades)
            ITaggedContent tagged = doc.TaggedContent;

            // Set document language and title (write‑only setters)
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Root element of the logical structure tree
            StructureElement root = tagged.RootElement;

            // -------------------------------------------------
            // Create a bullet list (disc bullet)
            // -------------------------------------------------
            ListElement bulletList = tagged.CreateListElement();
            root.AppendChild(bulletList); // attach to root

            // First bullet item
            ListLIElement bulletItem1 = tagged.CreateListLIElement();
            ListLBodyElement bulletBody1 = tagged.CreateListLBodyElement();
            ParagraphElement para1 = tagged.CreateParagraphElement();
            para1.SetText("First bullet point");
            bulletBody1.AppendChild(para1);
            bulletItem1.AppendChild(bulletBody1);
            bulletList.AppendChild(bulletItem1);

            // Second bullet item
            ListLIElement bulletItem2 = tagged.CreateListLIElement();
            ListLBodyElement bulletBody2 = tagged.CreateListLBodyElement();
            ParagraphElement para2 = tagged.CreateParagraphElement();
            para2.SetText("Second bullet point");
            bulletBody2.AppendChild(para2);
            bulletItem2.AppendChild(bulletBody2);
            bulletList.AppendChild(bulletItem2);

            // -------------------------------------------------
            // Create a numbered list
            // -------------------------------------------------
            ListElement numberedList = tagged.CreateListElement();
            root.AppendChild(numberedList); // attach to root

            // First numbered item
            ListLIElement numItem1 = tagged.CreateListLIElement();
            ListLBodyElement numBody1 = tagged.CreateListLBodyElement();
            ParagraphElement para3 = tagged.CreateParagraphElement();
            para3.SetText("First numbered item");
            numBody1.AppendChild(para3);
            numItem1.AppendChild(numBody1);
            numberedList.AppendChild(numItem1);

            // Second numbered item
            ListLIElement numItem2 = tagged.CreateListLIElement();
            ListLBodyElement numBody2 = tagged.CreateListLBodyElement();
            ParagraphElement para4 = tagged.CreateParagraphElement();
            para4.SetText("Second numbered item");
            numBody2.AppendChild(para4);
            numItem2.AppendChild(numBody2);
            numberedList.AppendChild(numItem2);

            // -------------------------------------------------
            // Validate the tagged PDF (PDF/UA compliance)
            // -------------------------------------------------
            // Use the correct PDF/UA constant
            doc.Validate(reportPath, PdfFormat.PDF_UA_1);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF with lists saved to '{outputPath}'.");
        Console.WriteLine($"Validation report saved to '{reportPath}'.");
    }
}
