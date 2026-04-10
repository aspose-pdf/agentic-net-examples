using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Facades; // Facades namespace as required

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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Prepare text appearance: bold Helvetica, red color
            TextState textState = new TextState
            {
                Font = FontRepository.FindFont("Helvetica"),
                FontSize = 24,
                FontStyle = FontStyles.Bold,
                ForegroundColor = Aspose.Pdf.Color.Red
            };

            // Create a TextStamp with the desired text and appearance
            TextStamp textStamp = new TextStamp(stampText, textState)
            {
                // Center the stamp horizontally on the page
                HorizontalAlignment = HorizontalAlignment.Center,
                // Optional: place the stamp near the top of the page
                // You can adjust YIndent or TopMargin as needed
                TopMargin = 50
            };

            // Add the stamp to page 5 (Aspose.Pdf uses 1‑based indexing)
            doc.Pages[5].AddStamp(textStamp);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text stamp applied and saved to '{outputPath}'.");
    }
}