using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static async Task Main(string[] args)
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string imagePath = "stamp.png";

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

        await ModifyPdfAsync(inputPath, outputPath, imagePath);
        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }

    private static Task ModifyPdfAsync(string inputFile, string outputFile, string imageFile)
    {
        return Task.Run(() =>
        {
            using (PdfFileMend mend = new PdfFileMend())
            {
                // Bind the source PDF document
                mend.BindPdf(inputFile);

                // Define image placement coordinates (float literals)
                float lowerLeftX = 100.0f;
                float lowerLeftY = 500.0f;
                float upperRightX = 300.0f;
                float upperRightY = 700.0f;
                int[] pages = new int[] { 1 };

                // Add the image to the specified page(s)
                mend.AddImage(imageFile, pages, lowerLeftX, lowerLeftY, upperRightX, upperRightY);

                // Save the modified PDF
                mend.Save(outputFile);
                mend.Close();
            }
        });
    }
}