using System;
using System.IO;
using Aspose.Pdf.Facades;

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

        // Initialize the Facades editor
        PdfContentEditor editor = new PdfContentEditor();

        // Load the PDF document
        editor.BindPdf(inputPath);

        // Regex pattern for dates in MM/DD/YYYY format
        string pattern = @"(\d{2})/(\d{2})/(\d{4})";

        // Replacement to convert to YYYY-MM-DD
        string replacement = "$3-$1-$2";

        // Replace all matching dates. In the Aspose.Pdf version used, the overload
        // expects (string oldValue, int replaceAll, string newValue). Passing 0 for
        // replaceAll means "replace all occurrences".
        editor.ReplaceText(pattern, 0, replacement);

        // Save the updated PDF
        editor.Save(outputPath);

        Console.WriteLine($"Dates reformatted and saved to '{outputPath}'.");
    }
}
