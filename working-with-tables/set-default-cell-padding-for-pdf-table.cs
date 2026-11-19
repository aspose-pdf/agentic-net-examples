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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content API
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Root element of the structure tree
            StructureElement root = tagged.RootElement;

            // Create a table structure element
            TableElement table = tagged.CreateTableElement();

            // Define default cell padding for the entire table (left, top, right, bottom)
            table.DefaultCellPadding = new MarginInfo(5, 5, 5, 5);

            // Optional: give the table a description for accessibility
            table.AlternativeText = "Sample data table with uniform cell padding";

            // Attach the table to the root of the structure tree
            root.AppendChild(table);

            // Save the modified PDF (no PreSave required)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF with default cell padding saved to '{outputPath}'.");
    }
}