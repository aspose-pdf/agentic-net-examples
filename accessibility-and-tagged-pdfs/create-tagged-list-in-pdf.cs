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
        const string outputPath = "tagged_list_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged‑content API
            ITaggedContent taggedContent = doc.TaggedContent;

            // Set document language and title (write‑only setters)
            taggedContent.SetLanguage("en-US");
            taggedContent.SetTitle("PDF with List Structure");

            // Root element of the logical structure tree
            StructureElement root = taggedContent.RootElement;

            // Create a List element and attach it to the root
            ListElement list = taggedContent.CreateListElement();
            list.AlternativeText = "Sample bullet list";
            root.AppendChild(list); // AppendChild with one argument

            // ---------- First list item ----------
            // Create the LI (list item) element
            ListLIElement li1 = taggedContent.CreateListLIElement();
            list.AppendChild(li1);

            // Create the label part of the list item
            ListLblElement lbl1 = taggedContent.CreateListLblElement();
            li1.AppendChild(lbl1);

            // Add a paragraph inside the label to hold the visible text
            ParagraphElement lblPara1 = taggedContent.CreateParagraphElement();
            lblPara1.SetText("First item");
            lbl1.AppendChild(lblPara1);

            // Create the body part of the list item
            ListLBodyElement body1 = taggedContent.CreateListLBodyElement();
            li1.AppendChild(body1);

            // Add a paragraph inside the body for the item description
            ParagraphElement bodyPara1 = taggedContent.CreateParagraphElement();
            bodyPara1.SetText("Description for the first item.");
            body1.AppendChild(bodyPara1);

            // ---------- Second list item ----------
            ListLIElement li2 = taggedContent.CreateListLIElement();
            list.AppendChild(li2);

            ListLblElement lbl2 = taggedContent.CreateListLblElement();
            li2.AppendChild(lbl2);

            ParagraphElement lblPara2 = taggedContent.CreateParagraphElement();
            lblPara2.SetText("Second item");
            lbl2.AppendChild(lblPara2);

            ListLBodyElement body2 = taggedContent.CreateListLBodyElement();
            li2.AppendChild(body2);

            ParagraphElement bodyPara2 = taggedContent.CreateParagraphElement();
            bodyPara2.SetText("Description for the second item.");
            body2.AppendChild(bodyPara2);

            // Save the modified PDF (no PreSave needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF with list saved to '{outputPath}'.");
    }
}