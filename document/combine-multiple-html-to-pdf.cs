using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Text;   // required for HtmlLoadOptions (inherits from LoadOptions)

class HtmlToPdfConverter
{
    /// <summary>
    /// Creates a single PDF document from a collection of HTML strings.
    /// Each HTML string is loaded with custom HtmlLoadOptions and its pages are appended to the target PDF.
    /// </summary>
    /// <param name="htmlContents">HTML fragments to be converted.</param>
    /// <param name="outputPdfPath">Path of the resulting PDF file.</param>
    public static void Convert(IEnumerable<string> htmlContents, string outputPdfPath)
    {
        if (htmlContents == null) throw new ArgumentNullException(nameof(htmlContents));
        if (string.IsNullOrWhiteSpace(outputPdfPath)) throw new ArgumentException("Output path must be provided.", nameof(outputPdfPath));

        // Target PDF that will hold all pages from the individual HTML conversions.
        using (Document targetPdf = new Document())
        {
            // Custom load options for HTML → PDF conversion.
            HtmlLoadOptions loadOptions = new HtmlLoadOptions
            {
                // Example customizations – adjust as needed.
                IsEmbedFonts = true,               // embed fonts into the PDF
                IsRenderToSinglePage = false,      // keep original pagination
                // BasePath can be set if the HTML strings reference external resources.
                // BasePath = @"C:\Resources\"
            };

            foreach (string html in htmlContents)
            {
                if (string.IsNullOrEmpty(html)) continue;

                // Load the HTML string via a memory stream.
                using (MemoryStream htmlStream = new MemoryStream(Encoding.UTF8.GetBytes(html)))
                using (Document htmlDoc = new Document(htmlStream, loadOptions))
                {
                    // Append all pages from the temporary document to the target PDF.
                    targetPdf.Pages.Add(htmlDoc.Pages);
                }
            }

            // Save the assembled PDF.
            targetPdf.Save(outputPdfPath);
        }
    }

    // Example usage.
    static void Main()
    {
        var htmlFragments = new List<string>
        {
            "<html><body><h1>First Document</h1><p>Hello, world!</p></body></html>",
            "<html><body><h2>Second Document</h2><p>Another paragraph.</p></body></html>"
        };

        const string outputPath = "CombinedOutput.pdf";

        try
        {
            Convert(htmlFragments, outputPath);
            Console.WriteLine($"PDF successfully created at '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}