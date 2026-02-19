using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Tagged;

class Program
{
    static void Main()
    {
        // Output file path
        string outputPath = "TaggedDocument.pdf";

        // Create a new empty PDF document
        Document pdfDoc = new Document();

        // Add a page to the document
        Page page = pdfDoc.Pages.Add();

        // Add a heading to the page
        TextFragment heading = new TextFragment("Sample Heading");
        heading.TextState.FontSize = 20;
        heading.TextState.FontStyle = FontStyles.Bold;
        page.Paragraphs.Add(heading);

        // Add a paragraph to the page
        TextFragment paragraph = new TextFragment(
            "This is a sample paragraph in an accessible PDF document.");
        paragraph.TextState.FontSize = 12;
        page.Paragraphs.Add(paragraph);

        // Obtain the tagged content interface
        ITaggedContent tagged = pdfDoc.TaggedContent;

        // Set document language and title for accessibility
        tagged.SetLanguage("en-US");
        tagged.SetTitle("Accessible Tagged PDF Example");

        // Create logical structure elements (optional, can be expanded)
        // Header element (level 1) and a paragraph element
        var headerElement = tagged.CreateHeaderElement(1);
        var paragraphElement = tagged.CreateParagraphElement();

        // Prepare the tagged content before saving
        tagged.PreSave();

        // Save the tagged content into the PDF document
        tagged.Save();

        // Save the final PDF file (uses the provided document-save rule)
        pdfDoc.Save(outputPath);
    }
}