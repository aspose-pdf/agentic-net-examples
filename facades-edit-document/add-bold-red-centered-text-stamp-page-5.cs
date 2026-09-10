using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;      // Facades namespace is referenced as required
using Aspose.Pdf.Text;        // Required for TextState and FontRepository

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

        // Load the PDF document (core API) – disposal is handled by the using block
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least five pages
            if (doc.Pages.Count < 5)
            {
                Console.Error.WriteLine("The document does not contain a page 5.");
                return;
            }

            // Create a TextStamp with the desired text
            TextStamp textStamp = new TextStamp("CONFIDENTIAL");

            // Configure the stamp's appearance
            // Bold font (Helvetica-Bold), red color, font size 24
            textStamp.TextState.Font = FontRepository.FindFont("Helvetica-Bold");
            textStamp.TextState.FontSize = 24;
            textStamp.TextState.ForegroundColor = Aspose.Pdf.Color.Red;

            // Center the stamp horizontally and vertically on the page
            textStamp.HorizontalAlignment = HorizontalAlignment.Center;
            textStamp.VerticalAlignment   = VerticalAlignment.Center;

            // Add the stamp to page 5 only
            Page pageFive = doc.Pages[5];
            pageFive.AddStamp(textStamp);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text stamp applied to page 5 and saved as '{outputPath}'.");
    }
}