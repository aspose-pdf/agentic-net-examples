using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath  = "input_tagged.pdf";
        const string outputPath = "output_tagged.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the tagged PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content interface
            ITaggedContent tagged = doc.TaggedContent;

            // Optional: set language and title for the document
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Get the root structure element (no cast required)
            StructureElement root = tagged.RootElement;

            // Create a new table element via the factory and attach it to the root
            TableElement table = tagged.CreateTableElement();
            table.AlternativeText = "Sample data table";
            root.AppendChild(table);

            // Create a table body using TableElement.CreateTBody (automatically attached)
            TableTBodyElement tbody = table.CreateTBody();

            // Create a row and attach it to the body
            TableTRElement row = tagged.CreateTableTRElement();
            tbody.AppendChild(row);

            // Create two cells, set their text, and attach them to the row
            TableTDElement cell1 = tagged.CreateTableTDElement();
            cell1.SetText("Item 1");
            row.AppendChild(cell1);

            TableTDElement cell2 = tagged.CreateTableTDElement();
            cell2.SetText("Value 1");
            row.AppendChild(cell2);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}