using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Error: PDF file not found – {inputPdf}");
            return;
        }

        try
        {
            // ---------- Read PDF metadata using Facades ----------
            // PdfFileInfo provides access to document properties such as Title, Author, etc.
            PdfFileInfo pdfInfo = new PdfFileInfo(inputPdf);
            Console.WriteLine("PDF Metadata:");
            Console.WriteLine($"  Title   : {pdfInfo.Title}");
            Console.WriteLine($"  Author  : {pdfInfo.Author}");
            Console.WriteLine($"  Subject : {pdfInfo.Subject}");
            Console.WriteLine($"  Keywords: {pdfInfo.Keywords}");
            Console.WriteLine($"  Creator : {pdfInfo.Creator}");
            Console.WriteLine($"  Producer: {pdfInfo.Producer}");
            Console.WriteLine($"  Created : {pdfInfo.CreationDate}");
            Console.WriteLine($"  Modified: {pdfInfo.ModDate}");
            Console.WriteLine($"  Pages   : {pdfInfo.NumberOfPages}");
            Console.WriteLine();

            // ---------- Add a Text Annotation ----------
            // Load the PDF document using the core API (required for annotation manipulation)
            Document pdfDoc = new Document(inputPdf);

            // Define the rectangle where the annotation will appear (coordinates are in points)
            // Here we place it on the first page; adjust values as needed.
            Aspose.Pdf.Rectangle annotRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the TextAnnotation object
            TextAnnotation textAnnot = new TextAnnotation(pdfDoc.Pages[1], annotRect)
            {
                // The visible text inside the annotation popup
                Contents = "This is a sample text annotation.",
                // The title shown in the annotation header
                Title = "Sample Title"
            };

            // Initialize the border using the provided border-initialization rule
            // Border must be set after the annotation object is created.
            textAnnot.Border = new Border(textAnnot)
            {
                Style = BorderStyle.Solid,
                Width = 1
            };

            // Add the annotation to the first page's annotation collection
            pdfDoc.Pages[1].Annotations.Add(textAnnot);

            // ---------- Save the modified PDF ----------
            // Use the document-save rule to persist changes.
            pdfDoc.Save(outputPdf);

            Console.WriteLine($"Annotation added and PDF saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}