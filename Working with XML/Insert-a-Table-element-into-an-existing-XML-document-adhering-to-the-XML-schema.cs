using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;               // ITaggedContent
using Aspose.Pdf.LogicalStructure;    // StructureElement, TableElement, etc.

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";          // Existing PDF (tagged or not)
        const string outputPath = "output_with_table.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access tagged content (creates one if missing)
            ITaggedContent tagged = doc.TaggedContent;

            // Set language and title for accessibility (optional)
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Get the root structure element (no cast needed)
            StructureElement root = tagged.RootElement;

            // Create a Table element
            TableElement table = tagged.CreateTableElement();
            table.AlternativeText = "Sample data table";
            table.Title = "Sample Table";
            // Alignment property removed because TableAlignment enum is not available in this version.

            // Append the table to the root
            root.AppendChild(table);

            // ----- Create table header -----
            TableTHeadElement thead = tagged.CreateTableTHeadElement();
            table.AppendChild(thead);

            TableTRElement headerRow = tagged.CreateTableTRElement();
            thead.AppendChild(headerRow);

            TableTHElement th1 = tagged.CreateTableTHElement();
            th1.SetText("Product");
            headerRow.AppendChild(th1);

            TableTHElement th2 = tagged.CreateTableTHElement();
            th2.SetText("Quantity");
            headerRow.AppendChild(th2);

            TableTHElement th3 = tagged.CreateTableTHElement();
            th3.SetText("Price");
            headerRow.AppendChild(th3);

            // ----- Create table body -----
            TableTBodyElement tbody = tagged.CreateTableTBodyElement();
            table.AppendChild(tbody);

            // First data row
            TableTRElement row1 = tagged.CreateTableTRElement();
            tbody.AppendChild(row1);

            TableTDElement td11 = tagged.CreateTableTDElement();
            td11.SetText("Widget A");
            row1.AppendChild(td11);

            TableTDElement td12 = tagged.CreateTableTDElement();
            td12.SetText("150");
            row1.AppendChild(td12);

            TableTDElement td13 = tagged.CreateTableTDElement();
            td13.SetText("$12.99");
            row1.AppendChild(td13);

            // Second data row
            TableTRElement row2 = tagged.CreateTableTRElement();
            tbody.AppendChild(row2);

            TableTDElement td21 = tagged.CreateTableTDElement();
            td21.SetText("Gadget B");
            row2.AppendChild(td21);

            TableTDElement td22 = tagged.CreateTableTDElement();
            td22.SetText("85");
            row2.AppendChild(td22);

            TableTDElement td23 = tagged.CreateTableTDElement();
            td23.SetText("$23.50");
            row2.AppendChild(td23);

            // Save the modified PDF (no SaveOptions needed for PDF output)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with table saved to '{outputPath}'.");
    }
}
