using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPathDefault = "output_default.pdf";
        const string outputPathWithDoc = "output_with_doc.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // -------------------------------------------------
            // Example 1: Use the parameterless constructor
            // -------------------------------------------------
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                // Bind the loaded document to the editor
                editor.BindPdf(doc);

                // Optional: replace a sample text
                editor.ReplaceText("Hello", "Hi");

                // Save the edited PDF
                editor.Save(outputPathDefault);
            }

            // -------------------------------------------------
            // Example 2: Use the constructor that accepts a Document
            // -------------------------------------------------
            using (PdfContentEditor editorWithDoc = new PdfContentEditor(doc))
            {
                // Optional: replace another sample text
                editorWithDoc.ReplaceText("World", "Earth");

                // Save the edited PDF
                editorWithDoc.Save(outputPathWithDoc);
            }
        }

        Console.WriteLine("PdfContentEditor examples completed.");
    }
}