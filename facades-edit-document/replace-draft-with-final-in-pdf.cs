using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle rule: use using for disposal)
            using (Document doc = new Document(inputPath))
            {
                // Create a PdfContentEditor to edit the document content
                using (PdfContentEditor editor = new PdfContentEditor())
                {
                    // Bind the loaded document to the editor
                    editor.BindPdf(doc);

                    // Replace every occurrence of "Draft" with "Final" in the whole document
                    editor.ReplaceText("Draft", "Final");
                }

                // Save the modified document (lifecycle rule: use Save without extra options)
                doc.Save(outputPath);
            }

            Console.WriteLine($"All occurrences of \"Draft\" replaced with \"Final\". Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}