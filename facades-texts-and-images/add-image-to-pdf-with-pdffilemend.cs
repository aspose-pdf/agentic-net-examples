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
            // Initialize the facade with the source PDF
            mend = new PdfFileMend();
            mend.BindPdf(inputPath);

            // Example modification: add an image to page 1
            if (File.Exists(imagePath))
            {
                // AddImage(string imageFile, int pageNumber, float llx, float lly, float urx, float ury)
                mend.AddImage(imagePath, 1, 100, 500, 300, 800);
            }

            // Persist the changes
            mend.Save(outputPath);
        }
        finally
        {
            // Ensure the facade is closed even if an exception occurs
            if (mend != null)
            {
                mend.Close();
            }
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}