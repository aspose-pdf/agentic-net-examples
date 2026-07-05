using System;
using System.IO;
using Aspose.Pdf;               // Core API (Document, Page, TextStamp)

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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TextStamp with the desired label
            TextStamp textStamp = new TextStamp(stampText)
            {
                // Set opacity to 0.6 for a subtle appearance
                Opacity = 0.6,

                // Optional: place the stamp diagonally across the page
                // RotateAngle = -45,          // uncomment if rotation is desired
                // HorizontalAlignment = HorizontalAlignment.Center,
                // VerticalAlignment   = VerticalAlignment.Center,

                // Ensure the stamp is drawn as text (not as graphic operators)
                Draw = false
            };

            // Apply the stamp to every page in the document
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