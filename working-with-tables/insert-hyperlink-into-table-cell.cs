using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";      // existing PDF or can be a blank PDF
        const string outputPath = "output_with_link.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Open the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Access tagged content (creates a tagged structure if not present)
                ITaggedContent taggedContent = doc.TaggedContent;
                taggedContent.SetLanguage("en-US");
                taggedContent.SetTitle("PDF with Hyperlink in Table Cell");

                // Root element of the structure tree
                StructureElement root = taggedContent.RootElement;

                // Create a table element and attach it to the root
                TableElement table = taggedContent.CreateTableElement();
                table.AlternativeText = "Sample data table with hyperlink";
                root.AppendChild(table);

                // ----- Table Header -----
                TableTHeadElement thead = taggedContent.CreateTableTHeadElement();
                table.AppendChild(thead);

                TableTRElement headerRow = taggedContent.CreateTableTRElement();
                thead.AppendChild(headerRow);

                TableTHElement th1 = taggedContent.CreateTableTHElement();
                th1.SetText("Item");
                headerRow.AppendChild(th1);

                TableTHElement th2 = taggedContent.CreateTableTHElement();
                th2.SetText("Link");
                headerRow.AppendChild(th2);

                // ----- Table Body -----
                TableTBodyElement tbody = taggedContent.CreateTableTBodyElement();
                table.AppendChild(tbody);

                TableTRElement dataRow = taggedContent.CreateTableTRElement();
                tbody.AppendChild(dataRow);

                // First cell: plain text
                TableTDElement tdItem = taggedContent.CreateTableTDElement();
                tdItem.SetText("Example");
                dataRow.AppendChild(tdItem);

                // Second cell: text that will be hyperlinked
                TableTDElement tdLink = taggedContent.CreateTableTDElement();
                // Create a paragraph element for the cell content
                ParagraphElement para = taggedContent.CreateParagraphElement();
                para.SetText("Visit Example.com");
                tdLink.AppendChild(para);
                dataRow.AppendChild(tdLink);

                // Add a LinkAnnotation that covers the cell area.
                // For demonstration we use static coordinates; in a real scenario
                // you would calculate the rectangle based on the cell position.
                Page firstPage = doc.Pages[1];
                Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(100, 500, 250, 520);
                LinkAnnotation link = new LinkAnnotation(firstPage, linkRect)
                {
                    Action = new GoToURIAction("https://www.example.com")
                };
                firstPage.Annotations.Add(link);

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF saved with hyperlink: '{outputPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
