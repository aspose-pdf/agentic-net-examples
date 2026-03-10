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
        const string searchText = "Hello World";
        const string replaceText = "Hi Universe";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a PdfContentEditor and bind it to the loaded document
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(doc);

            // Define the appearance of the replacement text (optional)
            TextState textState = new TextState();

            // Choose a font and ensure it is embedded in the PDF
            Aspose.Pdf.Text.Font font = FontRepository.FindFont("Courier New");
            font.IsEmbedded = true;

            textState.Font = font;
            textState.FontSize = 14;
            // Use the correct enum: FontStyles (plural) for the FontStyle property
            textState.FontStyle = FontStyles.Bold;
            textState.ForegroundColor = Aspose.Pdf.Color.Blue;

            // Replace the target string on all pages (page number 0 means "all pages")
            bool replaced = editor.ReplaceText(searchText, 0, replaceText, textState);

            Console.WriteLine(replaced
                ? $"Replaced '{searchText}' with '{replaceText}'."
                : $"String '{searchText}' not found.");

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
