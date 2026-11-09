using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "stamped_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF with the Document class (PdfFileEditor does not support stamping)
        Document doc = new Document(inputPath);

        // Create a text stamp with the desired appearance
        TextStamp stamp = new TextStamp("Confidential")
        {
            // Font and style settings
            TextState =
            {
                Font = FontRepository.FindFont("Arial"),
                FontSize = 14,
                FontStyle = FontStyles.Bold,
                ForegroundColor = Color.Red
            },
            // Semi‑transparent background
            Background = true,
            Opacity = 0.5, // 0 = fully transparent, 1 = fully opaque
            // Center the stamp on each page
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        };

        // Apply the stamp to every page in the document
        foreach (Page page in doc.Pages)
        {
            page.AddStamp(stamp);
        }

        // Save the modified PDF
        doc.Save(outputPath);
        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}
