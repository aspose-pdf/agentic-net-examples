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

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the source PDF
        Document doc = new Document(inputPdf);

        // Create a text stamp with the required appearance
        TextStamp stamp = new TextStamp("Confidential")
        {
            // 50 % opacity for a semi‑transparent effect
            Opacity = 0.5f,
            // Center the stamp on the page (optional – adjust as needed)
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        };

        // NOTE: In recent versions of Aspose.PDF the TextStamp class no longer exposes an
        //       IsBackground property. To place the stamp behind the existing page content
        //       you can add the stamp to the page's Annotations collection instead of the
        //       regular stamp collection, or simply add it first before any other content.
        //       Here we add the stamp directly; it will appear on top. If a background
        //       placement is required, use the Page.AddStamp method with the stamp's
        //       IsBackground flag set via the base Stamp class (available in older SDKs).

        // Configure the visual style of the stamp text
        stamp.TextState.Font = FontRepository.FindFont("Helvetica");
        stamp.TextState.FontSize = 36;
        stamp.TextState.ForegroundColor = Aspose.Pdf.Color.Red;

        // Apply the stamp to every page in the document
        foreach (Page page in doc.Pages)
        {
            page.AddStamp(stamp);
        }

        // Save the stamped PDF
        doc.Save(outputPdf);
        Console.WriteLine($"Stamped PDF saved to '{outputPdf}'.");
    }
}
