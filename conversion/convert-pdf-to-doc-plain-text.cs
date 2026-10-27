using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.doc";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPath))
            {
                // Set up DocSaveOptions to save as plain‑text DOC.
                // The SaveMode property was removed; specifying the format is sufficient.
                var docOptions = new DocSaveOptions
                {
                    Format = DocSaveOptions.DocFormat.Doc // Save as .doc (plain‑text representation)
                };

                // Save the document as DOC using the specified options
                pdfDoc.Save(outputPath, docOptions);
            }

            Console.WriteLine($"PDF successfully converted to plain‑text DOC at '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
