using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        Document pdfDoc = new Document(inputPdf);

        // Ensure the document has at least one page
        if (pdfDoc.Pages.Count == 0)
        {
            Console.Error.WriteLine("The PDF contains no pages.");
            return;
        }

        // Define the rectangle that will host the sticky‑note annotation
        // (lower‑left X, lower‑left Y, upper‑right X, upper‑right Y)
        Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 120, 720);

        // Instantiate TextAnnotation using the (Page, Rectangle) constructor
        TextAnnotation annotation = new TextAnnotation(pdfDoc.Pages[1], rect)
        {
            Subject  = "ReviewNotes",               // Subject for searching/categorization
            Title    = "Reviewer",                  // Optional author name
            Contents = "Please check this section.",// Optional visible comment text
            Color    = Aspose.Pdf.Color.Yellow      // Optional annotation colour
        };

        // Add the annotation to the first page
        pdfDoc.Pages[1].Annotations.Add(annotation);

        // Save the modified PDF
        pdfDoc.Save(outputPdf);

        Console.WriteLine($"Annotation with subject assigned and saved to '{outputPdf}'.");
    }
}