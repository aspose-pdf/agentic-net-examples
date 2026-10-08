using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

public static class PdfAsyncExtractor
{
    /// <summary>
    /// Asynchronously extracts all text from a PDF file using Aspose.Pdf.Facades.PdfExtractor.
    /// The heavy work is off‑loaded to a background thread via Task.Run to keep the UI responsive.
    /// </summary>
    /// <param name="pdfPath">Full path to the source PDF.</param>
    /// <returns>A task that resolves to the extracted plain text.</returns>
    public static async Task<string> ExtractTextAsync(string pdfPath)
    {
        if (string.IsNullOrWhiteSpace(pdfPath))
            throw new ArgumentException("PDF path must be provided.", nameof(pdfPath));

        if (!File.Exists(pdfPath))
            throw new FileNotFoundException("PDF file not found.", pdfPath);

        // Run the synchronous extraction on a thread‑pool thread.
        return await Task.Run(() =>
        {
            using (PdfExtractor extractor = new PdfExtractor())
            {
                extractor.BindPdf(pdfPath);
                extractor.ExtractText();

                // GetText requires a destination stream.
                using (MemoryStream textStream = new MemoryStream())
                {
                    extractor.GetText(textStream);
                    textStream.Position = 0;
                    using (StreamReader reader = new StreamReader(textStream))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
        }).ConfigureAwait(false);
    }

    /// <summary>
    /// Asynchronously extracts all images from a PDF file.
    /// Each image is returned as a byte array (PNG format by default).
    /// </summary>
    /// <param name="pdfPath">Full path to the source PDF.</param>
    /// <returns>A task that resolves to a list of image byte arrays.</returns>
    public static async Task<List<byte[]>> ExtractImagesAsync(string pdfPath)
    {
        if (string.IsNullOrWhiteSpace(pdfPath))
            throw new ArgumentException("PDF path must be provided.", nameof(pdfPath));

        if (!File.Exists(pdfPath))
            throw new FileNotFoundException("PDF file not found.", pdfPath);

        return await Task.Run(() =>
        {
            var images = new List<byte[]>();

            using (PdfExtractor extractor = new PdfExtractor())
            {
                extractor.BindPdf(pdfPath);
                // Ensure images defined in resources are also extracted.
                extractor.ExtractImageMode = ExtractImageMode.DefinedInResources;
                extractor.ExtractImage();

                while (extractor.HasNextImage())
                {
                    using (MemoryStream imgStream = new MemoryStream())
                    {
                        extractor.GetNextImage(imgStream);
                        images.Add(imgStream.ToArray());
                    }
                }
            }

            return images;
        }).ConfigureAwait(false);
    }

    /// <summary>
    /// Asynchronously extracts selected pages from a PDF and saves each page as an individual PDF file.
    /// </summary>
    /// <param name="pdfPath">Full path to the source PDF.</param>
    /// <param name="pageNumbers">Array of 1‑based page numbers to extract.</param>
    /// <param name="outputDirectory">Directory where the extracted pages will be saved.</param>
    /// <returns>A task that completes when all pages have been saved.</returns>
    public static async Task ExtractPagesAsync(string pdfPath, int[] pageNumbers, string outputDirectory)
    {
        if (string.IsNullOrWhiteSpace(pdfPath))
            throw new ArgumentException("PDF path must be provided.", nameof(pdfPath));

        if (!File.Exists(pdfPath))
            throw new FileNotFoundException("PDF file not found.", pdfPath);

        if (pageNumbers == null || pageNumbers.Length == 0)
            throw new ArgumentException("At least one page number must be specified.", nameof(pageNumbers));

        if (string.IsNullOrWhiteSpace(outputDirectory))
            throw new ArgumentException("Output directory must be provided.", nameof(outputDirectory));

        Directory.CreateDirectory(outputDirectory);

        await Task.Run(() =>
        {
            // Load the source document once and dispose it properly.
            using (Document srcDoc = new Document(pdfPath))
            {
                foreach (int pageNum in pageNumbers)
                {
                    if (pageNum < 1 || pageNum > srcDoc.Pages.Count)
                        throw new ArgumentOutOfRangeException(nameof(pageNumbers), $"Page number {pageNum} is out of range.");

                    // Create a new document that will contain only the requested page.
                    using (Document singlePageDoc = new Document())
                    {
                        // Add a copy of the specific page from the source document.
                        singlePageDoc.Pages.Add(srcDoc.Pages[pageNum]);

                        string outPath = Path.Combine(outputDirectory, $"Page_{pageNum}.pdf");
                        singlePageDoc.Save(outPath);
                    }
                }
            }
        }).ConfigureAwait(false);
    }
}

// Dummy entry point to satisfy the compiler when building as an executable.
public class Program
{
    public static void Main(string[] args)
    {
        // Intentionally left blank – the library methods are intended to be called from UI code.
    }
}