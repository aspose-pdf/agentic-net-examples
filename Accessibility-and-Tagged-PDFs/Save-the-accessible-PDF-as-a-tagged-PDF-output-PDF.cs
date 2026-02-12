using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Tagged;

class Program
{
    static void Main()
    {
        // Paths for input and output PDFs
        const string inputPath = "input.pdf";
        const string outputPath = "output_tagged.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        Document pdfDocument = new Document(inputPath);

        // -----------------------------------------------------------------
        // Add a free‑text annotation with a title on the first page
        // -----------------------------------------------------------------
        Page page = pdfDocument.Pages[1];

        // Define the rectangle that bounds the annotation (llx, lly, urx, ury)
        Aspose.Pdf.Rectangle annotRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

        // Create the TextAnnotation object
        TextAnnotation textAnnot = new TextAnnotation(page, annotRect)
        {
            Contents = "This is a note added to the PDF.",
            Title = "Note Title"
        };

        // Initialize the border for the annotation (border‑initialization rule)
        textAnnot.Border = new Border(textAnnot)
        {
            Style = BorderStyle.Solid,
            Width = 1
        };
        // Set the annotation color
        textAnnot.Color = Color.Blue;

        // Add the annotation to the page's annotation collection
        page.Annotations.Add(textAnnot);

        // -----------------------------------------------------------------
        // Enable tagging to make the PDF accessible
        // -----------------------------------------------------------------
        ITaggedContent tagged = pdfDocument.TaggedContent;

        // Set document language and title for accessibility
        tagged.SetLanguage("en-US");
        tagged.SetTitle("Accessible PDF with Text Annotation");

        // Prepare the tagged structure before saving
        tagged.PreSave();

        // Save the modified PDF (document-save rule)
        pdfDocument.Save(outputPath);

        Console.WriteLine($"Tagged PDF successfully saved to: {outputPath}");
    }
}