using System;
using System.IO;
using Aspose.Pdf.Facades; // PdfFileSignature lives here

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPng = "WitnessSignature.png";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Bind the PDF and extract the signature image
        using (PdfFileSignature pdfSignature = new PdfFileSignature())
        {
            pdfSignature.BindPdf(inputPdf);

            // Directly pass the signature field name to ExtractImage (no SignatureName object needed)
            using (Stream imageStream = pdfSignature.ExtractImage("WitnessSignature"))
            {
                if (imageStream == null)
                {
                    Console.Error.WriteLine("No image found for the specified signature field.");
                    return;
                }

                // Save the extracted image stream to a PNG file
                using (FileStream fileOut = new FileStream(outputPng, FileMode.Create, FileAccess.Write))
                {
                    imageStream.CopyTo(fileOut);
                }
            }
        }

        Console.WriteLine($"Signature image saved to '{outputPng}'.");
    }
}
