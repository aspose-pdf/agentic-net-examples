using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // TextStamp, FontRepository, FontStyles

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

        // Load the PDF document
        Document pdfDocument = new Document(inputPdf);

        // Create a TextStamp with bold Helvetica, red color, size 14, centered
        TextStamp textStamp = new TextStamp("CONFIDENTIAL");
        textStamp.TextState.Font = FontRepository.FindFont("Helvetica");
        textStamp.TextState.FontSize = 14;
        // Use Aspose.Pdf.Text.FontStyles instead of System.Drawing.FontStyle
        textStamp.TextState.FontStyle = FontStyles.Bold;
        textStamp.TextState.ForegroundColor = Aspose.Pdf.Color.Red;
        textStamp.HorizontalAlignment = HorizontalAlignment.Center;
        textStamp.VerticalAlignment = VerticalAlignment.Center;

        // Apply the stamp only to page 5
        if (pdfDocument.Pages.Count >= 5)
        {
            pdfDocument.Pages[5].AddStamp(textStamp);
        }
        else
        {
            Console.Error.WriteLine("PDF does not contain page 5.");
            return;
        }

        // Save the modified PDF
        pdfDocument.Save(outputPdf);
        Console.WriteLine($"Text stamp applied to page 5 and saved as '{outputPdf}'.");
    }
}
