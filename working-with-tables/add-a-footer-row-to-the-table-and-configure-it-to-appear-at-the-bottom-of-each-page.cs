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

        // Load the PDF (no special load options needed for a normal PDF)
        using (Document doc = new Document(inputPath))
        {
            // Obtain the tagged‑content helper
            ITaggedContent tagged = doc.TaggedContent;

            // Optional: set language / title for the tagged PDF
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Root element of the logical structure tree
            StructureElement root = tagged.RootElement;

            // Create a table structure element and attach it to the root
            TableElement table = tagged.CreateTableElement();
            table.AlternativeText = "Sample data table with footer";
            root.AppendChild(table);

            // -------------------------------------------------
            // Header (optional – shown here for completeness)
            // -------------------------------------------------
            TableTHeadElement thead = tagged.CreateTableTHeadElement();
            table.AppendChild(thead);
            TableTRElement headerRow = thead.CreateTR();
            TableTHElement th1 = tagged.CreateTableTHElement();
            th1.SetText("Product");
            headerRow.AppendChild(th1);
            TableTHElement th2 = tagged.CreateTableTHElement();
            th2.SetText("Price");
            headerRow.AppendChild(th2);

            // -------------------------------------------------
            // Body (optional – shown here for completeness)
            // -------------------------------------------------
            TableTBodyElement tbody = tagged.CreateTableTBodyElement();
            table.AppendChild(tbody);
            TableTRElement bodyRow = tbody.CreateTR();
            TableTDElement td1 = tagged.CreateTableTDElement();
            td1.SetText("Widget A");
            bodyRow.AppendChild(td1);
            TableTDElement td2 = tagged.CreateTableTDElement();
            td2.SetText("$25.00");
            bodyRow.AppendChild(td2);

            // -------------------------------------------------
            // Footer – the required part
            // -------------------------------------------------
            // Create the TFoot element (automatically attached to the table)
            TableTFootElement tfoot = table.CreateTFoot();

            // Create a row inside the footer
            TableTRElement footRow = tfoot.CreateTR();

            // Add cells to the footer row
            TableTDElement footCell1 = tagged.CreateTableTDElement();
            footCell1.SetText("Total");
            footRow.AppendChild(footCell1);

            TableTDElement footCell2 = tagged.CreateTableTDElement();
            footCell2.SetText("$25.00");
            footRow.AppendChild(footCell2);

            // The footer row will be rendered at the bottom of each page
            // when the table spans multiple pages. No additional settings
            // are required.

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with table footer: '{outputPath}'");
    }
}