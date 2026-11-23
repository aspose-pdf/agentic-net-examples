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
        const string outputPath = "output_tagged_table.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content interface
            ITaggedContent tagged = doc.TaggedContent;

            // Optional: set language and title for the tagged PDF
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Root of the logical structure tree (no cast required)
            StructureElement root = tagged.RootElement;

            // Create a table element and attach it to the root
            TableElement table = tagged.CreateTableElement();
            table.AlternativeText = "Sample data table with header, body, and footer.";
            root.AppendChild(table);

            // ---------- Header ----------
            TableTHeadElement thead = tagged.CreateTableTHeadElement();
            table.AppendChild(thead);

            TableTRElement headerRow = tagged.CreateTableTRElement();
            thead.AppendChild(headerRow);

            TableTHElement th1 = tagged.CreateTableTHElement();
            th1.SetText("Column A");
            headerRow.AppendChild(th1);

            TableTHElement th2 = tagged.CreateTableTHElement();
            th2.SetText("Column B");
            headerRow.AppendChild(th2);

            // ---------- Body ----------
            TableTBodyElement tbody = tagged.CreateTableTBodyElement();
            table.AppendChild(tbody);

            // First data row
            TableTRElement bodyRow1 = tagged.CreateTableTRElement();
            tbody.AppendChild(bodyRow1);

            TableTDElement td11 = tagged.CreateTableTDElement();
            td11.SetText("Row1-Cell1");
            bodyRow1.AppendChild(td11);

            TableTDElement td12 = tagged.CreateTableTDElement();
            td12.SetText("Row1-Cell2");
            bodyRow1.AppendChild(td12);

            // Second data row
            TableTRElement bodyRow2 = tagged.CreateTableTRElement();
            tbody.AppendChild(bodyRow2);

            TableTDElement td21 = tagged.CreateTableTDElement();
            td21.SetText("Row2-Cell1");
            bodyRow2.AppendChild(td21);

            TableTDElement td22 = tagged.CreateTableTDElement();
            td22.SetText("Row2-Cell2");
            bodyRow2.AppendChild(td22);

            // ---------- Footer ----------
            TableTFootElement tfoot = tagged.CreateTableTFootElement();
            table.AppendChild(tfoot);

            TableTRElement footerRow = tagged.CreateTableTRElement();
            tfoot.AppendChild(footerRow);

            TableTDElement footCell1 = tagged.CreateTableTDElement();
            footCell1.SetText("Footer1");
            footerRow.AppendChild(footCell1);

            TableTDElement footCell2 = tagged.CreateTableTDElement();
            footCell2.SetText("Footer2");
            footerRow.AppendChild(footCell2);

            // Save the modified PDF (no PreSave required)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF with table saved to '{outputPath}'.");
    }
}