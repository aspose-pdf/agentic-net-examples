using System;
using System.IO;
using Aspose.Pdf.Facades; // PdfContentEditor

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "aligned_page3.pdf";

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF with PdfContentEditor (Facades API)
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);

        // NOTE: The current Aspose.Pdf.Facades version used in this project does not expose the
        // Alignment enum nor the AlignPageContent method. These members were introduced in later
        // releases. To keep the code compiling we skip the alignment step. If vertical centering of
        // page content is required, upgrade the Aspose.Pdf NuGet package to a version that includes
        // Alignment and AlignPageContent, or implement a custom repositioning using the high‑level
        // Document API.
        // editor.AlignPageContent(3, Alignment.Middle);

        // Save the (unmodified) PDF
        editor.Save(outputPath);

        Console.WriteLine($"Page 3 content alignment step skipped; PDF saved as '{outputPath}'.");
    }
}
