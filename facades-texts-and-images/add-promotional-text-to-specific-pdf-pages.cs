using System;
using System.IO;
using System.Drawing; // for Color
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // for EncodingType

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

        // Use PdfFileMend – it provides AddText that accepts an array of page numbers
        PdfFileMend pdfMend = new PdfFileMend();
        try
        {
            // Bind the source PDF
            pdfMend.BindPdf(inputPdf);

            // Prepare the promotional message as FormattedText (all styling via constructor)
            FormattedText promoText = new FormattedText(
                "Special Offer! Buy now.",   // text
                Color.Red,                    // text color (System.Drawing.Color)
                "Helvetica",                 // font name
                EncodingType.Winansi,         // encoding
                false,                        // embed font?
                24);                          // font size

            // Pages on which the text will be placed (1‑based indexing)
            int[] targetPages = new int[] { 3, 5, 7 };

            // Define the rectangle where the text will appear (lower‑left and upper‑right coordinates)
            float lowerLeftX = 100f;
            float lowerLeftY = 500f;
            float upperRightX = 300f;
            float upperRightY = 600f;

            // Add the same promotional message to the specified pages
            pdfMend.AddText(promoText, targetPages, lowerLeftX, lowerLeftY, upperRightX, upperRightY);

            // Save the result
            pdfMend.Save(outputPdf);
        }
        finally
        {
            // Release file handles
            pdfMend.Close();
        }

        Console.WriteLine($"Promotional message added to pages 3, 5, and 7. Output saved to '{outputPdf}'.");
    }
}
