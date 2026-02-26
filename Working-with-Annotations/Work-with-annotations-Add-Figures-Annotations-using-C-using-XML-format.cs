using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Input PDF, XFDF (XML) file containing annotations, and output PDF paths
        const string inputPdfPath   = "input.pdf";
        const string xfdfPath       = "annotations.xfdf";
        const string outputPdfPath  = "output_with_figure.pdf";

        // Verify that the required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine($"XFDF file not found: {xfdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // -----------------------------------------------------------------
            // 1) Import existing annotations from the XFDF (XML) file.
            // -----------------------------------------------------------------
            PdfAnnotationEditor editor = new PdfAnnotationEditor();
            editor.BindPdf(pdfDoc);                       // Bind the editor to the loaded document
            editor.ImportAnnotationsFromXfdf(xfdfPath);   // Import all annotations defined in the XFDF file

            // -----------------------------------------------------------------
            // 2) Add a Figure‑like annotation.
            //    Aspose.Pdf does not have a dedicated "Figure" annotation type.
            //    A StampAnnotation can be used to represent a figure graphic.
            // -----------------------------------------------------------------
            // Choose the first page (1‑based indexing)
            Page page = pdfDoc.Pages[1];

            // Define the rectangle where the stamp (figure) will appear
            Aspose.Pdf.Rectangle figureRect = new Aspose.Pdf.Rectangle(100, 500, 200, 600);

            // Create the stamp annotation
            StampAnnotation figureStamp = new StampAnnotation(page, figureRect);

            // Set an icon that visually represents a figure; you can choose any available icon
            figureStamp.Icon = StampIcon.NotForPublicRelease; // example icon

            // Optional: set a descriptive text
            figureStamp.Contents = "Figure 1: Sample diagram";

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(figureStamp);

            // -----------------------------------------------------------------
            // 3) Save the modified document.
            // -----------------------------------------------------------------
            // The PdfAnnotationEditor saves the document it is bound to.
            editor.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with imported XFDF annotations and a figure stamp: {outputPdfPath}");
    }
}