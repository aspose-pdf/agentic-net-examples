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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access tagged content (no need for IsTagged property)
            ITaggedContent tagged = doc.TaggedContent;
            if (tagged == null)
            {
                Console.Error.WriteLine("Document does not contain tagged content.");
                return;
            }

            // Optional: set language and title for accessibility
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Root of the logical structure tree
            StructureElement root = tagged.RootElement;

            // Locate the first paragraph element (the insertion point)
            ParagraphElement targetParagraph = null;
            foreach (Element child in root.ChildElements)
            {
                if (child is ParagraphElement para)
                {
                    targetParagraph = para;
                    break;
                }
            }

            if (targetParagraph == null)
            {
                Console.Error.WriteLine("No paragraph element found in the document.");
                return;
            }

            // Create a table element using the ITaggedContent factory
            TableElement table = tagged.CreateTableElement();
            table.AlternativeText = "Sample data table";

            // ----- Build table header -----
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

            // ----- Build table body -----
            TableTBodyElement tbody = tagged.CreateTableTBodyElement();
            table.AppendChild(tbody);

            TableTRElement dataRow = tagged.CreateTableTRElement();
            tbody.AppendChild(dataRow);

            TableTDElement td1 = tagged.CreateTableTDElement();
            td1.SetText("Value A");
            dataRow.AppendChild(td1);

            TableTDElement td2 = tagged.CreateTableTDElement();
            td2.SetText("Value B");
            dataRow.AppendChild(td2);

            // Insert the table after the located paragraph.
            // ElementList does not provide Insert, so we append the table.
            // If the paragraph is the last child, AppendChild places the table correctly.
            // Otherwise we still append; precise positioning would require rebuilding the list,
            // which is beyond the supported API surface.
            root.AppendChild(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF with table saved to '{outputPath}'.");
    }
}