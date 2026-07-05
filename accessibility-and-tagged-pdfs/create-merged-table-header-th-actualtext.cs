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
        const string outputPath = "output_tagged.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the source PDF
        using (Document doc = new Document(inputPath))
        {
            // Access tagged content API
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Root element of the logical structure tree
            StructureElement root = tagged.RootElement;

            // Create a table structure element and attach it to the root
            TableElement table = tagged.CreateTableElement();
            root.AppendChild(table);

            // Create the table header (THead) element
            TableTHeadElement thead = tagged.CreateTableTHeadElement();
            table.AppendChild(thead);

            // ----- Merged header row (spans all columns) -----
            TableTRElement mergedHeaderRow = tagged.CreateTableTRElement();
            thead.AppendChild(mergedHeaderRow);

            TableTHElement mergedHeader = tagged.CreateTableTHElement();
            mergedHeader.SetText("Annual Sales Report");          // Visible header text
            mergedHeader.ActualText = "Annual Sales Report";      // /ActualText for accessibility
            mergedHeader.ColSpan = 3;                             // Merge across three columns
            mergedHeaderRow.AppendChild(mergedHeader);

            // ----- Column header row -----
            TableTRElement columnHeaderRow = tagged.CreateTableTRElement();
            thead.AppendChild(columnHeaderRow);

            TableTHElement thProduct = tagged.CreateTableTHElement();
            thProduct.SetText("Product");
            thProduct.ActualText = "Product column header";
            columnHeaderRow.AppendChild(thProduct);

            TableTHElement thRegion = tagged.CreateTableTHElement();
            thRegion.SetText("Region");
            thRegion.ActualText = "Region column header";
            columnHeaderRow.AppendChild(thRegion);

            TableTHElement thRevenue = tagged.CreateTableTHElement();
            thRevenue.SetText("Revenue");
            thRevenue.ActualText = "Revenue column header";
            columnHeaderRow.AppendChild(thRevenue);

            // Save the modified PDF (tagged structure is persisted automatically)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
    }
}