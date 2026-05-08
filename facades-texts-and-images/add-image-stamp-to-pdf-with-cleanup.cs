using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string imagePath = "stamp.png";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        PdfFileMend mend = null;
        try
        {
            // Initialize the facade and bind the source PDF
            mend = new PdfFileMend();
            mend.BindPdf(inputPath);

            // Example operation: add an image to the first page if the image file exists
            if (File.Exists(imagePath))
            {
                // AddImage(string imageFile, int pageNumber, float x, float y, float width, float height)
                mend.AddImage(imagePath, 1, 100f, 500f, 200f, 100f);
            }

            // Persist the modifications
            mend.Save(outputPath);
        }
        finally
        {
            // Guarantee that resources are released even if an exception occurs
            if (mend != null)
            {
                mend.Close();
            }
        }

        Console.WriteLine($"PDF processing completed. Output saved to '{outputPath}'.");
    }
}