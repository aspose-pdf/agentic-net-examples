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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Initialize the PdfContentEditor facade
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                // Bind the document to the editor
                editor.BindPdf(doc);

                // Prepare font and text state for the replacement
                Aspose.Pdf.Text.Font font = FontRepository.FindFont("Courier New");
                font.IsEmbedded = true;

                TextState textState = new TextState
                {
                    Font = font,
                    FontSize = 17,
                    FontStyle = FontStyles.Bold | FontStyles.Italic,
                    ForegroundColor = Aspose.Pdf.Color.Red
                };

                // Replace "hello world" with "hi world" on page 1 using the specified TextState
                bool replaced = editor.ReplaceText("hello world", 1, "hi world", textState);
                Console.WriteLine(replaced ? "Text replaced successfully." : "Target text not found.");
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}