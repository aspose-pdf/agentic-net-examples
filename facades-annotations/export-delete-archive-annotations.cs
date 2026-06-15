using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

namespace BatchAnnotationExport
{
    class Program
    {
        static void Main(string[] args)
        {
            // Step 1: Create a sample PDF with an annotation
            using (Document sampleDoc = new Document())
            {
                Page page = sampleDoc.Pages.Add();
                // Define the rectangle for the annotation (lower‑left X, lower‑left Y, upper‑right X, upper‑right Y)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 200, 650);
                TextAnnotation textAnnot = new TextAnnotation(page, rect);
                textAnnot.Title = "Sample";
                textAnnot.Contents = "This is a test annotation.";
                page.Annotations.Add(textAnnot);
                sampleDoc.Save("input.pdf");
            }

            // Step 2: Open the PDF and export annotations to XFDF
            using (Document doc = new Document("input.pdf"))
            {
                using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
                {
                    editor.BindPdf(doc);
                    using (FileStream xfdfStream = File.Create("annotations.xfdf"))
                    {
                        editor.ExportAnnotationsToXfdf(xfdfStream);
                    }

                    // Step 3: Delete all annotations
                    editor.DeleteAnnotations();

                    // Save the PDF without annotations
                    editor.Save("output.pdf");
                }
            }

            // Step 4: Archive the XFDF file (copy to an archive name)
            File.Copy("annotations.xfdf", "archive_annotations.xfdf", true);
        }
    }
}
