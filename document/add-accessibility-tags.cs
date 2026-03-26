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
        const string outputPath = "tagged_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Enable automatic tagging (optional but recommended)
            AutoTaggingSettings.Default.EnableAutoTagging = true;

            // Set language and title for the tagged PDF
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Root element of the logical structure tree
            StructureElement root = tagged.RootElement;

            // ----- Heading (Level 1) -----
            HeaderElement heading = tagged.CreateHeaderElement(1);
            heading.SetText("Document Title");
            root.AppendChild(heading);

            // ----- Paragraph -----
            ParagraphElement paragraph = tagged.CreateParagraphElement();
            paragraph.SetText("This is an accessible paragraph that provides a description of the document content.");
            root.AppendChild(paragraph);

            // ----- Table -----
            TableElement table = tagged.CreateTableElement();
            table.AlternativeText = "Sample data table";
            root.AppendChild(table);

            // Table header (THead)
            TableTHeadElement thead = tagged.CreateTableTHeadElement();
            table.AppendChild(thead);
            TableTRElement headerRow = tagged.CreateTableTRElement();
            thead.AppendChild(headerRow);
            TableTHElement thName = tagged.CreateTableTHElement();
            thName.SetText("Name");
            headerRow.AppendChild(thName);
            TableTHElement thValue = tagged.CreateTableTHElement();
            thValue.SetText("Value");
            headerRow.AppendChild(thValue);

            // Table body (TBody)
            TableTBodyElement tbody = tagged.CreateTableTBodyElement();
            table.AppendChild(tbody);
            TableTRElement bodyRow = tagged.CreateTableTRElement();
            tbody.AppendChild(bodyRow);
            TableTDElement tdItem = tagged.CreateTableTDElement();
            tdItem.SetText("Item A");
            bodyRow.AppendChild(tdItem);
            TableTDElement tdNumber = tagged.CreateTableTDElement();
            tdNumber.SetText("42");
            bodyRow.AppendChild(tdNumber);

            // Save the tagged PDF
            doc.Save(outputPath);
        }

        Console.WriteLine("Tagged PDF saved to 'tagged_output.pdf'.");
    }
}