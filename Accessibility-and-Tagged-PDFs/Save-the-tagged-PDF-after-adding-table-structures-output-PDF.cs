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
        const string outputPath = "output_tagged.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Obtain the tagged‑content interface
                ITaggedContent tagged = doc.TaggedContent;

                // Optional: set language and title for the tagged PDF
                tagged.SetLanguage("en-US");
                tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

                // Root element of the logical structure tree (no cast required)
                StructureElement root = tagged.RootElement;

                // Create a table element and give it an alternate description
                TableElement table = tagged.CreateTableElement();
                table.AlternativeText = "Sample data table";

                // Attach the table to the root of the structure tree
                root.AppendChild(table);

                // ----- Table header (thead) -----
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

                // ----- Table body (tbody) -----
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

                // Save the tagged content changes to the underlying PDF document
                tagged.Save();

                // Finally, save the modified PDF to the desired output path
                doc.Save(outputPath);
            }

            Console.WriteLine($"Tagged PDF with table saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}