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
        const string outputPath = "output_cleared.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content (logical structure)
            ITaggedContent taggedContent = doc.TaggedContent;

            // Get the root of the structure tree
            StructureElement root = taggedContent.RootElement;

            // Find all Section (Sect) elements in the tree (recursive search)
            var sections = root.FindElements<SectElement>(true);

            // Clear all children from each Section element
            foreach (SectElement sect in sections)
            {
                sect.ClearChilds();
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with cleared sections: {outputPath}");
    }
}
