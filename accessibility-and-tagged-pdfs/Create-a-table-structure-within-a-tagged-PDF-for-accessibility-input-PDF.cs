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
        const string outputPath = "tagged_table.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Access tagged content API
            ITaggedContent tagged = doc.TaggedContent;

            // Set language and title for the PDF (accessibility metadata)
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Root of the logical structure tree
            StructureElement root = tagged.RootElement;

            // Create a Table structure element
            TableElement table = tagged.CreateTableElement();
            table.AlternativeText = "Sample data table for accessibility";
            table.Title = "Sample Table";
            root.AppendChild(table); // attach table to the root

            // ----- Table Header -----
            TableTHeadElement thead = tagged.CreateTableTHeadElement();
            table.AppendChild(thead);

            TableTRElement headerRow = tagged.CreateTableTRElement();
            thead.AppendChild(headerRow);

            TableTHElement thProduct = tagged.CreateTableTHElement();
            thProduct.SetText("Product");
            headerRow.AppendChild(thProduct);

            TableTHElement thQuantity = tagged.CreateTableTHElement();
            thQuantity.SetText("Quantity");
            headerRow.AppendChild(thQuantity);

            // ----- Table Body -----
            TableTBodyElement tbody = tagged.CreateTableTBodyElement();
            table.AppendChild(tbody);

            TableTRElement dataRow = tagged.CreateTableTRElement();
            tbody.AppendChild(dataRow);

            TableTDElement tdWidget = tagged.CreateTableTDElement();
            tdWidget.SetText("Widget A");
            dataRow.AppendChild(tdWidget);

            TableTDElement tdQty = tagged.CreateTableTDElement();
            tdQty.SetText("150");
            dataRow.AppendChild(tdQty);

            // Save the modified PDF (tagged content is persisted automatically)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF with table saved to '{outputPath}'.");
    }
}