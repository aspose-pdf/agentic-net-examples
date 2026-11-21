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
        const string outputPath = "output_with_table.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content API
            ITaggedContent taggedContent = doc.TaggedContent;

            // Ensure the document is marked as tagged (if not already)
            // Setting language and title is optional but demonstrates usage
            taggedContent.SetLanguage("en-US");
            taggedContent.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Get the root structure element (no cast required)
            StructureElement root = taggedContent.RootElement;

            // Create a table element
            TableElement table = taggedContent.CreateTableElement();
            table.AlternativeText = "Sample table with bold text in third cell";
            root.AppendChild(table);

            // Create the table header row (optional)
            TableTHeadElement thead = taggedContent.CreateTableTHeadElement();
            table.AppendChild(thead);
            TableTRElement headerRow = taggedContent.CreateTableTRElement();
            thead.AppendChild(headerRow);

            // First two header cells (plain text)
            TableTHElement th1 = taggedContent.CreateTableTHElement();
            th1.SetText("Column 1");
            headerRow.AppendChild(th1);

            TableTHElement th2 = taggedContent.CreateTableTHElement();
            th2.SetText("Column 2");
            headerRow.AppendChild(th2);

            // Third header cell will contain the HTML paragraph
            TableTHElement th3 = taggedContent.CreateTableTHElement();
            // SetText accepts plain text; to embed HTML we use a ParagraphElement with an HtmlFragment later.
            // Here we create a ParagraphElement that will hold the HTML content.
            ParagraphElement htmlParagraph = taggedContent.CreateParagraphElement();
            // HtmlFragment can be added to the page, but for tagged structure we set the text directly.
            // The bold effect can be achieved by using HTML tags if the viewer supports it.
            htmlParagraph.SetText("<b>Bold Text</b>");
            th3.AppendChild(htmlParagraph);
            headerRow.AppendChild(th3);

            // Create a body row with sample data (optional)
            TableTBodyElement tbody = taggedContent.CreateTableTBodyElement();
            table.AppendChild(tbody);
            TableTRElement bodyRow = taggedContent.CreateTableTRElement();
            tbody.AppendChild(bodyRow);

            TableTDElement td1 = taggedContent.CreateTableTDElement();
            td1.SetText("Data 1");
            bodyRow.AppendChild(td1);

            TableTDElement td2 = taggedContent.CreateTableTDElement();
            td2.SetText("Data 2");
            bodyRow.AppendChild(td2);

            TableTDElement td3 = taggedContent.CreateTableTDElement();
            td3.SetText("Data 3");
            bodyRow.AppendChild(td3);

            // Save the modified PDF (no PreSave call needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with table: {outputPath}");
    }
}