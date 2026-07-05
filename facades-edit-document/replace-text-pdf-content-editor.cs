using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // Needed for TextFragment

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Ensure the input PDF exists. If it does not, create a minimal PDF containing the text "Hello".
        if (!File.Exists(inputPath))
        {
            using (Document doc = new Document())
            {
                Page page = doc.Pages.Add();
                // Add a simple text paragraph so that the ReplaceText example has something to work on.
                page.Paragraphs.Add(new TextFragment("Hello World!"));
                doc.Save(inputPath);
            }
        }

        // PdfContentEditor implements IDisposable, so a using block ensures it is closed/disposed.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the PDF to be edited.
            editor.BindPdf(inputPath);

            // Example edit: replace all occurrences of "Hello" with "Hi".
            editor.ReplaceText("Hello", "Hi");

            // Persist the changes to a new file.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Edited PDF saved to '{outputPath}'.");
    }
}