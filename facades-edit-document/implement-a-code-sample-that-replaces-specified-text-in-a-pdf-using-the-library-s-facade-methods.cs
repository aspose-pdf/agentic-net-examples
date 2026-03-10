using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string srcText    = "hello world";
        const string destText   = "hi world";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Initialize the facade and bind the loaded document
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(doc);

            // Define the visual appearance of the replacement text (optional)
            TextState textState = new TextState
            {
                Font = FontRepository.FindFont("Courier New"),
                FontSize = 12,
                FontStyle = FontStyles.Bold,
                ForegroundColor = Aspose.Pdf.Color.Red
            };

            // Replace the specified text on all pages (page number 0 means all pages)
            editor.ReplaceText(srcText, 0, destText, textState);

            // Save the modified PDF (PDF format does not require explicit SaveOptions)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text replacement completed. Output saved to '{outputPath}'.");
    }
}
