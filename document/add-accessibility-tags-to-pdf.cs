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
        const string outputPath = "tagged_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Enable global auto‑tagging (optional but helpful)
        AutoTaggingSettings.Default.EnableAutoTagging = true;

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Access tagged‑content API
            ITaggedContent tagged = doc.TaggedContent;

            // Set language and title for accessibility metadata
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Root element of the logical structure tree
            StructureElement root = tagged.RootElement;

            // Optional: create a section to group the new content
            SectElement sect = tagged.CreateSectElement();
            root.AppendChild(sect);

            // ----- Heading (H1) -----
            HeaderElement h1 = tagged.CreateHeaderElement(1); // level 1 heading
            h1.SetText("Document Title");
            sect.AppendChild(h1);

            // ----- Paragraph -----
            ParagraphElement para = tagged.CreateParagraphElement();
            para.SetText("This is an example paragraph that provides introductory information.");
            sect.AppendChild(para);

            // ----- Table -----
            TableElement table = tagged.CreateTableElement();
            table.AlternativeText = "Sample data table"; // alt text for screen readers
            sect.AppendChild(table);

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

            // Save the modified PDF (no PreSave required)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
    }
}