using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string stampText  = "CONFIDENTIAL";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document pdfDoc = new Document(inputPath);

        // Create a TextStamp with the desired text
        TextStamp textStamp = new TextStamp(stampText);

        // Underline the text
        textStamp.TextState.Underline = true;

        // Set a yellow background for the text
        textStamp.TextState.BackgroundColor = Aspose.Pdf.Color.Yellow;

        // Center the stamp horizontally and vertically on the page
        textStamp.HorizontalAlignment = HorizontalAlignment.Center;
        textStamp.VerticalAlignment   = VerticalAlignment.Center;

        // Add the stamp to page 10 (pages are 1‑based)
        if (pdfDoc.Pages.Count >= 10)
        {
            pdfDoc.Pages[10].AddStamp(textStamp);
        }
        else
        {
            Console.Error.WriteLine("The document has fewer than 10 pages.");
            return;
        }

        // Save the result
        pdfDoc.Save(outputPath);

        Console.WriteLine($"Text stamp added to page 10 and saved as '{outputPath}'.");
    }
}
