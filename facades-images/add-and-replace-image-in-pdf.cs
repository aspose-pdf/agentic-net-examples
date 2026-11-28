using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string imagePath = "image.png";
        const string replacementImagePath = "image.png"; // use same file for demo or a different one

        // Verify required files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }
        if (!File.Exists(replacementImagePath))
        {
            Console.Error.WriteLine($"Replacement image file not found: {replacementImagePath}");
            return;
        }

        // PdfFileMend does NOT implement IDisposable, so we instantiate it directly
        var mend = new PdfFileMend();
        mend.BindPdf(inputPdf);

        // Page number is 1‑based (Aspose.Pdf uses 1‑based indexing)
        int pageNumber = 1;

        // Define image placement rectangle (lower‑left and upper‑right coordinates) as floats
        float llx = 100f; // lower‑left X
        float lly = 500f; // lower‑left Y
        float urx = 300f; // upper‑right X
        float ury = 700f; // upper‑right Y

        // Add the first image at the specified location
        // PdfFileMend provides an overload that accepts a file path directly
        mend.AddImage(imagePath, pageNumber, llx, lly, urx, ury);

        // "Replace" the image by adding another one at the same coordinates.
        // The new image is drawn on top of the previous one, effectively replacing it.
        mend.AddImage(replacementImagePath, pageNumber, llx, lly, urx, ury);

        // Save the modified PDF
        mend.Save(outputPdf);
        mend.Close(); // optional – releases internal resources

        Console.WriteLine($"Modified PDF saved to '{outputPdf}'.");
    }
}
