using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";          // source PDF
        const string outputPdf = "stamped_output.pdf"; // destination PDF
        const string logoPath = "logo.png";           // company logo image

        // Verify required files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(logoPath))
        {
            Console.Error.WriteLine($"Logo image not found: {logoPath}");
            return;
        }

        // Load the PDF document
        Document pdfDoc = new Document(inputPdf);

        // ---------------------------------------------------------------------
        // Create an image stamp (company logo)
        // ---------------------------------------------------------------------
        ImageStamp imageStamp = new ImageStamp(logoPath)
        {
            Width = 100,
            Height = 50,
            Opacity = 0.5,
            Background = false,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        };

        // ---------------------------------------------------------------------
        // Create a text stamp ("Confidential")
        // ---------------------------------------------------------------------
        TextStamp textStamp = new TextStamp("Confidential")
        {
            Opacity = 0.5,
            Background = false,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        };
        // TextState is read‑only – modify the existing instance instead of assigning a new one
        textStamp.TextState.Font = FontRepository.FindFont("Arial");
        textStamp.TextState.FontSize = 24;
        textStamp.TextState.FontStyle = FontStyles.Bold;
        textStamp.TextState.ForegroundColor = Color.FromRgb(1, 0, 0); // red

        // Apply both stamps to every page in the document
        foreach (Page page in pdfDoc.Pages)
        {
            page.AddStamp(imageStamp);
            page.AddStamp(textStamp);
        }

        // Save the stamped PDF
        pdfDoc.Save(outputPdf);
        Console.WriteLine($"Stamped PDF saved to '{outputPdf}'.");
    }
}
