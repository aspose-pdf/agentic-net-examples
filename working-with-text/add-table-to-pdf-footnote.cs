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
        const string outputPath = "output_with_footnote.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF and work with its tagged content
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content interface
            ITaggedContent taggedContent = doc.TaggedContent;

            // Optional: set language and title for the document
            taggedContent.SetLanguage("en-US");
            taggedContent.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Root element of the structure tree
            StructureElement root = taggedContent.RootElement;

            // ------------------------------------------------------------
            // Create a footnote (NoteElement) and add it to the root
            // ------------------------------------------------------------
            NoteElement footnote = taggedContent.CreateNoteElement();
            footnote.SetText("Footnote:");
            root.AppendChild(footnote);

            // ------------------------------------------------------------
            // Create a table and add it to the footnote
            // ------------------------------------------------------------
            TableElement table = taggedContent.CreateTableElement();
            table.AlternativeText = "Sample data table in footnote";
            footnote.AppendChild(table);

            // Header row
            TableTHeadElement thead = taggedContent.CreateTableTHeadElement();
            table.AppendChild(thead);
            TableTRElement headerRow = taggedContent.CreateTableTRElement();
            thead.AppendChild(headerRow);

            TableTHElement th1 = taggedContent.CreateTableTHElement();
            th1.SetText("Item");
            headerRow.AppendChild(th1);

            TableTHElement th2 = taggedContent.CreateTableTHElement();
            th2.SetText("Quantity");
            headerRow.AppendChild(th2);

            // Body rows
            TableTBodyElement tbody = taggedContent.CreateTableTBodyElement();
            table.AppendChild(tbody);

            // First data row
            TableTRElement dataRow1 = taggedContent.CreateTableTRElement();
            tbody.AppendChild(dataRow1);

            TableTDElement td1a = taggedContent.CreateTableTDElement();
            td1a.SetText("Apples");
            dataRow1.AppendChild(td1a);

            TableTDElement td1b = taggedContent.CreateTableTDElement();
            td1b.SetText("10");
            dataRow1.AppendChild(td1b);

            // Second data row
            TableTRElement dataRow2 = taggedContent.CreateTableTRElement();
            tbody.AppendChild(dataRow2);

            TableTDElement td2a = taggedContent.CreateTableTDElement();
            td2a.SetText("Oranges");
            dataRow2.AppendChild(td2a);

            TableTDElement td2b = taggedContent.CreateTableTDElement();
            td2b.SetText("5");
            dataRow2.AppendChild(td2b);

            // Save the modified PDF (no PreSave call needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with footnote table saved to '{outputPath}'.");
    }
}