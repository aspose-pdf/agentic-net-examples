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
        const string watermarkText = "CONFIDENTIAL";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        Document pdfDocument = new Document(inputPdf);

        // Create a text stamp that will act as a watermark
        TextStamp stamp = new TextStamp(watermarkText)
        {
            // Center the watermark on each page
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            // Place the stamp behind the page content if the property exists (some versions expose IsBackground)
            // IsBackground = true,
            // 50% opacity for a translucent effect
            Opacity = 0.5f
        };

        // Configure the TextState – the TextState property itself is read‑only, so set its members individually
        stamp.TextState.Font = FontRepository.FindFont("Helvetica");
        stamp.TextState.FontSize = 72;
        stamp.TextState.ForegroundColor = Aspose.Pdf.Color.Red;

        // Apply the stamp to every page in the document
        foreach (Page page in pdfDocument.Pages)
        {
            page.AddStamp(stamp);
        }

        // Save the modified PDF
        pdfDocument.Save(outputPdf);
    }
}
