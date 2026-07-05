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
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Initialize the content editor facade
                PdfContentEditor editor = new PdfContentEditor();
                editor.BindPdf(doc);

                // Replace all occurrences of "Draft" with "Final" on all pages (0 = all pages)
                editor.ReplaceText("Draft", 0, "Final");

                // Save the modified document
                doc.Save(outputPath);

                // Release editor resources
                editor.Close();
            }

            Console.WriteLine($"All occurrences of \"Draft\" replaced with \"Final\". Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}