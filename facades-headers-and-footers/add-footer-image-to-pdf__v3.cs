using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // only needed if you use other Facades features elsewhere

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string signaturePath = "signature.png";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(signaturePath))
        {
            Console.Error.WriteLine($"Signature image not found: {signaturePath}");
            return;
        }

        // Load the source PDF
        var document = new Document(inputPdfPath);

        // Create an ImageStamp from the signature file stream
        ImageStamp imageStamp;
        using (FileStream imgStream = new FileStream(signaturePath, FileMode.Open, FileAccess.Read))
        {
            imageStamp = new ImageStamp(imgStream);
        }

        // Position the stamp at the bottom‑center with a 3‑point bottom margin
        imageStamp.HorizontalAlignment = HorizontalAlignment.Center;
        imageStamp.VerticalAlignment   = VerticalAlignment.Bottom;
        imageStamp.BottomMargin = 3; // points from the bottom edge

        // Optionally set size – if you want the original image size, omit these lines
        // imageStamp.Width = 100;   // example width in points
        // imageStamp.Height = 50;   // example height in points

        // Apply the stamp to every page of the PDF
        foreach (Page page in document.Pages)
        {
            page.AddStamp(imageStamp);
        }

        // Save the stamped PDF
        document.Save(outputPdfPath);

        Console.WriteLine($"Stamped PDF saved to '{outputPdfPath}'.");
    }
}
