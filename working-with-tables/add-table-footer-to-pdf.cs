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
        const string outputPath = "output_with_footer_table.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content API
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Root of the logical structure tree
            StructureElement root = tagged.RootElement;

            // Create a table element and attach it to the root
            TableElement table = tagged.CreateTableElement();
            table.AlternativeText = "Sample data table with footer";
            root.AppendChild(table);

            // ----- Header -----
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

            // ----- Body -----
            TableTBodyElement tbody = tagged.CreateTableTBodyElement();
            table.AppendChild(tbody);

            // Example data row
            TableTRElement dataRow = tagged.CreateTableTRElement();
            tbody.AppendChild(dataRow);

            TableTDElement tdItem = tagged.CreateTableTDElement();
            tdItem.SetText("Widget A");
            dataRow.AppendChild(tdItem);

            TableTDElement tdCost = tagged.CreateTableTDElement();
            tdCost.SetText("$10");
            dataRow.AppendChild(tdCost);

            // ----- Footer -----
            TableTFootElement tfoot = tagged.CreateTableTFootElement();
            // The footer will automatically repeat on each page when the table spans multiple pages.
            // No explicit Repeat property exists on TableTFootElement in the current Aspose.Pdf version.
            table.AppendChild(tfoot);
            TableTRElement footerRow = tagged.CreateTableTRElement();
            tfoot.AppendChild(footerRow);

            TableTDElement footCell1 = tagged.CreateTableTDElement();
            footCell1.SetText("Total");
            footerRow.AppendChild(footCell1);

            TableTDElement footCell2 = tagged.CreateTableTDElement();
            footCell2.SetText("$10");
            footerRow.AppendChild(footCell2);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
