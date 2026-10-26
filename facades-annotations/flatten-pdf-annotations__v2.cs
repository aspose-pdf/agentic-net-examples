using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "flattened.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPath}");
            return;
        }

        // Load the PDF document
        Document doc = new Document(inputPath);

        // PdfAnnotationEditor does not implement IDisposable, so we instantiate it directly.
        // Bind the document, then flatten annotations which copies their appearance streams
        // to the page content and removes the interactive annotation objects.
        PdfAnnotationEditor editor = new PdfAnnotationEditor();
        editor.BindPdf(doc);
        editor.FlatteningAnnotations(); // correct method name

        // Save the flattened PDF
        doc.Save(outputPath);

        Console.WriteLine($"Annotations flattened and saved to '{outputPath}'.");
    }
}
