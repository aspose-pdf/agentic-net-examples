using System;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "email_link.pdf";

        // Ensure the Document is disposed properly
        using (Document doc = new Document())
        {
            // Add a new page to the document
            Page page = doc.Pages.Add();

            // Create visible text that will act as the link label
            TextFragment tf = new TextFragment("Email us");
            tf.Position = new Position(100, 700);               // Position on the page
            tf.TextState.FontSize = 12;
            tf.TextState.FontStyle = FontStyles.Bold;
            page.Paragraphs.Add(tf);

            // Define a rectangle that covers the text area
            // (llx, lly, urx, ury) – lower‑left and upper‑right corners
            Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(100, 680, 200, 720);

            // Create a link annotation with a mailto: URI
            LinkAnnotation link = new LinkAnnotation(page, linkRect)
            {
                Action = new GoToURIAction("mailto:support@example.com")
            };

            // Add the annotation to the page
            page.Annotations.Add(link);

            // Save the PDF file
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with email link saved to '{outputPath}'.");
    }
}