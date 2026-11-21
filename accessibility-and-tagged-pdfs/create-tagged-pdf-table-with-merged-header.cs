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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content interface
            ITaggedContent taggedContent = doc.TaggedContent;
            taggedContent.SetLanguage("en-US");
            taggedContent.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Root element of the structure tree
            StructureElement root = taggedContent.RootElement;

            // Create a table structure element and attach it to the root
            TableElement table = taggedContent.CreateTableElement();
            table.AlternativeText = "Sample data table";
            root.AppendChild(table);

            // Create the table header (thead) and attach it to the table
            TableTHeadElement thead = taggedContent.CreateTableTHeadElement();
            table.AppendChild(thead);

            // Create a header row (tr) inside the thead
            TableTRElement headerRow = taggedContent.CreateTableTRElement();
            thead.AppendChild(headerRow);

            // First header cell (TH)
            TableTHElement th1 = taggedContent.CreateTableTHElement();
            th1.SetText("Header 1");                 // Visible text
            th1.ActualText = "Actual header 1 text"; // /ActualText attribute
            headerRow.AppendChild(th1);

            // Second header cell (TH)
            TableTHElement th2 = taggedContent.CreateTableTHElement();
            th2.SetText("Header 2");
            th2.ActualText = "Actual header 2 text";
            headerRow.AppendChild(th2);

            // Additional rows/cells can be added similarly using CreateTableTBodyElement, etc.

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
    }
}