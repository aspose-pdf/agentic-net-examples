using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;
using System.Drawing;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string imageFile = "image.jpg";
        const string textContent = "Sample text for audit";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(imageFile))
        {
            Console.Error.WriteLine($"Image file not found: {imageFile}");
            return;
        }

        // Initialize PdfFileMend facade and bind the source PDF
        using (PdfFileMend mend = new PdfFileMend())
        {
            mend.BindPdf(inputPdf);

            // Define target page and coordinates (float literals required)
            int targetPage = 1;
            float llx = 100f;
            float lly = 500f;
            float urx = 300f;
            float ury = 600f;

            // Add image and log the operation
            mend.AddImage(imageFile, new int[] { targetPage }, llx, lly, urx, ury);
            Console.WriteLine($"{DateTime.UtcNow:O} Added image '{imageFile}' to page {targetPage}");

            // Prepare formatted text (use System.Drawing.Color for the color argument)
            FormattedText ft = new FormattedText(
                textContent,
                System.Drawing.Color.Black, // corrected color type
                "Helvetica",
                EncodingType.Winansi,
                false,
                12f);

            // Add text and log the operation (log the text content as the "file name" for audit purposes)
            mend.AddText(ft, new int[] { targetPage }, llx, lly, urx, ury);
            Console.WriteLine($"{DateTime.UtcNow:O} Added text '{textContent}' to page {targetPage}");

            // Save the modified PDF
            mend.Save(outputPdf);
            Console.WriteLine($"{DateTime.UtcNow:O} Saved modified PDF as '{outputPdf}'");
        }
    }
}
