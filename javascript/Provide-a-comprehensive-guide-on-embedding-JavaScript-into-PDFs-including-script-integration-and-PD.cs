using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class JavaScriptEmbeddingDemo
{
    static void Main()
    {
        // Paths to the source PDF and the output PDF
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_js.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // 1️⃣ Add JavaScript that runs automatically when the document is opened
            AddDocumentOpenJavaScript(pdfDoc, "app.alert('Document opened!');");

            // 2️⃣ Add a clickable link on page 1 that runs JavaScript when the user clicks it
            //    The rectangle defines the clickable area (llx, lly, urx, ury)
            AddLinkJavaScript(
                pdfDoc,
                pageNumber: 1,
                rect: new Aspose.Pdf.Rectangle(100, 500, 300, 550),
                script: "app.alert('Link clicked!');"
            );

            // Save the modified PDF. No SaveOptions are needed because the output format is PDF.
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with JavaScript to '{outputPath}'.");
    }

    // Embeds a JavaScript action that executes when the PDF is opened.
    static void AddDocumentOpenJavaScript(Document doc, string script)
    {
        // JavascriptAction constructor accepts the script code as a string.
        JavascriptAction jsAction = new JavascriptAction(script);

        // Assign the action to the document's OpenAction property.
        // This action is triggered automatically when the PDF viewer opens the file.
        doc.OpenAction = jsAction;
    }

    // Adds a link annotation to a specific page that runs JavaScript when activated.
    static void AddLinkJavaScript(Document doc, int pageNumber, Aspose.Pdf.Rectangle rect, string script)
    {
        // Validate the page number (Aspose.Pdf uses 1‑based indexing).
        if (pageNumber < 1 || pageNumber > doc.Pages.Count)
            throw new ArgumentOutOfRangeException(nameof(pageNumber));

        // Retrieve the target page.
        Page page = doc.Pages[pageNumber];

        // Create a link annotation covering the specified rectangle.
        LinkAnnotation link = new LinkAnnotation(page, rect);

        // Attach a JavaScript action to the link.
        link.Action = new JavascriptAction(script);

        // Optional visual styling for the link (blue border).
        link.Color = Aspose.Pdf.Color.Blue;
        link.Border = new Border(link) { Width = 1 };

        // Add the annotation to the page's annotation collection.
        page.Annotations.Add(link);
    }
}