using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        string inputPath = "input.pdf";
        string outputPath = "output.pdf";
        string imagePath = "stamp.png";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image not found: {imagePath}");
            return;
        }

        PdfFileMend mend = new PdfFileMend();
        try
        {
            // Load the source PDF
            mend.BindPdf(inputPath);

            // Add an image to page 1 at specified coordinates
            using (FileStream imgStream = File.OpenRead(imagePath))
            {
                // AddImage(stream, pageNumber, llx, lly, urx, ury)
                mend.AddImage(imgStream, 1, 100f, 500f, 200f, 600f);
            }

            // Save the modified PDF to a new file
            mend.Save(outputPath);
        }
        finally
        {
            // Ensure the facade is closed and changes are committed
            mend.Close();
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
