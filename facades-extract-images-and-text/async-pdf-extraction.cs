using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

class PdfAsyncExtractor
{
    // Asynchronously extracts all text from a PDF and saves it to a single .txt file.
    public static async Task ExtractTextAsync(string pdfPath, string outputTextPath, CancellationToken cancellationToken = default)
    {
        // Ensure the source PDF exists.
        if (!File.Exists(pdfPath))
            throw new FileNotFoundException($"PDF file not found: {pdfPath}");

        // PdfExtractor implements IDisposable, so wrap it in a using block.
        using (var extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor (synchronous operation).
            extractor.BindPdf(pdfPath);

            // Run the extraction on a background thread to avoid blocking the UI thread.
            await Task.Run(() =>
            {
                // Extract the text content.
                extractor.ExtractText();

                // Save the extracted text to the specified file.
                extractor.GetText(outputTextPath);
            }, cancellationToken).ConfigureAwait(false);
        }
    }

    // Asynchronously extracts all images from a PDF and saves them to the specified folder.
    public static async Task ExtractImagesAsync(string pdfPath, string outputFolder, CancellationToken cancellationToken = default)
    {
        if (!File.Exists(pdfPath))
            throw new FileNotFoundException($"PDF file not found: {pdfPath}");

        // Create the output folder if it does not exist.
        Directory.CreateDirectory(outputFolder);

        using (var extractor = new PdfExtractor())
        {
            extractor.BindPdf(pdfPath);

            await Task.Run(() =>
            {
                // Start image extraction.
                extractor.ExtractImage();

                int imageIndex = 1;
                // Retrieve each image until no more are available.
                while (extractor.HasNextImage())
                {
                    // Build a file name for the image.
                    string imagePath = Path.Combine(outputFolder, $"image-{imageIndex}.png");

                    // Save the image. Using GetNextImage(string) saves in the default format (PNG).
                    extractor.GetNextImage(imagePath);

                    imageIndex++;
                }
            }, cancellationToken).ConfigureAwait(false);
        }
    }

    // Asynchronously extracts all embedded file attachments from a PDF and saves them to the specified folder.
    public static async Task ExtractAttachmentsAsync(string pdfPath, string outputFolder, CancellationToken cancellationToken = default)
    {
        if (!File.Exists(pdfPath))
            throw new FileNotFoundException($"PDF file not found: {pdfPath}");

        Directory.CreateDirectory(outputFolder);

        using (var extractor = new PdfExtractor())
        {
            extractor.BindPdf(pdfPath);

            await Task.Run(() =>
            {
                // Extract all attachments.
                extractor.ExtractAttachment();

                // Save each attachment to the output folder.
                extractor.GetAttachment(outputFolder);
            }, cancellationToken).ConfigureAwait(false);
        }
    }

    // Example usage of the asynchronous extraction methods.
    static async Task Main(string[] args)
    {
        // Paths (adjust as needed).
        string pdfFile = "sample.pdf";
        string textOutput = "sample_text.txt";
        string imagesOutputFolder = "ExtractedImages";
        string attachmentsOutputFolder = "ExtractedAttachments";

        // Cancellation token source (optional, can be used to cancel operations).
        using var cts = new CancellationTokenSource();

        try
        {
            // Extract text.
            await ExtractTextAsync(pdfFile, textOutput, cts.Token);
            Console.WriteLine($"Text extracted to: {textOutput}");

            // Extract images.
            await ExtractImagesAsync(pdfFile, imagesOutputFolder, cts.Token);
            Console.WriteLine($"Images extracted to folder: {imagesOutputFolder}");

            // Extract attachments.
            await ExtractAttachmentsAsync(pdfFile, attachmentsOutputFolder, cts.Token);
            Console.WriteLine($"Attachments extracted to folder: {attachmentsOutputFolder}");
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Extraction was canceled.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during extraction: {ex.Message}");
        }
    }
}