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
        const string outputPath = "output_with_footer.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the source PDF
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged‑content API
            ITaggedContent tagged = doc.TaggedContent;
            // Root element of the logical structure tree
            StructureElement root = tagged.RootElement;

            // -------------------------------------------------
            // Create a table element and attach it to the root
            // -------------------------------------------------
            TableElement table = tagged.CreateTableElement();
            table.AlternativeText = "Sample data table with footer";
            root.AppendChild(table);

            // -----------------
            // Table header (THead)
            // -----------------
            TableTHeadElement thead = tagged.CreateTableTHeadElement();
            table.AppendChild(thead);
            TableTRElement headerRow = tagged.CreateTableTRElement();
            thead.AppendChild(headerRow);

            TableTHElement thName = tagged.CreateTableTHElement();
            thName.SetText("Name");
            headerRow.AppendChild(thName);

            TableTHElement thValue = tagged.CreateTableTHElement();
            thValue.SetText("Value");
            headerRow.AppendChild(thValue);

            // ---------------
            // Table body (TBody)
            // ---------------
            TableTBodyElement tbody = tagged.CreateTableTBodyElement();
            table.AppendChild(tbody);
            TableTRElement bodyRow = tagged.CreateTableTRElement();
            tbody.AppendChild(bodyRow);

            TableTDElement tdItem = tagged.CreateTableTDElement();
            tdItem.SetText("Item A");
            bodyRow.AppendChild(tdItem);

            TableTDElement tdAmount = tagged.CreateTableTDElement();
            tdAmount.SetText("42");
            bodyRow.AppendChild(tdAmount);

            // -----------------
            // Table footer (TFoot)
            // -----------------
            TableTFootElement tfoot = tagged.CreateTableTFootElement();
            table.AppendChild(tfoot);

            // Create a footer row inside the TFoot element
            TableTRElement footerRow = tfoot.CreateTR(); // TableTFootElement provides CreateTR()
            // First cell of the footer
            TableTDElement footCell1 = tagged.CreateTableTDElement();
            footCell1.SetText("Total");
            footerRow.AppendChild(footCell1);
            // Second cell of the footer
            TableTDElement footCell2 = tagged.CreateTableTDElement();
            footCell2.SetText("42");
            footerRow.AppendChild(footCell2);

            // The TFoot element is automatically rendered at the bottom of each
            // table fragment when the table spans multiple pages.

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with table footer: {outputPath}");
    }
}