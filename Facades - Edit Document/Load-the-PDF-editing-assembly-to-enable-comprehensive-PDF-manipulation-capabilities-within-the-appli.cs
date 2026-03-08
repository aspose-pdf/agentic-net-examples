using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the PDF editing facade and bind the source PDF
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);

        // Example manipulation: replace a specific text string
        editor.ReplaceText("OldText", "NewText");

        // Persist the changes to a new file
        editor.Save(outputPath);

        Console.WriteLine($"Edited PDF saved to '{outputPath}'.");
    }
}