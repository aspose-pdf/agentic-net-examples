using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_header.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a HeaderFooter instance for the first page
            HeaderFooter header = new HeaderFooter();

            // Configure margin information for the header
            // (MarginInfo can be customized; here we use default margins)
            header.Margin = new MarginInfo();

            // Add a text paragraph to the header
            // TextFragment resides in Aspose.Pdf.Text namespace
            TextFragment tf = new TextFragment("Document Header – Page 1");
            tf.TextState.Font = FontRepository.FindFont("Helvetica");
            tf.TextState.FontSize = 12;
            tf.TextState.ForegroundColor = Aspose.Pdf.Color.DarkGray;
            header.Paragraphs.Add(tf);

            // Assign the header to the first page (pages are 1‑based)
            doc.Pages[1].Header = header;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Header added and saved to '{outputPath}'.");
    }
}