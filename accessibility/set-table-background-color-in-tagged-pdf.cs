using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath  = "input_tagged.pdf";
        const string outputPath = "output_with_table.pdf";

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

            // Ensure the document is tagged (optional safety check)
            if (taggedContent == null)
            {
                Console.Error.WriteLine("The PDF is not tagged.");
                return;
            }

            // Get the root structure element (no cast required)
            StructureElement root = taggedContent.RootElement;

            // Create a new table element via the ITaggedContent factory
            TableElement table = taggedContent.CreateTableElement();

            // Set the background color of the table (using Aspose.Pdf.Color)
            table.BackgroundColor = Aspose.Pdf.Color.LightGray; // or Color.FromRgb(0.9, 0.9, 0.9)

            // Optionally add a description for accessibility
            table.AlternativeText = "Sample table with background color";

            // Append the table to the root of the structure tree
            root.AppendChild(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF saved with table background color to '{outputPath}'.");
    }
}