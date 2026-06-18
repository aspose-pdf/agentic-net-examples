using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing; // HorizontalAlignment, VerticalAlignment

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string stampText = "CONFIDENTIAL";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document pdfDocument = new Document(inputPath);

        // Ensure the document has at least 5 pages
        if (pdfDocument.Pages.Count < 5)
        {
            Console.Error.WriteLine("The PDF does not contain a page 5.");
            return;
        }

        // Create a TextStamp with bold Helvetica, red color, size 24, centered
        TextStamp textStamp = new TextStamp(stampText);
        textStamp.TextState.Font = FontRepository.FindFont("Helvetica");
        textStamp.TextState.FontSize = 24;
        textStamp.TextState.FontStyle = FontStyles.Bold;
        textStamp.TextState.ForegroundColor = Aspose.Pdf.Color.Red; // fully‑qualified Aspose color
        textStamp.HorizontalAlignment = HorizontalAlignment.Center;
        textStamp.VerticalAlignment = VerticalAlignment.Center;

        // Add the stamp to page 5 (1‑based index)
        pdfDocument.Pages[5].AddStamp(textStamp);

        // Save the modified PDF
        pdfDocument.Save(outputPath);

        Console.WriteLine($"Text stamp applied to page 5 and saved as '{outputPath}'.");
    }
}
