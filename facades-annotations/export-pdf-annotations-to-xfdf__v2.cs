using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Initialize the annotation editor and bind the loaded document
            PdfAnnotationEditor editor = new PdfAnnotationEditor();
            editor.BindPdf(doc);

            // Create a memory stream to receive the XFDF data
            using (MemoryStream xfdfStream = new MemoryStream())
            {
                // Export annotations from pages 1 to 2 (inclusive) to the stream.
                // Passing null for the annotation types array exports all annotation types.
                editor.ExportAnnotationsXfdf(xfdfStream, 1, 2, (string[])null);

                // Reset the stream position to the beginning for any further processing
                xfdfStream.Position = 0;

                // Example: read the XFDF content as a string (optional)
                using (StreamReader reader = new StreamReader(xfdfStream))
                {
                    string xfdfContent = reader.ReadToEnd();
                    Console.WriteLine("Exported XFDF:");
                    Console.WriteLine(xfdfContent);
                }
            }

            // No need to save the document because we only exported annotations
        }
    }
}