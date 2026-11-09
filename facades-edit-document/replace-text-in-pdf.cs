using System;
using System.IO;
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
            // Initialize the Facades editor (does not implement IDisposable)
            PdfContentEditor editor = new PdfContentEditor();

            // Load the PDF document
            editor.BindPdf(inputPath);

            // Replace every occurrence of the word "Draft" with "Final"
            editor.ReplaceText("Draft", "Final");

            // Save the modified PDF
            editor.Save(outputPath);

            Console.WriteLine($"All occurrences replaced. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}