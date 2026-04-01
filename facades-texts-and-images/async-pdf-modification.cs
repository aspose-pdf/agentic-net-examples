using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static async Task Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string imageFile = "stamp.png";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        if (!File.Exists(imageFile))
        {
            Console.Error.WriteLine($"Image file not found: {imageFile}");
            return;
        }

        try
        {
            await ModifyPdfAsync(inputPdf, outputPdf, imageFile, CancellationToken.None);
            Console.WriteLine($"Modified PDF saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    private static Task ModifyPdfAsync(string inputPath, string outputPath, string imagePath, CancellationToken cancellationToken)
    {
        return Task.Run(() =>
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (PdfFileMend fileMend = new PdfFileMend())
            {
                fileMend.BindPdf(inputPath);
                // Add image to the first page; coordinates are in points.
                fileMend.AddImage(imagePath, new int[] { 1 }, 100f, 500f, 300f, 700f);
                fileMend.Save(outputPath);
            }
        }, cancellationToken);
    }
}