using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for input and output PDFs
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        // Load the existing PDF document
        Document pdfDocument = new Document(inputPath);

        // -----------------------------------------------------------------
        // Modify accessibility‑related metadata
        // -----------------------------------------------------------------
        pdfDocument.Info.Title   = "Accessible PDF Example";
        pdfDocument.Info.Author  = "Jane Doe";
        pdfDocument.Info.Subject = "Demonstration of PDF accessibility properties";
        pdfDocument.Info.Keywords = "accessibility, pdf, Aspose";

        // Enable PDF tagging if the property is available in the current library version
        var taggedProp = typeof(Document).GetProperty("Tagged");
        if (taggedProp != null && taggedProp.CanWrite)
        {
            taggedProp.SetValue(pdfDocument, true);
        }

        // -----------------------------------------------------------------
        // Save the modified PDF (uses the prescribed document-save rule)
        // -----------------------------------------------------------------
        pdfDocument.Save(outputPath);
    }
}