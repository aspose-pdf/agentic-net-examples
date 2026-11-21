using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_footnote.pdf";
        const string imagePath = "reference.png";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image not found: {imagePath}");
            return;
        }

        // Load PDF with deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Add some body text to which the footnote will be attached
            TextFragment body = new TextFragment("Sample body text with a footnote reference.");
            page.Paragraphs.Add(body);

            // Create a footnote (Note) and attach it to the body TextFragment
            Note footnote = new Note();
            body.FootNote = footnote;

            // Add descriptive text to the footnote
            footnote.Paragraphs.Add(new TextFragment("Figure 1: "));

            // Load image and add it to the footnote's Paragraphs collection
            FileStream imgStream = File.OpenRead(imagePath);
            Image img = new Image
            {
                ImageStream = imgStream,
                // Optional sizing – adjust as needed
                FixWidth = 100,
                FixHeight = 100,
                // Alignment inside the footnote paragraph (centered)
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            footnote.Paragraphs.Add(img);

            // Save the modified PDF
            doc.Save(outputPath);

            // Close the image stream after saving
            imgStream.Dispose();
        }

        Console.WriteLine($"PDF saved with footnote: {outputPath}");
    }
}
