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
        const string outputPath = "accessible_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Enable global auto‑tagging (optional but useful for heading detection)
        AutoTaggingSettings.Default.EnableAutoTagging = true;
        // AutoTaggingSettings.Default.HeadingRecognitionStrategy = HeadingRecognitionStrategy.Auto; // default

        using (Document doc = new Document(inputPath))
        {
            // Access the tagged‑content API
            ITaggedContent tagged = doc.TaggedContent;

            // Set document language and title (metadata for accessibility)
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Accessible PDF Document");

            // Root element of the logical structure tree
            StructureElement root = tagged.RootElement;

            // ----- Heading (H1) -----
            HeaderElement heading = tagged.CreateHeaderElement(1); // level 1 heading
            heading.SetText("Document Title");
            root.AppendChild(heading); // attach heading to the root

            // ----- Paragraph -----
            ParagraphElement paragraph = tagged.CreateParagraphElement();
            paragraph.SetText("This paragraph provides an introduction to the document. It is tagged as a regular paragraph for screen‑reader navigation.");
            root.AppendChild(paragraph);

            // ----- Table -----
            TableElement table = tagged.CreateTableElement();
            table.AlternativeText = "Sample data table showing example values.";
            root.AppendChild(table);

            // Table header (THead)
            TableTHeadElement thead = tagged.CreateTableTHeadElement();
            table.AppendChild(thead);

            TableTRElement headerRow = tagged.CreateTableTRElement();
            thead.AppendChild(headerRow);

            TableTHElement th1 = tagged.CreateTableTHElement();
            th1.SetText("Item");
            headerRow.AppendChild(th1);

            TableTHElement th2 = tagged.CreateTableTHElement();
            th2.SetText("Quantity");
            headerRow.AppendChild(th2);

            // Table body (TBody)
            TableTBodyElement tbody = tagged.CreateTableTBodyElement();
            table.AppendChild(tbody);

            TableTRElement dataRow = tagged.CreateTableTRElement();
            tbody.AppendChild(dataRow);

            TableTDElement td1 = tagged.CreateTableTDElement();
            td1.SetText("Apples");
            dataRow.AppendChild(td1);

            TableTDElement td2 = tagged.CreateTableTDElement();
            td2.SetText("42");
            dataRow.AppendChild(td2);

            // Save the modified PDF (no PreSave call needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Accessible PDF saved to '{outputPath}'.");
    }
}
