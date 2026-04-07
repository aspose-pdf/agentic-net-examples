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

        // Bind the existing PDF to PdfFileMend
        using (PdfFileMend mend = new PdfFileMend())
        {
            mend.BindPdf(inputPdf);

            // Add PNG image to page 2 at specified coordinates
            // lower-left (100, 200), upper-right (300, 400)
            bool added = mend.AddImage(imagePath, 2, 100f, 200f, 300f, 400f);
            if (!added)
            {
                Console.Error.WriteLine("Failed to add image to the PDF.");
            }

            // Save the modified PDF
            mend.Save(outputPdf);
        }

        Console.WriteLine($"Image added to page 2 and saved as {outputPdf}");
    }
}