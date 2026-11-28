using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string sealImage = "seal.jpg";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(sealImage))
        {
            Console.Error.WriteLine($"Seal image not found: {sealImage}");
            return;
        }

        // Load the PDF document
        Document doc = new Document(inputPdf);

        // Create an ImageStamp (core API) and configure its position
        ImageStamp stamp = new ImageStamp(sealImage)
        {
            // Center horizontally, align to the bottom of the page
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment   = VerticalAlignment.Bottom,
            // Zero bottom margin places the image flush with the page edge
            BottomMargin = 0
        };

        // Apply the stamp to every page in the document
        foreach (Page page in doc.Pages)
        {
            page.AddStamp(stamp);
        }

        // Save the stamped PDF
        doc.Save(outputPdf);

        Console.WriteLine($"Footer image added and saved to '{outputPdf}'.");
    }
}
