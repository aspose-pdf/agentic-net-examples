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

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Create a HeaderFooter instance for the header
            HeaderFooter header = new HeaderFooter();

            // Configure the margin using MarginInfo (optional adjustments)
            header.Margin = new MarginInfo();
            // Example: you can set specific margin parts if needed
            // header.Margin.TopMarginIfAny = MarginPartStyle.None;

            // Create a text fragment for the header content
            TextFragment tf = new TextFragment("Sample Header Text");
            tf.TextState.Font = FontRepository.FindFont("Helvetica");
            tf.TextState.FontSize = 12;
            tf.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;

            // Add the text fragment to the header's paragraph collection
            header.Paragraphs.Add(tf);

            // Assign the header to the first page (pages are 1‑based)
            doc.Pages[1].Header = header;

            // Save the modified PDF (lifecycle rule: use Save within using)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Header added and saved to '{outputPath}'.");
    }
}