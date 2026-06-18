using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class ReplaceDraftWithFinal
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Create a PdfContentEditor and bind it to the document
                using (PdfContentEditor editor = new PdfContentEditor())
                {
                    editor.BindPdf(doc);

                    // Replace every occurrence of "Draft" with "Final" on all pages (page = 0)
                    editor.ReplaceText("Draft", 0, "Final");
                }

                // Save the modified document
                doc.Save(outputPath);
            }

            Console.WriteLine($"All occurrences of \"Draft\" have been replaced with \"Final\" and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}