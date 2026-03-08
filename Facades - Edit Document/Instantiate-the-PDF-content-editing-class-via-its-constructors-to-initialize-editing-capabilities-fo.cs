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

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: wrap Document in using)
        using (Document doc = new Document(inputPath))
        {
            // Instantiate PdfContentEditor with the loaded document (constructor overload)
            using (PdfContentEditor editor = new PdfContentEditor(doc))
            {
                // Example editing operation: replace all occurrences of "Hello" with "Hi"
                editor.ReplaceText("Hello", "Hi");

                // Save the edited PDF (PdfContentEditor inherits SaveableFacade)
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"Edited PDF saved to '{outputPath}'.");
    }
}