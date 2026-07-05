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

        // Enable auto‑tagging and use automatic heading detection
        AutoTaggingSettings.Default.EnableAutoTagging = true;
        AutoTaggingSettings.Default.HeadingRecognitionStrategy = HeadingRecognitionStrategy.Auto;

        using (Document doc = new Document(inputPath))
        {
            // Access the tagged‑content API
            ITaggedContent tagged = doc.TaggedContent;

            // Set document language and title (accessible metadata)
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Accessible Document");

            // Root element of the logical structure tree
            StructureElement root = tagged.RootElement;

            // ----- Heading (level 1) -----
            HeaderElement h1 = tagged.CreateHeaderElement(1);
            h1.SetText("Document Title");
            root.AppendChild(h1);

            // ----- Paragraph -----
            ParagraphElement para = tagged.CreateParagraphElement();
            para.SetText("This is the first paragraph of the document. It provides an overview of the content.");
            root.AppendChild(para);

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
            td1.SetText("Row 1, Cell A");
            bodyRow.AppendChild(td1);
            TableTDElement td2 = tagged.CreateTableTDElement();
            td2.SetText("Row 1, Cell B");
            bodyRow.AppendChild(td2);

            // Save the PDF with the new accessibility tags
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
    }
}