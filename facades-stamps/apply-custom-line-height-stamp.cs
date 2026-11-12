using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // TextState

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "watermarked.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Prepare TextState with desired font, size, color and line spacing (leading)
            TextState textState = new TextState("Arial", 36f)
            {
                ForegroundColor = Aspose.Pdf.Color.Gray
                // Note: TextState does not have a 'Leading' property. Use LineSpacing instead.
            };
            // Set custom line spacing (leading) – 1.5 × font size for equal spacing
            textState.LineSpacing = 54f; // 36 * 1.5

            // Create a concrete TextStamp (the class exists in Aspose.Pdf.Facades)
            TextStamp stamp = new TextStamp(
                "Confidential\nDo Not Distribute\nCompany XYZ",
                textState)
            {
                // Center the stamp on each page
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center,

                // Make the stamp semi‑transparent and keep page content visible
                Opacity   = 0.3,
                Background = false
            };

            // Apply the stamp to every page
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                doc.Pages[i].AddStamp(stamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}
