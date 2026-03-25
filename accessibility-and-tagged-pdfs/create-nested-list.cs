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

        using (Document doc = new Document())
        {
            // Set document-level tagging metadata
            doc.TaggedContent.SetLanguage("en-US");
            doc.TaggedContent.SetTitle("Nested List Example");

            ITaggedContent tagged = doc.TaggedContent;
            StructureElement root = tagged.RootElement;

            // Create an outer list (bullet list)
            ListElement outerList = tagged.CreateListElement();

            // First bullet item with a nested numbered list
            ListLIElement li1 = tagged.CreateListLIElement();
            ListLblElement lbl1 = tagged.CreateListLblElement();
            lbl1.ActualText = "Bullet item 1";
            ListLBodyElement body1 = tagged.CreateListLBodyElement();

            // Nested numbered list inside the first bullet item
            ListElement innerNumberedList = tagged.CreateListElement();
            for (int i = 1; i <= 2; i++)
            {
                ListLIElement innerLi = tagged.CreateListLIElement();
                ListLblElement innerLbl = tagged.CreateListLblElement();
                innerLbl.ActualText = $"Numbered subitem {i}";
                innerLi.AppendChild(innerLbl);
                innerNumberedList.AppendChild(innerLi);
            }
            body1.AppendChild(innerNumberedList);
            li1.AppendChild(lbl1);
            li1.AppendChild(body1);
            outerList.AppendChild(li1);

            // Second bullet item (no nested list)
            ListLIElement li2 = tagged.CreateListLIElement();
            ListLblElement lbl2 = tagged.CreateListLblElement();
            lbl2.ActualText = "Bullet item 2";
            li2.AppendChild(lbl2);
            outerList.AppendChild(li2);

            // Attach the list to the document root
            root.AppendChild(outerList);

            // Save the tagged PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to {Path.GetFullPath(outputPath)}");
    }
}