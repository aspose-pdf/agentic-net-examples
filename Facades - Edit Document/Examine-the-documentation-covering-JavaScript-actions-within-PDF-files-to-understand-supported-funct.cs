using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class JavaScriptActionsDemo
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPdf  = "sample.pdf";
        const string outputPdf = "sample_with_js.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // ------------------------------------------------------------
        // 1. Use PdfContentEditor (Facade) to add document‑level JavaScript actions
        // ------------------------------------------------------------
        PdfContentEditor editor = new PdfContentEditor();

        // Bind the existing PDF file to the editor
        editor.BindPdf(inputPdf);

        // Add a JavaScript action that runs when the document is opened
        editor.AddDocumentAdditionalAction(PdfContentEditor.DocumentOpen,
            "app.alert('Document opened!');");

        // Add a JavaScript action that runs when the document is closed
        editor.AddDocumentAdditionalAction(PdfContentEditor.DocumentClose,
            "app.alert('Document closed!');");

        // Save the modified PDF (the editor implements SaveableFacade)
        editor.Save(outputPdf);

        // ------------------------------------------------------------
        // 2. Use the high‑level Document API to add a JavaScript link annotation
        // ------------------------------------------------------------
        // Wrap the PDF in a using block for deterministic disposal
        using (Document doc = new Document(outputPdf))
        {
            // Create a rectangle (left, bottom, right, top) for the clickable area
            // Aspose.Pdf.Rectangle is used to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a JavaScript action that will be executed when the link is clicked
            JavascriptAction jsAction = new JavascriptAction("app.alert('Link clicked!');");

            // Create a link annotation on the first page and assign the JavaScript action
            LinkAnnotation link = new LinkAnnotation(doc.Pages[1], linkRect)
            {
                Action = jsAction,
                Color  = Aspose.Pdf.Color.Blue   // Visual border color of the link
            };

            // Add the annotation to the page's annotation collection
            doc.Pages[1].Annotations.Add(link);

            // Save the final PDF with both document‑level actions and the link annotation
            doc.Save(outputPdf);
        }

        Console.WriteLine($"JavaScript actions added and saved to '{outputPdf}'.");
    }
}