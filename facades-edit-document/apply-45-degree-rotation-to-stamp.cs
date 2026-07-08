using System;
using System.IO;
using Aspose.Pdf.Facades; // Facade API for stamping
using Aspose.Pdf; // Needed for Aspose.Pdf.Color if used elsewhere

class Program
{
    static void Main()
    {
        // Input and output PDF paths
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Create a PdfFileStamp facade and specify source/target files
        Aspose.Pdf.Facades.PdfFileStamp fileStamp = new Aspose.Pdf.Facades.PdfFileStamp();
        fileStamp.InputFile  = inputPdf;
        fileStamp.OutputFile = outputPdf;

        // Create a stamp (text logo) and set its rotation to 45 degrees
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();

        // Bind simple text as the stamp content
        // FormattedText constructor requires System.Drawing.Color (Windows‑only), but this is
        // acceptable for the stamp API. If running on non‑Windows platforms, replace with an
        // image stamp via BindImage().
        Aspose.Pdf.Facades.FormattedText ft = new Aspose.Pdf.Facades.FormattedText(
            "DIAGONAL",                     // text
            System.Drawing.Color.Black,     // text color
            "Helvetica",                    // font name
            EncodingType.Winansi,           // encoding
            false,                          // embed font?
            36);                            // font size

        stamp.BindLogo(ft);

        // Apply a 45‑degree rotation (arbitrary angle)
        stamp.Rotation = 45f; // Rotation property on Facades.Stamp accepts float degrees

        // Add the stamp to the PDF file
        fileStamp.AddStamp(stamp);

        // Finalize and close the facade (saves the output file)
        fileStamp.Close();

        Console.WriteLine($"Stamp with 45° rotation applied. Output saved to '{outputPdf}'.");
    }
}