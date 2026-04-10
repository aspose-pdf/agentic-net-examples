using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Aspose.Pdf;

class HtmlToPdfConverter
{
    /// <summary>
    /// Creates a PDF file from a collection of HTML strings using custom rendering options.
    /// </summary>
    /// <param name="htmlFragments">HTML fragments to be combined into a single document.</param>
    /// <param name="outputPdfPath">Path where the resulting PDF will be saved.</param>
    public static void Convert(IEnumerable<string> htmlFragments, string outputPdfPath)
    {
        // Combine all HTML fragments into one HTML document.
        // A simple concatenation is sufficient for this example.
        string combinedHtml = string.Join("\n", htmlFragments);

        // Encode the combined HTML into a UTF‑8 byte array.
        byte[] htmlBytes = Encoding.UTF8.GetBytes(combinedHtml);

        // Prepare custom load options for HTML → PDF conversion.
        HtmlLoadOptions loadOptions = new HtmlLoadOptions
        {
            // Example custom options – adjust as needed.
            IsEmbedFonts = true,                 // Embed fonts into the PDF.
            IsRenderToSinglePage = false,        // Render each HTML page as a separate PDF page.
            // Additional options can be set here, e.g. BasePath, PageInfo, etc.
        };

        // Load the HTML from a memory stream using the specified options.
        using (MemoryStream htmlStream = new MemoryStream(htmlBytes))
        using (Document pdfDocument = new Document(htmlStream, loadOptions))
        {
            // Save the resulting PDF using a FileStream overload to avoid file‑locking issues.
            using (FileStream outputStream = new FileStream(outputPdfPath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                pdfDocument.Save(outputStream);
            }
        }
    }

    // Example usage.
    static void Main()
    {
        var htmlParts = new List<string>
        {
            "<html><body><h1>First Section</h1><p>This is the first part.</p></body></html>",
            "<html><body><h2>Second Section</h2><p>This is the second part.</p></body></html>"
        };

        string outputPath = "CombinedOutput.pdf";

        Convert(htmlParts, outputPath);

        Console.WriteLine($"PDF created at '{outputPath}'.");
    }
}
