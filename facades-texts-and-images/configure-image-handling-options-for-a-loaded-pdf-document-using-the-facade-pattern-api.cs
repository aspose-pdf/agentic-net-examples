using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string outputPdf = "output.pdf";         // PDF after image handling
        const string imagePath = "logo.png";           // image to place

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Initialize the facade for adding images/text
            using (PdfFileMend mend = new PdfFileMend(doc))
            {
                // Example of configuring text handling (optional)
                mend.IsWordWrap = true;                                   // enable word‑wrap for AddText (if used)
                mend.TextPositioningMode = PositioningMode.Legacy;        // default positioning mode

                // Configure compositing parameters for the image (e.g., blend mode)
                CompositingParameters compParams = new CompositingParameters(BlendMode.Multiply);

                // Add the image to page 1 at the desired rectangle (LLX, LLY, URX, URY)
                using (FileStream imgStream = File.OpenRead(imagePath))
                {
                    // lower‑left (50,50), upper‑right (200,200) – adjust as needed
                    bool added = mend.AddImage(imgStream, 1, 50f, 50f, 200f, 200f, compParams);
                    if (!added)
                    {
                        Console.Error.WriteLine("Failed to add image to the PDF.");
                    }
                }

                // Save the modified PDF
                mend.Save(outputPdf);
            }
        }

        Console.WriteLine($"Image handling completed. Output saved to '{outputPdf}'.");
    }
}