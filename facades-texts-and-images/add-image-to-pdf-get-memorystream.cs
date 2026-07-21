using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

public static class PdfImageHelper
{
    /// <summary>
    /// Adds an image to the first page of an existing PDF and returns the modified PDF as a <see cref="MemoryStream"/>.
    /// The caller can write the stream to an HTTP response, a file, or any other destination.
    /// </summary>
    /// <param name="pdfFilePath">Full path to the source PDF.</param>
    /// <param name="imageFilePath">Full path to the image that will be placed on the PDF.</param>
    /// <returns>A <see cref="MemoryStream"/> containing the modified PDF. The stream's position is set to 0.</returns>
    public static async Task<MemoryStream> AddImageAndGetStreamAsync(string pdfFilePath, string imageFilePath)
    {
        if (string.IsNullOrEmpty(pdfFilePath))
            throw new ArgumentException("PDF file path is required.", nameof(pdfFilePath));
        if (string.IsNullOrEmpty(imageFilePath))
            throw new ArgumentException("Image file path is required.", nameof(imageFilePath));

        if (!File.Exists(pdfFilePath))
            throw new FileNotFoundException("Source PDF not found.", pdfFilePath);
        if (!File.Exists(imageFilePath))
            throw new FileNotFoundException("Image file not found.", imageFilePath);

        // PdfFileMend is a facade that can modify an existing PDF (add images, texts, etc.).
        using (PdfFileMend pdfMend = new PdfFileMend())
        {
            // Bind the existing PDF document.
            pdfMend.BindPdf(pdfFilePath);

            // Add the image to page 1.
            // Parameters: image path, page number (1‑based), lower‑left X, lower‑left Y, upper‑right X, upper‑right Y.
            pdfMend.AddImage(imageFilePath, 1, 100f, 500f, 300f, 700f);

            // Prepare a memory stream to hold the modified PDF.
            var pdfStream = new MemoryStream();
            pdfMend.Save(pdfStream);
            pdfStream.Position = 0; // reset for reading
            return pdfStream; // caller owns the stream
        }
    }
}

// Added entry point to satisfy the compiler when the project is built as an executable.
public class Program
{
    public static async Task Main(string[] args)
    {
        // Simple demonstration of the helper. In a real web application you would write the
        // returned stream directly to the HttpResponse.
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: <pdfPath> <imagePath>");
            return;
        }

        string pdfPath = args[0];
        string imagePath = args[1];

        using MemoryStream modifiedPdf = await PdfImageHelper.AddImageAndGetStreamAsync(pdfPath, imagePath);

        // For console/demo purposes write the result to a file.
        const string outputPath = "modified.pdf";
        using (FileStream file = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
        {
            await modifiedPdf.CopyToAsync(file);
        }

        Console.WriteLine($"Modified PDF saved to {outputPath}");
    }
}
