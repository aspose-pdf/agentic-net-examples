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
        const string outputPath = "output_with_table.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content API
            ITaggedContent tagged = doc.TaggedContent;

            // Set language and title for the tagged PDF
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Get the root structure element (no cast needed)
            StructureElement root = tagged.RootElement;

            // Create a Table structure element
            TableElement table = tagged.CreateTableElement();
            table.AlternativeText = "Sample data table";
            root.AppendChild(table); // AppendChild with one argument

            // ----- Table Header -----
            TableTHeadElement thead = tagged.CreateTableTHeadElement();
            table.AppendChild(thead);

            TableTRElement headerRow = tagged.CreateTableTRElement();
            thead.AppendChild(headerRow);

            TableTHElement th1 = tagged.CreateTableTHElement();
            th1.SetText("Product");
            headerRow.AppendChild(th1);

            TableTHElement th2 = tagged.CreateTableTHElement();
            th2.SetText("Price");
            headerRow.AppendChild(th2);

            // ----- Table Body -----
            TableTBodyElement tbody = tagged.CreateTableTBodyElement();
            table.AppendChild(tbody);

            TableTRElement dataRow = tagged.CreateTableTRElement();
            tbody.AppendChild(dataRow);

            TableTDElement td1 = tagged.CreateTableTDElement();
            td1.SetText("Widget A");
            dataRow.AppendChild(td1);

            TableTDElement td2 = tagged.CreateTableTDElement();
            td2.SetText("$49.99");
            dataRow.AppendChild(td2);

            // Save the modified PDF (lifecycle rule: use Save without extra options)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with table saved to '{outputPath}'.");
    }
}