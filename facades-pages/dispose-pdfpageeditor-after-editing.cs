using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

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

        // Load the PDF document. The using block ensures the Document is disposed properly.
        using (Document doc = new Document(inputPath))
        {
            // PdfPageEditor implements IDisposable, so wrap it in a using block.
            // This guarantees that Close() (or Dispose()) is called, releasing file handles.
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                // Bind the loaded document to the editor.
                editor.BindPdf(doc);

                // Example modifications:
                // Rotate all pages by 90 degrees.
                editor.Rotation = 90;
                // Set zoom factor to 150%.
                editor.Zoom = 1.5f;

                // Apply the changes to the document.
                editor.ApplyChanges();

                // Save the edited PDF. Save(string) writes a PDF regardless of extension.
                editor.Save(outputPath);
            } // editor is automatically closed/disposed here.
        } // doc is automatically closed/disposed here.

        Console.WriteLine($"Edited PDF saved to '{outputPath}'.");
    }
}