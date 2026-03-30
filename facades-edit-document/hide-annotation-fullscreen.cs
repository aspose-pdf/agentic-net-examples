using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        // Create a new PDF document with a single page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Define rectangle for the annotation (left, bottom, right, top)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a text annotation and mark it hidden
            TextAnnotation annotation = new TextAnnotation(page, rect);
            annotation.Title = "Hidden Note";
            annotation.Contents = "This annotation is hidden in full-screen mode.";
            annotation.Flags = AnnotationFlags.Hidden;

            // Add the annotation to the page
            page.Annotations.Add(annotation);

            // Save the document
            doc.Save(outputPath);
        }

        // Change viewer preference to FullScreen
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(outputPath);
            editor.ChangeViewerPreference(ViewerPreference.PageModeFullScreen);
            editor.Save(outputPath);
        }
    }
}
