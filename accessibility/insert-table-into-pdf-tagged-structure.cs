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
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content of the document
            ITaggedContent tagged = doc.TaggedContent;

            // Root element of the logical structure tree
            StructureElement root = tagged.RootElement;

            // Create a new table element and add it to the root
            TableElement table = tagged.CreateTableElement();
            table.AlternativeText = "Sample data table";
            root.AppendChild(table); // attach table to the root

            // ----- Build table header -----
            TableTHeadElement thead = tagged.CreateTableTHeadElement();
            TableTRElement headerRow = tagged.CreateTableTRElement();
            TableTHElement th1 = tagged.CreateTableTHElement();
            th1.SetText("Column A");
            TableTHElement th2 = tagged.CreateTableTHElement();
            th2.SetText("Column B");

            // Insert header row and header cells at specific positions
            thead.InsertChild(headerRow, 0);   // first child of thead
            headerRow.InsertChild(th1, 0);     // first cell
            headerRow.InsertChild(th2, 1);     // second cell

            // Attach header to the table
            table.AppendChild(thead);

            // ----- Build table body -----
            TableTBodyElement tbody = tagged.CreateTableTBodyElement();
            TableTRElement dataRow = tagged.CreateTableTRElement();
            TableTDElement td1 = tagged.CreateTableTDElement();
            td1.SetText("Value 1");
            TableTDElement td2 = tagged.CreateTableTDElement();
            td2.SetText("Value 2");

            // Insert data cells and row
            dataRow.InsertChild(td1, 0);
            dataRow.InsertChild(td2, 1);
            tbody.InsertChild(dataRow, 0); // first row in tbody

            // Attach body to the table
            table.AppendChild(tbody);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}