using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.doc";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPath))
            {
                // Specify DOC save options; this ensures the output is a Word document
                DocSaveOptions docOptions = new DocSaveOptions {
                    Format = DocSaveOptions.DocFormat.Doc
                };

                // Save as DOC using the options
                pdfDoc.Save(outputPath, docOptions);
            }

            Console.WriteLine($"PDF successfully saved as DOC to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}