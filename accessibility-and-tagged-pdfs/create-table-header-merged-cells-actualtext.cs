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
        const string outputPath = "tagged_with_header.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Access tagged content API
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Root element of the logical structure tree
            StructureElement root = tagged.RootElement;

            // Create a table element and attach it to the root
            TableElement table = tagged.CreateTableElement();
            root.AppendChild(table);

            // Create the table header (THead) and attach it to the table
            TableTHeadElement thead = tagged.CreateTableTHeadElement();
            table.AppendChild(thead);

            // Create a header row (TR) inside the THead
            TableTRElement headerRow = tagged.CreateTableTRElement();
            thead.AppendChild(headerRow);

            // First header cell (TH) – normal single column
            TableTHElement th1 = tagged.CreateTableTHElement();
            th1.SetText("Column A");               // Visible header text
            th1.ActualText = "Column A actual text"; // /ActualText attribute
            headerRow.AppendChild(th1);

            // Second header cell (TH) – merged across two columns
            TableTHElement th2 = tagged.CreateTableTHElement();
            th2.SetText("Merged Columns B‑C");
            th2.ColSpan = 2;                       // Merge two columns
            th2.ActualText = "Merged Columns B‑C actual text";
            headerRow.AppendChild(th2);

            // Third header cell (TH) – normal single column
            TableTHElement th3 = tagged.CreateTableTHElement();
            th3.SetText("Column D");
            th3.ActualText = "Column D actual text";
            headerRow.AppendChild(th3);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF with header saved to '{outputPath}'.");
    }
}