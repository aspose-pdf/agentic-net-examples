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
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content API
            ITaggedContent tagged = doc.TaggedContent;

            // Enable auto‑tagging if the document is not already tagged
            AutoTaggingSettings.Default.EnableAutoTagging = true;

            // Optional metadata for accessibility
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Table with RowSpan");

            // Root of the structure tree
            StructureElement root = tagged.RootElement;

            // Create a table element and attach it to the root
            TableElement table = tagged.CreateTableElement();
            table.AlternativeText = "Sample table with merged cells";
            root.AppendChild(table);

            // ----- Table Header (optional) -----
            TableTHeadElement thead = tagged.CreateTableTHeadElement();
            table.AppendChild(thead);
            TableTRElement headerRow = tagged.CreateTableTRElement();
            thead.AppendChild(headerRow);

            TableTHElement th1 = tagged.CreateTableTHElement();
            th1.SetText("Header 1");
            headerRow.AppendChild(th1);

            TableTHElement th2 = tagged.CreateTableTHElement();
            th2.SetText("Header 2");
            headerRow.AppendChild(th2);

            // ----- Table Body -----
            TableTBodyElement tbody = tagged.CreateTableTBodyElement();
            table.AppendChild(tbody);

            // First row
            TableTRElement row1 = tagged.CreateTableTRElement();
            tbody.AppendChild(row1);

            // Cell that will span two rows
            TableTDElement cellA = tagged.CreateTableTDElement();
            cellA.SetText("Cell A1");
            cellA.RowSpan = 2;               // Merge with the cell directly below
            row1.AppendChild(cellA);

            TableTDElement cellB = tagged.CreateTableTDElement();
            cellB.SetText("Cell B1");
            row1.AppendChild(cellB);

            // Second row (first column is occupied by the RowSpan)
            TableTRElement row2 = tagged.CreateTableTRElement();
            tbody.AppendChild(row2);

            TableTDElement cellB2 = tagged.CreateTableTDElement();
            cellB2.SetText("Cell B2");
            row2.AppendChild(cellB2);

            // Save the modified PDF (no PreSave call required)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved to '{outputPath}'.");
    }
}