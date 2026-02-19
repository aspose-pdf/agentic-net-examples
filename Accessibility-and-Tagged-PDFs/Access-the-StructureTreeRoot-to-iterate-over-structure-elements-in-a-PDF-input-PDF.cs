using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;          // ITaggedContent
using Aspose.Pdf.Structure;       // StructureElement
using Aspose.Pdf.LogicalStructure; // StructTreeRootElement
using LogicalElement = Aspose.Pdf.LogicalStructure.Element; // Alias to resolve ambiguity

class Program
{
    static void Main(string[] args)
    {
        // Path to the input PDF
        const string inputPath = "input.pdf";

        // Verify that the file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPath);

            // Access the tagged (logical) content via the Document.TaggedContent property
            ITaggedContent taggedContent = pdfDocument.TaggedContent;
            if (taggedContent != null && taggedContent.StructTreeRootElement != null)
            {
                // Access the StructTreeRootElement which is the root of the logical structure tree
                StructTreeRootElement structRoot = taggedContent.StructTreeRootElement;

                Console.WriteLine("Structure elements in the PDF (StructTreeRoot):");
                IterateElements(structRoot, 0);
            }
            else
            {
                Console.WriteLine("The PDF does not contain tagged (logical) content.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred while processing the PDF: {ex.Message}");
        }
    }

    // Recursively iterate over a LogicalStructure.Element and its children, printing basic information
    static void IterateElements(LogicalElement element, int level)
    {
        string indent = new string(' ', level * 2);
        Console.WriteLine($"{indent}- Element Type: {element.GetType().Name}");

        // If the element is a StructureElement, display optional Title and AlternativeText
        if (element is StructureElement structElem)
        {
            if (!string.IsNullOrEmpty(structElem.Title))
                Console.WriteLine($"{indent}  Title: {structElem.Title}");
            if (!string.IsNullOrEmpty(structElem.AlternativeText))
                Console.WriteLine($"{indent}  AltText: {structElem.AlternativeText}");
        }

        // Recurse into child elements
        foreach (LogicalElement child in element.ChildElements)
        {
            IterateElements(child, level + 1);
        }
    }
}