using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "header_added.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a header/footer object
            HeaderFooter header = new HeaderFooter();

            // Configure margins for the header (values are in points)
            header.Margin = new MarginInfo
            {
                Top    = 20,   // distance from the top edge of the page
                Bottom = 0,
                Left   = 0,
                Right  = 0
            };

            // Create the text fragment that will appear in the header
            TextFragment tf = new TextFragment("Document Header");
            tf.TextState.FontSize = 12;
            tf.TextState.Font = FontRepository.FindFont("Arial");
            tf.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

            // Add the text fragment to the header's paragraph collection
            header.Paragraphs.Add(tf);

            // Assign the header to the first page (pages are 1‑based)
            doc.Pages[1].Header = header;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Header added and saved to '{outputPath}'.");
    }
}