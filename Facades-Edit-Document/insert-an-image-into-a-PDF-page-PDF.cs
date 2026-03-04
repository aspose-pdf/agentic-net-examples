using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string imagePath = "image.jpg";

        // Verify that the source PDF and image exist
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

        // Use PdfFileMend (a Facade) to add the image onto the PDF
        using (PdfFileMend mend = new PdfFileMend())
        {
            // Bind the existing PDF document
            mend.BindPdf(inputPdf);

            // Open the image file as a stream
            using (FileStream imgStream = File.OpenRead(imagePath))
            {
                // Add the image to page 1.
                // Parameters: stream, page number (1‑based), lower‑left X/Y, upper‑right X/Y
                bool added = mend.AddImage(imgStream, 1, 10f, 10f, 100f, 100f);
                if (!added)
                {
                    Console.Error.WriteLine("Failed to add the image to the PDF.");
                }
            }

            // Save the modified PDF to the output path
            mend.Save(outputPdf);

            // Close the facade (Dispose will also be called by the using block)
            mend.Close();
        }

        Console.WriteLine($"Image successfully inserted. Output saved to '{outputPdf}'.");
    }
}