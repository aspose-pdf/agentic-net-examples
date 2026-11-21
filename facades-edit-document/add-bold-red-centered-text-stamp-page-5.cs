using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;          // TextStamp resides here
using Aspose.Pdf.Text;            // TextState, FontStyles, FontRepository
using Aspose.Pdf;                 // for Color (cross‑platform)

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string stampText  = "Confidential";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least five pages
            if (doc.Pages.Count < 5)
            {
                Console.Error.WriteLine("The document has fewer than 5 pages.");
                return;
            }

            // Create a TextStamp (Aspose.Pdf.Facades)
            TextStamp textStamp = new TextStamp(stampText)
            {
                // Center the stamp horizontally on the page
                HorizontalAlignment = HorizontalAlignment.Center,
                // Optional vertical positioning (e.g., middle of the page)
                VerticalAlignment   = VerticalAlignment.Center
            };

            // Configure the appearance: bold font, red color
            textStamp.TextState.Font = FontRepository.FindFont("Arial");
            textStamp.TextState.FontSize = 24;
            textStamp.TextState.FontStyle = FontStyles.Bold;
            textStamp.TextState.ForegroundColor = Aspose.Pdf.Color.Red;

            // Apply the stamp to page five (1‑based indexing)
            Page pageFive = doc.Pages[5];
            pageFive.AddStamp(textStamp);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text stamp applied and saved to '{outputPath}'.");
    }
}