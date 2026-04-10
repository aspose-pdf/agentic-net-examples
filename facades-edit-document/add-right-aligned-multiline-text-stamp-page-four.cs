using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // Added for FontRepository

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF using the Facades API
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.BindPdf(inputPath);

        // Access the underlying Document to work with individual pages
        Document doc = fileStamp.Document;

        // Ensure the document has at least four pages
        if (doc.Pages.Count < 4)
        {
            Console.Error.WriteLine("The document does not contain a fourth page.");
            fileStamp.Close();
            return;
        }

        // Create a multiline text stamp
        string multilineText = "First line\r\nSecond line\r\nThird line";
        TextStamp textStamp = new TextStamp(multilineText);

        // Align the stamp to the right and place it at the bottom of the page
        textStamp.HorizontalAlignment = HorizontalAlignment.Right;   // right‑aligned
        textStamp.VerticalAlignment   = VerticalAlignment.Bottom;   // bottom of the page
        textStamp.BottomMargin = 10;   // distance from the bottom edge
        textStamp.RightMargin  = 10;   // distance from the right edge

        // Optionally set text appearance (font, size, color)
        textStamp.TextState.Font = FontRepository.FindFont("Helvetica");
        textStamp.TextState.FontSize = 12;
        textStamp.TextState.ForegroundColor = Color.Gray;

        // Apply the stamp only to page 4
        Page targetPage = doc.Pages[4];
        targetPage.AddStamp(textStamp);

        // Save the modified PDF
        fileStamp.Save(outputPath);
        fileStamp.Close();

        Console.WriteLine($"Text stamp added to page 4 and saved as '{outputPath}'.");
    }
}
