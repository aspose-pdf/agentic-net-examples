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
        const string message = "Custom Message";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TextStamp that will act as a line‑like annotation displaying the custom message
            TextStamp textStamp = new TextStamp(message);
            textStamp.Background = false;
            textStamp.Opacity = 0.5;
            textStamp.HorizontalAlignment = HorizontalAlignment.Center;
            textStamp.VerticalAlignment = VerticalAlignment.Center;

            // Define the visual appearance of the text (modify the existing TextState instance)
            textStamp.TextState.FontSize = 24;
            textStamp.TextState.FontStyle = FontStyles.Bold;
            textStamp.TextState.ForegroundColor = Color.Red;

            // Apply the stamp to every page in the document
            foreach (Page page in doc.Pages)
            {
                // Make the stamp span the whole width of the current page
                textStamp.Width = page.PageInfo.Width;
                // Add the stamp to the current page
                page.AddStamp(textStamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Line‑like stamp added and saved to '{outputPath}'.");
    }
}
