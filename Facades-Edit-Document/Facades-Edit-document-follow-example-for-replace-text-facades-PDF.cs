using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Paths for the source PDF and the resulting PDF
        string inputPath = "input.pdf";
        string outputPath = "output.pdf";

        // Ensure the source file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Create a PdfContentEditor instance and bind it to the source PDF
            var editor = new PdfContentEditor();
            editor.BindPdf(inputPath);

            // Perform the text replacement on all pages (startPage = 1)
            editor.ReplaceText("old text", "new text", 1);

            // Save the edited PDF to the specified output path
            editor.Save(outputPath);

            Console.WriteLine($"Text replacement completed successfully. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
