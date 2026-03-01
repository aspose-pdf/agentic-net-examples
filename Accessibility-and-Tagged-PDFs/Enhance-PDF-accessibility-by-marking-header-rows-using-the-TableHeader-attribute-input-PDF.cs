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
        const string outputPath = "accessible_table_with_header.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content API
            ITaggedContent tagged = doc.TaggedContent;

            // Set language and title for the tagged PDF
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Root element of the logical structure tree
            StructureElement root = tagged.RootElement;

            // Create a Table structure element
            TableElement table = tagged.CreateTableElement();
            table.AlternativeText = "Sample data table with header rows";
            root.AppendChild(table); // attach table to the root

            // ----- Create the table header (THead) -----
            TableTHeadElement thead = tagged.CreateTableTHeadElement();
            table.AppendChild(thead); // attach THead to the table

            // Header row
            TableTRElement headerRow = tagged.CreateTableTRElement();
            thead.AppendChild(headerRow);

            // Header cells (TH)
            string[] headers = { "Product", "Quantity", "Price" };
            foreach (string hdr in headers)
            {
                TableTHElement th = tagged.CreateTableTHElement();
                th.SetText(hdr);               // set header text
                headerRow.AppendChild(th);     // add header cell to the header row
            }

            // ----- Create the table body (TBody) -----
            TableTBodyElement tbody = tagged.CreateTableTBodyElement();
            table.AppendChild(tbody); // attach TBody to the table

            // Example data row
            TableTRElement dataRow = tagged.CreateTableTRElement();
            tbody.AppendChild(dataRow);

            // Data cells (TD)
            string[] data = { "Widget A", "10", "$25.00" };
            foreach (string cellText in data)
            {
                TableTDElement td = tagged.CreateTableTDElement();
                td.SetText(cellText);          // set cell text
                dataRow.AppendChild(td);       // add cell to the data row
            }

            // Save the modified PDF (no PreSave needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Accessible PDF with table header saved to '{outputPath}'.");
    }
}