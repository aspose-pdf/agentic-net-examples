using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "redacted_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create the annotation editor and bind the document
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(doc);

                // ---------- Page 2 redaction ----------
                // Define rectangle area (llx, lly, urx, ury)
                Aspose.Pdf.Rectangle rectPage2 = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
                // Create redaction annotation for page 2
                RedactionAnnotation redaction2 = new RedactionAnnotation(doc.Pages[2], rectPage2);
                // Set fill color (use Aspose.Pdf.Color, not System.Drawing.Color)
                redaction2.FillColor = Aspose.Pdf.Color.Yellow;
                // Apply the annotation to page 2 (startPage = endPage = 2)
                editor.ModifyAnnotations(2, 2, redaction2);

                // ---------- Page 3 redaction ----------
                Aspose.Pdf.Rectangle rectPage3 = new Aspose.Pdf.Rectangle(150, 400, 350, 450);
                RedactionAnnotation redaction3 = new RedactionAnnotation(doc.Pages[3], rectPage3);
                redaction3.FillColor = Aspose.Pdf.Color.LightGray;
                editor.ModifyAnnotations(3, 3, redaction3);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Redacted PDF saved to '{outputPath}'.");
    }
}