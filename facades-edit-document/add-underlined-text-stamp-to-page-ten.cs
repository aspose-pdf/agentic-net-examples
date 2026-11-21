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

        // Load the PDF document using the high‑level Document API
        Document pdf = new Document(inputPath);

        // Verify that page 10 exists (pages are 1‑based)
        if (pdf.Pages.Count < 10)
        {
            Console.Error.WriteLine("The document does not contain page 10.");
            return;
        }

        // Create a TextStamp with underline, yellow background and centered alignment
        TextStamp stamp = new TextStamp("Your Text Here")
        {
            Background = true,
            HorizontalAlignment = HorizontalAlignment.Center
        };
        stamp.TextState.FontSize = 12f;
        stamp.TextState.FontStyle = FontStyles.Bold;
        stamp.TextState.Underline = true;
        stamp.TextState.ForegroundColor = Color.Black;
        stamp.TextState.BackgroundColor = Color.Yellow;

        // Apply the stamp to page 10
        pdf.Pages[10].AddStamp(stamp);

        // Save the modified PDF
        pdf.Save(outputPath);

        Console.WriteLine($"Text stamp added to page 10 and saved as '{outputPath}'.");
    }
}