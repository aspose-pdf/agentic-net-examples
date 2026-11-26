using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "DuplexLongEdge.pdf";

        // Create a new PDF document inside a using block for proper disposal
        using (Document doc = new Document())
        {
            // Add a page
            Page page = doc.Pages.Add();

            // Add simple text to the page
            TextFragment tf = new TextFragment("Sample PDF with duplex setting.");
            page.Paragraphs.Add(tf);

            // Set duplex printing mode to flip on the long edge
            doc.Duplex = PrintDuplex.DuplexFlipLongEdge;

            // Save the document as PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}