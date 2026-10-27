using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF using the Facades API
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(inputPath);

            // Replace every occurrence of the word "Draft" with "Final"
            editor.ReplaceText("Draft", "Final");

            // Save the modified document
            editor.Save(outputPath);

            Console.WriteLine($"All occurrences replaced. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}