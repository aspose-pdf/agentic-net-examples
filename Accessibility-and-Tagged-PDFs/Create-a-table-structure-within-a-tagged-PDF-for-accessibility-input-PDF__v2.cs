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
        const string outputPath = "tagged_table.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content API
            ITaggedContent tagged = doc.TaggedContent;

            // Set language and title for the PDF (accessibility metadata)
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Root element of the logical structure tree
            StructureElement root = tagged.RootElement;

            // Create a Table structure element
            TableElement table = tagged.CreateTableElement();
            table.AlternativeText = "Sample data table for accessibility";
            // Optional visual positioning (coordinates are in points)
            table.Left = 50;
            table.Top = 700;
            root.AppendChild(table); // attach table to the root

            // ----- Table Header (THead) -----
            TableTHeadElement thead = tagged.CreateTableTHeadElement();
            table.AppendChild(thead);
            TableTRElement headerRow = tagged.CreateTableTRElement();
            thead.AppendChild(headerRow);

            TableTHElement thProduct = tagged.CreateTableTHElement();
            thProduct.SetText("Product");
            headerRow.AppendChild(thProduct);

            TableTHElement thPrice = tagged.CreateTableTHElement();
            thPrice.SetText("Price");
            headerRow.AppendChild(thPrice);

            // ----- Table Body (TBody) -----
            TableTBodyElement tbody = tagged.CreateTableTBodyElement();
            table.AppendChild(tbody);

            // First data row
            TableTRElement row1 = tagged.CreateTableTRElement();
            tbody.AppendChild(row1);
            TableTDElement td1 = tagged.CreateTableTDElement();
            td1.SetText("Widget A");
            row1.AppendChild(td1);
            TableTDElement td2 = tagged.CreateTableTDElement();
            td2.SetText("$10.00");
            row1.AppendChild(td2);

            // Second data row
            TableTRElement row2 = tagged.CreateTableTRElement();
            tbody.AppendChild(row2);
            TableTDElement td3 = tagged.CreateTableTDElement();
            td3.SetText("Widget B");
            row2.AppendChild(td3);
            TableTDElement td4 = tagged.CreateTableTDElement();
            td4.SetText("$15.00");
            row2.AppendChild(td4);

            // Save the modified PDF (no PreSave required)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF with table saved to '{outputPath}'.");
    }
}