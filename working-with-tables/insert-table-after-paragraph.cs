using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Access tagged content (document must be tagged)
            ITaggedContent tagged = doc.TaggedContent;

            // Locate the paragraph after which the table will be inserted.
            ParagraphElement targetParagraph = null;
            var allParagraphs = tagged.RootElement.FindElements<ParagraphElement>(true);
            foreach (var para in allParagraphs)
            {
                if (!string.IsNullOrEmpty(para.ActualText) &&
                    para.ActualText.Contains("Insert table after this paragraph"))
                {
                    targetParagraph = para;
                    break;
                }
            }

            if (targetParagraph == null)
            {
                Console.WriteLine("Target paragraph not found. No changes made.");
                doc.Save(outputPath);
                return;
            }

            // Get the parent element of the located paragraph.
            StructureElement parent = targetParagraph.ParentElement as StructureElement;
            if (parent == null)
            {
                Console.WriteLine("Parent element not found. No changes made.");
                doc.Save(outputPath);
                return;
            }

            // Create a table element using the ITaggedContent factory.
            TableElement table = tagged.CreateTableElement();
            table.AlternativeText = "Sample data table";

            // Build table header.
            TableTHeadElement thead = tagged.CreateTableTHeadElement();
            table.AppendChild(thead);
            TableTRElement headerRow = tagged.CreateTableTRElement();
            thead.AppendChild(headerRow);
            TableTHElement th1 = tagged.CreateTableTHElement();
            th1.SetText("Column 1");
            headerRow.AppendChild(th1);
            TableTHElement th2 = tagged.CreateTableTHElement();
            th2.SetText("Column 2");
            headerRow.AppendChild(th2);

            // Build table body.
            TableTBodyElement tbody = tagged.CreateTableTBodyElement();
            table.AppendChild(tbody);
            TableTRElement bodyRow = tagged.CreateTableTRElement();
            tbody.AppendChild(bodyRow);
            TableTDElement td1 = tagged.CreateTableTDElement();
            td1.SetText("Value A");
            bodyRow.AppendChild(td1);
            TableTDElement td2 = tagged.CreateTableTDElement();
            td2.SetText("Value B");
            bodyRow.AppendChild(td2);

            // Insert the table after the located paragraph.
            int paragraphIndex = -1;
            for (int i = 0; i < parent.ChildElements.Count; i++)
            {
                if (parent.ChildElements[i] == targetParagraph)
                {
                    paragraphIndex = i;
                    break;
                }
            }

            if (paragraphIndex >= 0 && paragraphIndex < parent.ChildElements.Count - 1)
            {
                // Insert directly after the paragraph. Note: InsertChild expects (Element element, int index).
                parent.InsertChild(table, paragraphIndex + 1);
            }
            else
            {
                // Paragraph is the last child – just append.
                parent.AppendChild(table);
            }

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table inserted and saved to '{outputPath}'.");
    }
}
