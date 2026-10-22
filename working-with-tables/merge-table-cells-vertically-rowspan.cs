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
            // Access tagged content and set basic metadata
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Table with RowSpan");

            // Root element of the structure tree
            StructureElement root = tagged.RootElement;

            // Create a table and attach it to the root
            TableElement table = tagged.CreateTableElement();
            table.AlternativeText = "Sample table with merged cells";
            root.AppendChild(table);

            // Header row
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

            // Body of the table
            TableTBodyElement tbody = tagged.CreateTableTBodyElement();
            table.AppendChild(tbody);

            // First data row
            TableTRElement row1 = tagged.CreateTableTRElement();
            tbody.AppendChild(row1);
            TableTDElement cellA = tagged.CreateTableTDElement();
            cellA.SetText("Cell A1");
            row1.AppendChild(cellA);
            TableTDElement cellB = tagged.CreateTableTDElement();
            cellB.SetText("Cell B1");
            // Merge this cell with the cell directly below
            cellB.RowSpan = 2;
            row1.AppendChild(cellB);

            // Second data row (column B is omitted because of the RowSpan)
            TableTRElement row2 = tagged.CreateTableTRElement();
            tbody.AppendChild(row2);
            TableTDElement cellC = tagged.CreateTableTDElement();
            cellC.SetText("Cell A2");
            row2.AppendChild(cellC);
            // No cell for column B – it is merged with the cell above

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved merged table PDF to '{outputPath}'.");
    }
}