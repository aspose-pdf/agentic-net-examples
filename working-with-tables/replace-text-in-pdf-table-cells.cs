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
        const string outputPath = "output.pdf";
        const string keyword    = "PLACEHOLDER";   // text to search for
        const string newContent = "Replaced Text"; // new cell content

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access tagged content (requires the PDF to be tagged)
            ITaggedContent tagged = doc.TaggedContent;

            // Get the root of the structure tree
            StructureElement root = tagged.RootElement;

            // Find all table cell elements (TableTDElement) in the document
            var cells = root.FindElements<TableTDElement>(true);

            foreach (TableTDElement cell in cells)
            {
                // Check if the cell's current text contains the keyword
                if (!string.IsNullOrEmpty(cell.ActualText) && cell.ActualText.Contains(keyword))
                {
                    // Replace the cell's text with the new content
                    cell.SetText(newContent);
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Cell content replacement completed. Output saved to '{outputPath}'.");
    }
}