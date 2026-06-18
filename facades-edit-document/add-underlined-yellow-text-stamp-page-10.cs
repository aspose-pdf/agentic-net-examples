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
        const string stampText = "CONFIDENTIAL";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Verify that page 10 exists (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 10)
            {
                Console.Error.WriteLine("The document has fewer than 10 pages.");
                return;
            }

            // Create a text stamp with the desired value
            TextStamp textStamp = new TextStamp(stampText);

            // Underline decoration
            textStamp.TextState.Underline = true;

            // Yellow background behind the text
            textStamp.TextState.BackgroundColor = Aspose.Pdf.Color.Yellow;

            // Center the stamp horizontally on the page
            textStamp.HorizontalAlignment = Aspose.Pdf.HorizontalAlignment.Center;

            // Optionally center vertically as well
            textStamp.VerticalAlignment = Aspose.Pdf.VerticalAlignment.Center;

            // Add the stamp to page 10
            doc.Pages[10].AddStamp(textStamp);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text stamp added to page 10 and saved as '{outputPath}'.");
    }
}