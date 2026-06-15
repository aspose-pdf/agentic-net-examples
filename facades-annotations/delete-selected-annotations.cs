using System;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

namespace DeleteAnnotationsDemo
{
    class Program
    {
        static void Main()
        {
            // Create a sample PDF that contains several different annotations
            CreateSamplePdf();

            // Simulate user selection of annotation types to delete (max 4 elements as per evaluation mode)
            List<string> annotationTypesToDelete = new List<string>();
            annotationTypesToDelete.Add("Text");
            annotationTypesToDelete.Add("Highlight");
            annotationTypesToDelete.Add("Underline");
            annotationTypesToDelete.Add("Square");

            // Delete selected annotation types using PdfAnnotationEditor
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf("input.pdf");

                foreach (string annotType in annotationTypesToDelete)
                {
                    editor.DeleteAnnotations(annotType);
                }

                // Save a preview PDF so the user can see the result before the final save
                editor.Save("preview.pdf");

                // Save the final PDF
                editor.Save("output.pdf");
            }
        }

        private static void CreateSamplePdf()
        {
            using (Document document = new Document())
            {
                Page page = document.Pages.Add();

                // Text annotation
                TextAnnotation textAnnotation = new TextAnnotation(page, new Aspose.Pdf.Rectangle(100, 700, 200, 650));
                textAnnotation.Contents = "Sample Text Annotation";
                page.Annotations.Add(textAnnotation);

                // Highlight annotation
                HighlightAnnotation highlightAnnotation = new HighlightAnnotation(page, new Aspose.Pdf.Rectangle(100, 600, 200, 550));
                highlightAnnotation.QuadPoints = new Aspose.Pdf.Point[]
                {
                    new Aspose.Pdf.Point(100, 600),
                    new Aspose.Pdf.Point(200, 600),
                    new Aspose.Pdf.Point(200, 550),
                    new Aspose.Pdf.Point(100, 550)
                };
                page.Annotations.Add(highlightAnnotation);

                // Underline annotation
                UnderlineAnnotation underlineAnnotation = new UnderlineAnnotation(page, new Aspose.Pdf.Rectangle(100, 500, 200, 450));
                underlineAnnotation.QuadPoints = new Aspose.Pdf.Point[]
                {
                    new Aspose.Pdf.Point(100, 500),
                    new Aspose.Pdf.Point(200, 500),
                    new Aspose.Pdf.Point(200, 450),
                    new Aspose.Pdf.Point(100, 450)
                };
                page.Annotations.Add(underlineAnnotation);

                // Square annotation
                SquareAnnotation squareAnnotation = new SquareAnnotation(page, new Aspose.Pdf.Rectangle(100, 400, 200, 350));
                page.Annotations.Add(squareAnnotation);

                document.Save("input.pdf");
            }
        }
    }
}
