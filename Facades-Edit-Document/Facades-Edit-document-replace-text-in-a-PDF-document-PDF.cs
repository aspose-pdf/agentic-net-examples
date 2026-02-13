using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments: input PDF, text to find, replacement text, output PDF
        if (args.Length < 4)
        {
            Console.WriteLine("Usage: <inputPdf> <searchText> <replaceText> <outputPdf>");
            return;
        }

        string inputPath = args[0];
        string searchText = args[1];
        string replaceText = args[2];
        string outputPath = args[3];

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document (load rule)
            Document pdfDoc = new Document(inputPath);

            // Use the Facades editor to replace text
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                editor.BindPdf(pdfDoc);
                // Replace all occurrences of the specified text
                editor.ReplaceText(searchText, replaceText);
                // Save the edited PDF (save rule)
                editor.Save(outputPath);
            }

            Console.WriteLine($"Text replacement completed. Saved to {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}