using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "stamped_output.pdf";
        const string author = "John Doe";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document using the Document class (PdfFileEditor has no BindPdf method).
            Document pdfDoc = new Document(inputPath);

            // Create a text stamp with dynamic content using string interpolation.
            string stampText = $"Created on {DateTime.Now:yyyy-MM-dd HH:mm} by {author}";
            TextStamp textStamp = new TextStamp(stampText)
            {
                // Position the stamp – use XIndent/YIndent instead of non‑existent X/Y properties.
                XIndent = 100,
                YIndent = 100,
                // All visual formatting is done through the TextState object.
                TextState =
                {
                    Font = FontRepository.FindFont("Arial"),
                    FontSize = 12,
                    ForegroundColor = Color.Black
                }
            };

            // Apply the stamp to every page – Document.Pages does not have an AddStamp method.
            foreach (Page page in pdfDoc.Pages)
            {
                page.AddStamp(textStamp);
            }

            // Save the modified document.
            pdfDoc.Save(outputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
