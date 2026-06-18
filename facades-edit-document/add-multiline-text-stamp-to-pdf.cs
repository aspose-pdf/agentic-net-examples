using System;
using System.IO;
using Aspose.Pdf;
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

        // Multiline text for the stamp
        string stampContent = "First line\nSecond line\nThird line";

        // Configure text appearance: Arial, size 14, blue color
        TextState textState = new TextState
        {
            Font = FontRepository.FindFont("Arial"),
            FontSize = 14,
            ForegroundColor = Color.Blue
        };

        // Create a TextStamp with the multiline content and the defined TextState
        TextStamp textStamp = new TextStamp(stampContent, textState)
        {
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            Opacity = 0.8f
        };

        // Load the PDF and apply the stamp to every page using the Document API
        Document pdfDocument = new Document(inputPath);
        foreach (Page page in pdfDocument.Pages)
        {
            page.AddStamp(textStamp);
        }

        pdfDocument.Save(outputPath);
        Console.WriteLine($"Text stamp applied and saved to '{outputPath}'.");
    }
}
