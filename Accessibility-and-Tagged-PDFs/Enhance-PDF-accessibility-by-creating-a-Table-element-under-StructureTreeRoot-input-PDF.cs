using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;               // ITaggedContent interface
using Aspose.Pdf.LogicalStructure;    // TableElement and related logical structure classes
using Aspose.Pdf.Structure;            // StructTreeRootElement

class Program
{
    static void Main()
    {
        // Paths for input and output PDFs
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        Document pdfDoc = new Document(inputPath);

        // Obtain the tagged‑content helper
        ITaggedContent tagged = pdfDoc.TaggedContent;

        // Get the structure tree root element (the top of the logical structure)
        StructTreeRootElement structRoot = tagged.StructTreeRootElement;
        if (structRoot == null)
        {
            Console.Error.WriteLine("The PDF does not contain a structure tree root.");
            return;
        }

        // Create a new Table element in the logical structure
        TableElement table = tagged.CreateTableElement();

        // Optional: set descriptive properties for accessibility
        table.Title = "Sample Table";
        table.AlternativeText = "A sample data table for screen readers";

        // Append the table element to the structure tree root
        structRoot.AppendChild(table, true);

        // Save the modified PDF (uses the provided document-save rule)
        pdfDoc.Save(outputPath);
    }
}