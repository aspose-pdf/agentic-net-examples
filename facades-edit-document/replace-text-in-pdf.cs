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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a PdfContentEditor facade and bind it to the loaded document
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(doc);

            // Replace every occurrence of the word "Draft" with "Final" in the whole document
            editor.ReplaceText("Draft", "Final");

            // Save the modified PDF
            doc.Save(outputPath);

            // Close the editor (optional, but releases any internal resources)
            editor.Close();
        }

        Console.WriteLine($"All occurrences of 'Draft' have been replaced with 'Final'. Saved to '{outputPath}'.");
    }
}