using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Drawing;   // needed for Border and BorderStyle

class SetPdfMetadataAndAnnotation
{
    static void Main()
    {
        // Input XSL‑FO file and output PDF file paths
        const string xslFoPath = "input.fo";
        const string outputPdfPath = "output.pdf";

        // Verify that the XSL‑FO source exists
        if (!File.Exists(xslFoPath))
        {
            Console.Error.WriteLine($"XSL‑FO file not found: {xslFoPath}");
            return;
        }

        try
        {
            // ------------------------------------------------------------
            // Load the XSL‑FO document into a PDF Document object
            // ------------------------------------------------------------
            Document pdfDoc = new Document(xslFoPath, new XslFoLoadOptions());

            // ------------------------------------------------------------
            // Set PDF metadata using the PdfFileInfo facade
            // ------------------------------------------------------------
            PdfFileInfo pdfInfo = new PdfFileInfo(pdfDoc);
            pdfInfo.Title    = "Sample PDF Title";
            pdfInfo.Author   = "John Doe";
            pdfInfo.Subject  = "Demonstration of metadata setting";
            pdfInfo.Keywords = "Aspose.Pdf, Facades, Metadata";

            // ------------------------------------------------------------
            // Add a text annotation with a title to the first page
            // ------------------------------------------------------------
            PdfAnnotationEditor annotEditor = new PdfAnnotationEditor();
            annotEditor.BindPdf(pdfDoc);   // initialize the facade with the document

            // Define the rectangle that bounds the annotation (coordinates in points)
            var rect = new Aspose.Pdf.Rectangle(100, 100, 200, 200);

            // Create the annotation on page 1
            TextAnnotation textAnnot = new TextAnnotation(pdfDoc.Pages[1], rect);
            textAnnot.Title    = "Sample Annotation";
            textAnnot.Contents = "This is a text annotation added via Facades.";
            textAnnot.Color    = Aspose.Pdf.Color.Yellow;

            // Initialize the border after the object is created (border‑initialization rule)
            textAnnot.Border = new Border(textAnnot)
            {
                Style = BorderStyle.Solid,
                Width = 1
            };

            // Add the annotation to the page's annotation collection
            pdfDoc.Pages[1].Annotations.Add(textAnnot);

            // ------------------------------------------------------------
            // Save the modified PDF document
            // ------------------------------------------------------------
            pdfDoc.Save(outputPdfPath);   // document-save rule

            Console.WriteLine($"PDF created successfully: {outputPdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}