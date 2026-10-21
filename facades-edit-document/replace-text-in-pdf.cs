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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // PdfContentEditor is a Facades class for page-level operations.
            // It does NOT implement IDisposable, so we do NOT wrap it in a using block.
            PdfContentEditor editor = new PdfContentEditor();

            // Bind the source PDF file.
            editor.BindPdf(inputPath);

            // Replace every occurrence of the word "Draft" with "Final".
            // This method works on the entire document content.
            editor.ReplaceText("Draft", "Final");

            // Save the modified PDF to the desired output path.
            editor.Save(outputPath);

            // Release any internal resources held by the editor.
            editor.Close();

            Console.WriteLine($"Text replacement completed. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during processing: {ex.Message}");
        }
    }
}