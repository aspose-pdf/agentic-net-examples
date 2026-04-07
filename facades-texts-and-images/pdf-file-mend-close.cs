using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string imagePath = "sample.png";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Create the facade for modifying the PDF
        PdfFileMend mend = new PdfFileMend();
        try
        {
            // Load the source PDF
            mend.BindPdf(inputPath);

            // Add an image to page 1 at the specified rectangle (llx, lly, urx, ury)
            using (FileStream imgStream = File.OpenRead(imagePath))
            {
                mend.AddImage(imgStream, 1, 100f, 500f, 300f, 700f);
            }

            // Save the modified document
            mend.Save(outputPath);
        }
        finally
        {
            // Ensure the facade is closed and all changes are flushed
            mend.Close();
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}