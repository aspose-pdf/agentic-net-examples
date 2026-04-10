using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing; // HorizontalAlignment enum lives here

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_centered.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document pdfDoc = new Document(inputPath);

        // Verify that a second page exists
        if (pdfDoc.Pages.Count < 2)
        {
            Console.Error.WriteLine("The PDF does not contain a second page.");
            return;
        }

        // Create a fragment (you can replace this with any content you need to centre)
        TextFragment centeredFragment = new TextFragment("Centered Content")
        {
            // Use the Drawing namespace HorizontalAlignment enum to centre horizontally
            HorizontalAlignment = HorizontalAlignment.Center,
            // Position.Y is measured from the bottom of the page; X is ignored when centred
            Position = new Position(0, 500) // adjust Y as required
        };

        // Add the fragment to page 2
        pdfDoc.Pages[2].Paragraphs.Add(centeredFragment);

        // Save the modified PDF
        pdfDoc.Save(outputPath);
        Console.WriteLine($"Centered page saved to '{outputPath}'.");
    }
}
