using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF, text to find, replacement text, and output PDF paths
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string srcText   = "Hello World";
        const string destText  = "Hi Universe";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdf))
            {
                // Create a PdfContentEditor and bind it to the loaded document
                PdfContentEditor editor = new PdfContentEditor();
                editor.BindPdf(doc);

                // Replace text on all pages (page index 0 means all pages)
                // This overload preserves the original font, size, and color
                bool replaced = editor.ReplaceText(srcText, 0, destText);

                if (replaced)
                {
                    Console.WriteLine($"Text \"{srcText}\" was replaced with \"{destText}\".");
                }
                else
                {
                    Console.WriteLine($"Text \"{srcText}\" not found.");
                }

                // Save the modified document
                doc.Save(outputPdf);
            }

            Console.WriteLine($"Modified PDF saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}