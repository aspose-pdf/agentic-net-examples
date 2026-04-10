using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string stampImage = "stamp.png";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        if (!File.Exists(stampImage))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImage}");
            return;
        }

        // Initialize the facade and specify input/output files
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.InputFile = inputPdf;
        fileStamp.OutputFile = outputPdf;

        // Create a stamp, bind an image, set opacity and background flag
        Stamp stamp = new Stamp();
        stamp.BindImage(stampImage);
        stamp.Opacity = 0.3f;          // 30% opacity
        stamp.IsBackground = true;    // place behind page content

        // Optional: position the stamp (origin at lower‑left corner)
        stamp.SetOrigin(0, 0);

        // Apply the stamp to all pages
        fileStamp.AddStamp(stamp);
        fileStamp.Close();

        Console.WriteLine($"Background stamp applied with 30% opacity: {outputPdf}");
    }
}