using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;          // required for FontRepository and TextState

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_text_stamp.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TextStamp with the desired text
            TextStamp textStamp = new TextStamp("Dynamic Size Text")
            {
                // Position the stamp in the middle of the page
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center,

                // Define the stamp rectangle (width & height in points)
                Width  = 300,
                Height = 100
            };

            // TextStamp.TextState is read‑only – modify the existing instance instead of assigning a new one
            textStamp.TextState.Font = FontRepository.FindFont("Arial");
            // Setting FontSize to 0 tells Aspose.Pdf to calculate the maximum size that fits the rectangle
            textStamp.TextState.FontSize = 0;
            // If the used Aspose.Pdf version supports FontSizeAutoFit, enable it; otherwise the FontSize = 0 behaviour is sufficient
            // textStamp.TextState.FontSizeAutoFit = true; // uncomment if the property exists in your version
            textStamp.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

            // Apply the stamp to every page (AddStamp is a Page method, not a collection method)
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(textStamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text stamp added and saved to '{outputPath}'.");
    }
}
