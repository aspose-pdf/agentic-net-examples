using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the original and the modified PDF files.
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_with_js.pdf";

        // Verify that the source PDF exists.
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdf}");
            return;
        }

        // -----------------------------------------------------------------
        // Step 1: Embed JavaScript actions into the PDF using PdfContentEditor.
        // -----------------------------------------------------------------
        PdfContentEditor editor = new PdfContentEditor();

        // Bind the existing PDF file.
        editor.BindPdf(inputPdf);

        // Add a document‑level JavaScript that runs when the document is opened.
        editor.AddDocumentAdditionalAction(PdfContentEditor.DocumentOpen,
            "app.alert('Document opened via JavaScript action.');");

        // Add a document‑level JavaScript that runs when the document is closed.
        editor.AddDocumentAdditionalAction(PdfContentEditor.DocumentClose,
            "app.alert('Document closed via JavaScript action.');");

        // Save the modified PDF.
        editor.Save(outputPdf);
        Console.WriteLine($"JavaScript actions added and saved to '{outputPdf}'.");

        // -----------------------------------------------------------------
        // Step 2: Load the modified PDF to demonstrate that it can be opened.
        // -----------------------------------------------------------------
        using (Document doc = new Document(outputPdf))
        {
            Console.WriteLine($"Loaded modified PDF. Page count: {doc.Pages.Count}");
            // Additional processing can be performed here if needed.
        }
    }
}