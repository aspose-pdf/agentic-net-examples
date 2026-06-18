using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";          // existing PDF (can be empty)
        const string outputPath = "output_with_header.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF and obtain the tagged‑content helper
        using (Document doc = new Document(inputPath))
        {
            ITaggedContent tagged = doc.TaggedContent;

            // Set language and title (optional)
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Root element of the logical structure tree
            StructureElement root = tagged.RootElement;

            // Create a table element and add it to the root
            TableElement table = tagged.CreateTableElement();
            table.AlternativeText = "Sample data table with repeating header";
            root.AppendChild(table);

            // Create the table header (THead) group
            TableTHeadElement thead = tagged.CreateTableTHeadElement();
            table.AppendChild(thead);

            // Create a header row (TableTRElement) inside the THead
            TableTRElement headerRow = tagged.CreateTableTRElement();
            // No IsHeader property – the row is inside THead, which makes it a header.
            thead.AppendChild(headerRow);

            // Add header cells (TH) – column titles
            TableTHElement th1 = tagged.CreateTableTHElement();
            th1.SetText("Product");
            headerRow.AppendChild(th1);

            TableTHElement th2 = tagged.CreateTableTHElement();
            th2.SetText("Quantity");
            headerRow.AppendChild(th2);

            TableTHElement th3 = tagged.CreateTableTHElement();
            th3.SetText("Price");
            headerRow.AppendChild(th3);

            // Create the table body (TBody) group
            TableTBodyElement tbody = tagged.CreateTableTBodyElement();
            table.AppendChild(tbody);

            // Add a few data rows
            for (int i = 1; i <= 5; i++)
            {
                TableTRElement dataRow = tagged.CreateTableTRElement();
                tbody.AppendChild(dataRow);

                TableTDElement td1 = tagged.CreateTableTDElement();
                td1.SetText($"Item {i}");
                dataRow.AppendChild(td1);

                TableTDElement td2 = tagged.CreateTableTDElement();
                td2.SetText((i * 10).ToString());
                dataRow.AppendChild(td2);

                TableTDElement td3 = tagged.CreateTableTDElement();
                td3.SetText($"${i * 2.5:F2}");
                dataRow.AppendChild(td3);
            }

            // Ensure the header row is repeated on each new page
            table.RepeatingRowsCount = 1;   // repeat the first row (the header)

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with repeating table header saved to '{outputPath}'.");
    }
}
