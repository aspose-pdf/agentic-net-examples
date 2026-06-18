using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify the source file exists
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfContentEditor implements IDisposable, so a using block ensures it is closed automatically
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the PDF document into the editor
            editor.BindPdf(inputPath);

            // Perform an editing operation – replace all occurrences of "OldText" with "NewText"
            editor.ReplaceText("OldText", "NewText");

            // Save the modified PDF to a new file
            editor.Save(outputPath);
        }

        Console.WriteLine($"Edited PDF saved to '{outputPath}'.");
    }
}