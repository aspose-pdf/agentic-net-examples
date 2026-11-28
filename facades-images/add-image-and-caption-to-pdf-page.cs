using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // FormattedText, EncodingType

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";   // source PDF
        const string imagePath = "image.jpg"; // image to insert
        const string outputPdf = "output.pdf"; // result PDF

        // Verify files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        try
        {
            // PdfFileMend does NOT implement IDisposable – do NOT wrap in using
            var mend = new PdfFileMend();

            // Bind the existing PDF file
            mend.BindPdf(inputPdf);

            // Target page(s)
            int[] targetPages = { 11 };

            // Add the image to page 11 at (250,250). The overload expects a rectangle (llx,lly,urx,ury).
            // Here we place the image in a 100x100 box for illustration.
            mend.AddImage(imagePath, targetPages, 250f, 250f, 350f, 350f);

            // Prepare formatted text "Caption"
            var formatted = new FormattedText(
                "Caption",
                System.Drawing.Color.Black,               // fully qualified to avoid ambiguity
                Aspose.Pdf.Facades.FontStyle.Helvetica,   // FontStyle enum, not a string
                Aspose.Pdf.Facades.EncodingType.Winansi,
                false,                                     // isBold
                12f);                                      // font size

            // Add the formatted text to the same page and rectangle (adjust as needed)
            mend.AddText(formatted, targetPages, 250f, 250f, 350f, 270f);

            // Save the modified PDF
            mend.Save(outputPdf);
            mend.Close();

            Console.WriteLine($"PDF successfully updated and saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}
