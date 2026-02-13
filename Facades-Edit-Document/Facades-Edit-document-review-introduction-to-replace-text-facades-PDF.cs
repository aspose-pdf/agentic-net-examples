using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths for source and destination PDFs
        string inputPath = "input.pdf";
        string outputPath = "output.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Create the editor and bind the source PDF
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(inputPath);

            // Perform the text replacement (replace all occurrences of "Hello" with "Hi")
            // Use the overload without TextReplaceOptions – it replaces all matches by default.
            editor.ReplaceText("Hello", "Hi");

            // Save the edited PDF to the output path
            editor.Save(outputPath);
            Console.WriteLine($"Edited PDF saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}
