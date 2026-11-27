using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Bind the document to PdfAnnotationEditor
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(doc);

                // Define the annotation rectangle (llx, lly, urx, ury)
                var rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

                // Create a TextAnnotation for page 4
                var txtAnn = new TextAnnotation(doc.Pages[4], rect)
                {
                    Title    = "Note",
                    Contents = "This is a text annotation on page 4.",
                    Open     = true,
                    Icon     = TextIcon.Note
                };

                // Add the annotation – ModifyAnnotations works for adding when startPage == endPage
                editor.ModifyAnnotations(4, 4, txtAnn);
            }

            // Update the document's Modified date
            doc.Info.ModDate = DateTime.Now;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with annotation and updated Modified date: {outputPath}");
    }
}
