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

        // Load the PDF (lifecycle rule: create, load, save)
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content API
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Table with Header");

            // Root element of the logical structure tree
            StructureElement root = tagged.RootElement;

            // Create a Table element and attach it to the root
            TableElement table = tagged.CreateTableElement();
            table.AlternativeText = "Sample data table";
            root.AppendChild(table); // AppendChild with one argument

            // Create the table header (THead) element
            TableTHeadElement thead = tagged.CreateTableTHeadElement();
            table.AppendChild(thead);

            // Create a header row (TR) inside the THead
            TableTRElement headerRow = tagged.CreateTableTRElement();
            thead.AppendChild(headerRow);

            // First header cell that spans two columns (merged cells)
            TableTHElement th1 = tagged.CreateTableTHElement();
            th1.SetText("Merged Header");          // Visible header text
            th1.ColSpan = 2;                       // Merge two columns
            th1.ActualText = "Merged Header";      // /ActualText for accessibility
            headerRow.AppendChild(th1);

            // Second header cell (single column)
            TableTHElement th2 = tagged.CreateTableTHElement();
            th2.SetText("Column 3");
            th2.ActualText = "Column 3";
            headerRow.AppendChild(th2);

            // Save the modified PDF (lifecycle rule)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
    }
}