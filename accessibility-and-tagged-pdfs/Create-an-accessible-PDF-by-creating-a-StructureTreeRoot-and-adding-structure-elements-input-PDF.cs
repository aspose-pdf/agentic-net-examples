using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;                 // ITaggedContent
using Aspose.Pdf.LogicalStructure;      // StructureElement, ParagraphElement, FigureElement, etc.

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "accessible_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Access tagged‑content interface
            ITaggedContent tagged = doc.TaggedContent;

            // Set document language and title (write‑only setters)
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Get the StructTreeRootElement – the top of the logical structure tree
            StructTreeRootElement structRoot = tagged.StructTreeRootElement;

            // -------------------------------------------------
            // Example: add a paragraph element with accessible text
            // -------------------------------------------------
            ParagraphElement paragraph = tagged.CreateParagraphElement();
            paragraph.SetText("This PDF has been made accessible using a custom structure tree.");
            // Append to the StructTreeRootElement (bool parameter has a default value)
            structRoot.AppendChild(paragraph);

            // -------------------------------------------------
            // Example: add a figure element (e.g., for an image) with alternate text
            // -------------------------------------------------
            FigureElement figure = tagged.CreateFigureElement();
            figure.AlternativeText = "Sample diagram illustrating the workflow.";
            structRoot.AppendChild(figure);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Accessible PDF saved to '{outputPath}'.");
    }
}