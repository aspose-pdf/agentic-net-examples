using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_with_js.pdf";

        // Verify that the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document using the recommended using pattern
        using (Document doc = new Document(inputPdf))
        {
            // Create a PdfContentEditor facade to edit PDF content
            PdfContentEditor editor = new PdfContentEditor();

            // Bind the loaded document to the editor (uses the BindPdf(Document) overload)
            editor.BindPdf(doc);

            // Add a JavaScript action that runs when the document is opened
            // PdfContentEditor.DocumentOpen is a constant representing the open event
            editor.AddDocumentAdditionalAction(
                PdfContentEditor.DocumentOpen,
                "app.alert('Document opened via JavaScript action');");

            // Add a JavaScript action that runs when the document is closed
            editor.AddDocumentAdditionalAction(
                PdfContentEditor.DocumentClose,
                "app.alert('Document closed via JavaScript action');");

            // Save the modified PDF. PdfContentEditor.Save(string) writes the result to a file.
            editor.Save(outputPdf);
        }

        Console.WriteLine($"PDF with JavaScript actions saved to '{outputPdf}'.");
    }
}