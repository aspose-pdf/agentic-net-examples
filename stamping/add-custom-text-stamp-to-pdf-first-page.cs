using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "stamped_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a text stamp with the desired content
            TextStamp textStamp = new TextStamp("Sample Text");

            // Configure font, size and color (using Aspose.Pdf.Color for cross‑platform compatibility)
            textStamp.TextState.Font = FontRepository.FindFont("Arial"); // replace with any installed font name or use a custom TTF via FontRepository.AddFontFile
            textStamp.TextState.FontSize = 24; // custom size
            textStamp.TextState.ForegroundColor = Aspose.Pdf.Color.Blue; // blue color

            // Optional positioning – center of the page
            textStamp.HorizontalAlignment = HorizontalAlignment.Center;
            textStamp.VerticalAlignment   = VerticalAlignment.Center;

            // Apply the stamp only to the first page (pages are 1‑based)
            Page firstPage = doc.Pages[1];
            firstPage.AddStamp(textStamp);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text stamp added and saved to '{outputPath}'.");
    }
}