using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";          // source PDF
        const string outputPath = "output.pdf";         // result PDF
        const string searchText = "PLACEHOLDER";        // text to replace
        const string replaceText = "NEW VALUE";         // replacement text

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Initialize the content editor and bind it to the loaded document
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(doc);

            // Optional: define visual style for the replaced text (font, size, color)
            TextState textState = new TextState
            {
                Font = FontRepository.FindFont("Arial"),
                FontSize = 12,
                FontStyle = FontStyles.Regular,
                ForegroundColor = Aspose.Pdf.Color.Black
            };

            // Replace all occurrences of the search string throughout the document
            // Using the overload that accepts a TextState preserves layout and applies the style
            editor.ReplaceText(searchText, replaceText, textState);

            // Save the modified PDF (PDF format, no extra SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text replacement completed. Output saved to '{outputPath}'.");
    }
}