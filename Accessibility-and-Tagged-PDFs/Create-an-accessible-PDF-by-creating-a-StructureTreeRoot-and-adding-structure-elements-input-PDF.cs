using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;          // ITaggedContent
using Aspose.Pdf.Structure;       // ParagraphElement (structure namespace)
using Aspose.Pdf.LogicalStructure; // StructTreeRootElement, FigureElement, etc.

class AccessiblePdfCreator
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPath = "input.pdf";
        const string outputPath = "output_accessible.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        try
        {
            // Load the existing PDF document
            Document pdfDocument = new Document(inputPath);

            // Obtain the tagged‑content interface
            ITaggedContent tagged = pdfDocument.TaggedContent;

            // Set document‑level accessibility properties
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Accessible PDF Example");

            // Get the StructTreeRoot element (the root of the logical structure tree)
            StructTreeRootElement structRoot = tagged.StructTreeRootElement;

            // ------------------------------------------------------------
            // Create a paragraph structure element and add it to the tree
            // ------------------------------------------------------------
            ParagraphElement paragraph = tagged.CreateParagraphElement();
            paragraph.Title = "Introduction Paragraph";
            paragraph.AlternativeText = "This paragraph introduces the document.";
            // Append the paragraph to the root; the second argument indicates that the element
            // should be added as the last child.
            structRoot.AppendChild(paragraph, true);

            // ------------------------------------------------------------
            // Create a figure (image) structure element and add it to the tree
            // ------------------------------------------------------------
            // Use the LogicalStructure FigureElement (the type returned by CreateFigureElement)
            Aspose.Pdf.LogicalStructure.FigureElement figure = tagged.CreateFigureElement();
            figure.Title = "Sample Figure";
            figure.AlternativeText = "An illustrative figure for accessibility.";
            structRoot.AppendChild(figure, true);

            // ------------------------------------------------------------
            // Prepare the tagged content for saving and persist the PDF
            // ------------------------------------------------------------
            tagged.PreSave();   // Performs necessary pre‑save operations for tagging
            tagged.Save();      // Saves the tagged content back into the PDF document

            // Save the modified PDF to the output file
            pdfDocument.Save(outputPath);

            Console.WriteLine($"Accessible PDF created successfully: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}
