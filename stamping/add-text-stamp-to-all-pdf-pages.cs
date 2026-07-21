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

        using (Document doc = new Document(inputPath))
        {
            // Create a text stamp that will be applied to every page
            TextStamp stamp = new TextStamp("CONFIDENTIAL")
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center,
                Opacity = 0.5,
                Background = true
            };

            // TextState is read‑only; modify its members instead of assigning a new instance
            stamp.TextState.FontSize = 48;
            stamp.TextState.Font = FontRepository.FindFont("Helvetica");
            stamp.TextState.ForegroundColor = Color.Red;

            // Apply the stamp to each page individually
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(stamp);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}
