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

        // Create a new PDF document
        Document pdfDocument = new Document();

        // Add a page to the document
        Page page = pdfDocument.Pages.Add();

        // Add visible text to the page
        TextFragment text = new TextFragment("Hello, accessible PDF!");
        text.TextState.FontSize = 14;
        text.TextState.Font = FontRepository.FindFont("Arial");
        page.Paragraphs.Add(text);

        // Enable tagging and set document metadata
        ITaggedContent tagged = pdfDocument.TaggedContent;
        tagged.SetLanguage("en-US");
        tagged.SetTitle("Sample Tagged PDF");

        // Create a paragraph structure element and attach it to the root element
        var paragraphElement = tagged.CreateParagraphElement();
        tagged.RootElement.AppendChild(paragraphElement);

        // Prepare tagged content before saving
        tagged.PreSave();

        // Save the PDF (using the prescribed save rule)
        pdfDocument.Save(outputPath);
    }
}