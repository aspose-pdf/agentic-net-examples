using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

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

        // Multi‑line disclaimer text (use '\n' for line breaks)
        const string disclaimer = "This document is confidential.\nPlease do not distribute.\n© 2026 MyCompany";

        // Load the PDF (using statement ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Configure text appearance and custom line spacing
            TextState textState = new TextState
            {
                Font        = FontRepository.FindFont("Helvetica"),
                FontSize    = 10,
                ForegroundColor = Color.Gray,
                LineSpacing = 4f   // additional spacing between lines
            };

            // Create a TextStamp with the disclaimer and the configured TextState
            TextStamp textStamp = new TextStamp(disclaimer, textState)
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Bottom,
                BottomMargin        = 20,   // distance from the bottom edge
                Opacity             = 0.7f  // semi‑transparent
            };

            // Apply the stamp to every page in the document
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(textStamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Disclaimer stamp added and saved to '{outputPath}'.");
    }
}