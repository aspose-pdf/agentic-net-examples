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
        const string logoPath = "logo.png";
        const string customText = "Confidential";

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
        var document = new Document(inputPdf);

        // Iterate over each page and add both an image stamp and a text stamp
        foreach (Page page in document.Pages)
        {
            // ---------- Image stamp (logo) ----------
            var imageStamp = new ImageStamp(logoPath)
            {
                // Optional: set a custom size for the logo
                Width = 80,
                Height = 40,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Bottom,
                YIndent = 10 // distance from the bottom edge
            };
            page.AddStamp(imageStamp);

            // ---------- Text stamp (custom text) ----------
            var textStamp = new TextStamp(customText)
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Bottom,
                YIndent = 30 // place the text a bit above the logo
            };
            // Configure the appearance of the text
            textStamp.TextState.Font = FontRepository.FindFont("Helvetica");
            textStamp.TextState.FontSize = 24;
            textStamp.TextState.FontStyle = FontStyles.Regular;
            textStamp.TextState.ForegroundColor = Aspose.Pdf.Color.Red;

            page.AddStamp(textStamp);
        }

        // Save the stamped PDF
        document.Save(outputPdf);
        Console.WriteLine($"Stamped PDF saved to '{outputPdf}'.");
    }
}
