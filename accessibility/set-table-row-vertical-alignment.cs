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
            // Access the tagged content of the PDF
            ITaggedContent tagged = doc.TaggedContent;

            // Get the root structure element (no cast needed)
            StructureElement root = tagged.RootElement;

            // Create a table element and attach it to the root
            TableElement table = tagged.CreateTableElement();
            table.AlternativeText = "Sample table with vertical alignment";
            root.AppendChild(table);

            // Create a table body and attach it to the table
            TableTBodyElement tbody = tagged.CreateTableTBodyElement();
            table.AppendChild(tbody);

            // Create a table row (TableTRElement)
            TableTRElement row = tagged.CreateTableTRElement();

            // Set the vertical alignment for the row
            row.VerticalAlignment = VerticalAlignment.Center; // corrected enum member

            // Create two cells and add them to the row
            TableTDElement cell1 = tagged.CreateTableTDElement();
            cell1.SetText("Cell 1");
            row.AppendChild(cell1);

            TableTDElement cell2 = tagged.CreateTableTDElement();
            cell2.SetText("Cell 2");
            row.AppendChild(cell2);

            // Attach the row to the table body
            tbody.AppendChild(row);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
    }
}
