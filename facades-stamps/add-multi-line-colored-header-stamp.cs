using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // FormattedText, EncodingType
using System.Drawing; // System.Drawing.Color

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

        // Load the document to obtain page dimensions (assumes all pages have same size)
        Document doc = new Document(inputPdf);
        float pageWidth = (float)doc.Pages[1].PageInfo.Width;   // cast double → float
        float pageHeight = (float)doc.Pages[1].PageInfo.Height; // cast double → float

        // Initialize the PdfFileStamp facade using the recommended BindPdf method
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.BindPdf(inputPdf);

        // ---------- First header line (red) ----------
        Aspose.Pdf.Facades.Stamp redStamp = new Aspose.Pdf.Facades.Stamp(); // fully‑qualified to avoid CS0104
        FormattedText redText = new FormattedText(
            "First Header Line",
            System.Drawing.Color.Red,
            "Helvetica",
            EncodingType.Winansi,
            false,
            14);
        redStamp.BindLogo(redText);
        // Position near the top of the page (Y measured from bottom)
        redStamp.SetOrigin(0, pageHeight - 40);
        // Removed HorizontalAlignment – not supported on Facades.Stamp
        fileStamp.AddStamp(redStamp);

        // ---------- Second header line (blue) ----------
        Aspose.Pdf.Facades.Stamp blueStamp = new Aspose.Pdf.Facades.Stamp(); // fully‑qualified
        FormattedText blueText = new FormattedText(
            "Second Header Line",
            System.Drawing.Color.Blue,
            "Helvetica",
            EncodingType.Winansi,
            false,
            14);
        blueStamp.BindLogo(blueText);
        // Slightly lower than the first line
        blueStamp.SetOrigin(0, pageHeight - 60);
        // Removed HorizontalAlignment – not supported on Facades.Stamp
        fileStamp.AddStamp(blueStamp);

        // Save the modified PDF
        fileStamp.Save(outputPdf);
        fileStamp.Close();

        Console.WriteLine($"Multi‑line header stamp applied and saved to '{outputPdf}'.");
    }
}
