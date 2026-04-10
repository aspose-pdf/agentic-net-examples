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

        // Load the PDF document
        Document pdfDocument = new Document(inputPath);

        // Verify that page 10 exists
        if (pdfDocument.Pages.Count < 10)
        {
            Console.Error.WriteLine("The document does not contain a page 10.");
            return;
        }

        // Configure the text appearance: underline, yellow background, font, size
        TextState textState = new TextState
        {
            Font = FontRepository.FindFont("Helvetica"),
            FontSize = 24,
            ForegroundColor = Aspose.Pdf.Color.Black,
            BackgroundColor = Aspose.Pdf.Color.Yellow,
            Underline = true
        };

        // Create the text stamp with the configured TextState
        TextStamp textStamp = new TextStamp("Underlined Text", textState)
        {
            // Center the stamp horizontally and vertically on the page
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            // Ensure the stamp is drawn on top of page content
            Background = false
        };

        // Add the stamp only to page 10
        Page pageTen = pdfDocument.Pages[10];
        pageTen.AddStamp(textStamp);

        // Save the modified PDF
        pdfDocument.Save(outputPath);

        Console.WriteLine($"Text stamp added and saved to '{outputPath}'.");
    }
}
