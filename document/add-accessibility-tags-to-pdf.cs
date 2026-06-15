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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Enable automatic tagging (optional but improves overall accessibility)
        AutoTaggingSettings.Default.EnableAutoTagging = true;

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content API
            ITaggedContent tagged = doc.TaggedContent;

            // Set language and title for the PDF
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Accessible Document");

            // Root element of the logical structure tree
            StructureElement root = tagged.RootElement;

            // ----- Heading (H1) -----
            HeaderElement heading = tagged.CreateHeaderElement(1);
            heading.SetText("Document Title");
            root.AppendChild(heading); // AppendChild with a single argument

            // ----- Paragraph -----
            ParagraphElement paragraph = tagged.CreateParagraphElement();
            paragraph.SetText("This is an example paragraph that provides introductory text for the document.");
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
            TableTHElement th1 = tagged.CreateTableTHElement();
            th1.SetText("Column A");
            headerRow.AppendChild(th1);
            TableTHElement th2 = tagged.CreateTableTHElement();
            th2.SetText("Column B");
            headerRow.AppendChild(th2);

            // Table body (TBody)
            TableTBodyElement tbody = tagged.CreateTableTBodyElement();
            table.AppendChild(tbody);
            TableTRElement bodyRow = tagged.CreateTableTRElement();
            tbody.AppendChild(bodyRow);
            TableTDElement td1 = tagged.CreateTableTDElement();
            td1.SetText("Row 1, Cell 1");
            bodyRow.AppendChild(td1);
            TableTDElement td2 = tagged.CreateTableTDElement();
            td2.SetText("Row 1, Cell 2");
            bodyRow.AppendChild(td2);

            // Save the modified PDF with accessibility tags
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
    }
}