using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Annotations; // Added for TextStamp and alignment enums

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "stamped_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Define text appearance: Arial, size 14, blue color
        TextState textState = new TextState
        {
            Font = FontRepository.FindFont("Arial"),
            FontSize = 14,
            ForegroundColor = Aspose.Pdf.Color.Blue
        };

        // Multiline content (use newline characters)
        string multilineText = "First line\nSecond line\nThird line";

        // Create a TextStamp with the multiline text and the defined TextState
        TextStamp stamp = new TextStamp(multilineText, textState)
        {
            // Optional: center the stamp on the page
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        };

        // Load the PDF, add the stamp to each page, and save the result
        Document pdfDocument = new Document(inputPath);
        foreach (Page page in pdfDocument.Pages)
        {
            page.AddStamp(stamp);
        }
        pdfDocument.Save(outputPath);

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}
