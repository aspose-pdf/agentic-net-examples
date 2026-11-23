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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access tagged content (creates a tagged structure if missing)
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Root of the structure tree
            StructureElement root = tagged.RootElement;

            // Create a new table element and attach it to the root
            TableElement table = tagged.CreateTableElement();
            table.AlternativeText = "Sample data table";
            root.AppendChild(table); // attach table to the document structure

            // Create the table header using TableElement.CreateTHead (auto‑attached)
            TableTHeadElement thead = table.CreateTHead();

            // Add a header row
            TableTRElement headerRow = thead.CreateTR();

            // Add header cells (TH) and set their text
            TableTHElement th1 = headerRow.CreateTH();
            th1.SetText("Column A");

            TableTHElement th2 = headerRow.CreateTH();
            th2.SetText("Column B");

            // (Optional) create a body with one data row for completeness
            TableTBodyElement tbody = table.CreateTBody();
            TableTRElement bodyRow = tbody.CreateTR();

            TableTDElement td1 = bodyRow.CreateTD();
            td1.SetText("Value 1");

            TableTDElement td2 = bodyRow.CreateTD();
            td2.SetText("Value 2");

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF with table header saved to '{outputPath}'.");
    }
}