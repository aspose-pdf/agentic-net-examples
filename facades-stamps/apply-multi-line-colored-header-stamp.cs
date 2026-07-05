using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Initialize the facade and bind the source PDF.
            using (PdfFileStamp fileStamp = new PdfFileStamp())
            {
                fileStamp.BindPdf(inputPdf);

                // First header line – red color.
                FormattedText line1 = new FormattedText(
                    "First Line",
                    System.Drawing.Color.Red,          // Font color
                    "Helvetica",                       // Font name
                    EncodingType.Winansi,              // Encoding
                    false,                             // Do not embed font
                    24);                               // Font size
                fileStamp.AddHeader(line1, 20f);      // Top margin for first line

                // Second header line – green color.
                FormattedText line2 = new FormattedText(
                    "Second Line",
                    System.Drawing.Color.Green,
                    "Helvetica",
                    EncodingType.Winansi,
                    false,
                    24);
                fileStamp.AddHeader(line2, 50f);      // Top margin for second line

                // Third header line – blue color.
                FormattedText line3 = new FormattedText(
                    "Third Line",
                    System.Drawing.Color.Blue,
                    "Helvetica",
                    EncodingType.Winansi,
                    false,
                    24);
                fileStamp.AddHeader(line3, 80f);      // Top margin for third line

                // Save the stamped PDF.
                fileStamp.Save(outputPdf);
                fileStamp.Close();
            }

            Console.WriteLine($"Multi‑line header stamp applied successfully. Output saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}