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
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Access tagged content (creates one if missing)
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle("PDF with Hyperlink Table");

            // Root of the logical structure tree
            StructureElement root = tagged.RootElement;

            // Create a table element
            TableElement table = tagged.CreateTableElement();
            table.AlternativeText = "Sample data table with hyperlink";
            root.AppendChild(table);

            // ----- Header row -----
            TableTHeadElement thead = tagged.CreateTableTHeadElement();
            table.AppendChild(thead);
            TableTRElement headerRow = tagged.CreateTableTRElement();
            thead.AppendChild(headerRow);

            TableTHElement th1 = tagged.CreateTableTHElement();
            th1.SetText("Item");
            headerRow.AppendChild(th1);

            TableTHElement th2 = tagged.CreateTableTHElement();
            th2.SetText("Link");
            headerRow.AppendChild(th2);

            // ----- Body row -----
            TableTBodyElement tbody = tagged.CreateTableTBodyElement();
            table.AppendChild(tbody);
            TableTRElement dataRow = tagged.CreateTableTRElement();
            tbody.AppendChild(dataRow);

            // First cell: plain text
            TableTDElement td1 = tagged.CreateTableTDElement();
            td1.SetText("Example");
            dataRow.AppendChild(td1);

            // Second cell: text that will act as a hyperlink
            TableTDElement td2 = tagged.CreateTableTDElement();
            // Create a paragraph element for the cell content
            ParagraphElement para = tagged.CreateParagraphElement();
            para.SetText("Click here");
            td2.AppendChild(para);
            dataRow.AppendChild(td2);

            // -------------------------------------------------
            // Add a LinkAnnotation that covers the second cell.
            // For demonstration we use approximate coordinates.
            // In a real scenario you would calculate the exact
            // rectangle of the cell (e.g., via layout analysis).
            // -------------------------------------------------
            // Assume the table is placed on the first page.
            Page page = doc.Pages[1];

            // Approximate rectangle covering the second cell.
            // Values are in points (1/72 inch). Adjust as needed.
            Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(
                llx: 300, // lower‑left X
                lly: 500, // lower‑left Y
                urx: 450, // upper‑right X
                ury: 520  // upper‑right Y
            );

            // Create the link annotation with a URI action.
            LinkAnnotation link = new LinkAnnotation(page, linkRect);
            link.Action = new GoToURIAction("https://example.com");
            // Set an invisible border for the annotation.
            link.Border = new Border(link) { Width = 0 };
            page.Annotations.Add(link);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Hyperlinked table saved to '{outputPath}'.");
    }
}
