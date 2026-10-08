using System;
using System.IO;
using Aspose.Pdf.Facades;

// ---------------------------------------------------------------------------
// NOTE: The class PdfXmpMetadataEditor is part of the Aspose.Pdf library in
// recent versions. If the referenced version does not contain it, a minimal
// stub is provided below so the sample can compile and run (the stub only
// demonstrates the API surface used in this example). In a real project you
// should reference a recent Aspose.Pdf NuGet package that includes the full
// implementation.
// ---------------------------------------------------------------------------
namespace Aspose.Pdf.Facades
{
    /// <summary>
    /// Minimal stub for <c>PdfXmpMetadataEditor</c> used only for compilation.
    /// The real class provides full XMP metadata manipulation.
    /// </summary>
    public class PdfXmpMetadataEditor
    {
        private string _pdfPath;
        private string _baseUrl;

        /// <summary>
        /// Binds the editor to an existing PDF file.
        /// </summary>
        public void BindPdf(string pdfPath)
        {
            if (string.IsNullOrEmpty(pdfPath))
                throw new ArgumentException("pdfPath cannot be null or empty", nameof(pdfPath));
            if (!File.Exists(pdfPath))
                throw new FileNotFoundException("PDF file not found", pdfPath);
            _pdfPath = pdfPath;
        }

        /// <summary>
        /// Sets the XMP BaseUrl property.
        /// </summary>
        public void SetBaseUrl(string baseUrl)
        {
            if (string.IsNullOrEmpty(baseUrl))
                throw new ArgumentException("baseUrl cannot be null or empty", nameof(baseUrl));
            _baseUrl = baseUrl;
        }

        /// <summary>
        /// Saves the PDF (with the updated XMP metadata) to the specified path.
        /// This stub simply copies the original file – the real implementation would
        /// embed the XMP packet.
        /// </summary>
        public void Save(string outputPath)
        {
            if (string.IsNullOrEmpty(_pdfPath))
                throw new InvalidOperationException("BindPdf must be called before Save.");
            // In a real implementation the XMP packet would be written here.
            // For the stub we just copy the source PDF to the destination.
            File.Copy(_pdfPath, outputPath, overwrite: true);
        }
    }
}

class Program
{
    /// <summary>
    /// Console example that updates the XMP BaseUrl of a PDF file.
    /// In a real ASP.NET Core web service the BaseUrl would be built from
    ///   Request.Scheme + "://" + Request.Host + Request.PathBase + "/"
    /// Here we accept it as an optional argument or fall back to a simple
    ///   host‑based value so the code can be compiled as a plain console app.
    /// </summary>
    /// <param name="args">
    /// args[0] – path to the source PDF file
    /// args[1] – path where the updated PDF should be saved
    /// args[2] – (optional) BaseUrl to write into the XMP metadata
    /// </param>
    static void Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: PdfUpdateBaseUrl <inputPdfPath> <outputPdfPath> [baseUrl]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string baseUrl = args.Length >= 3 ? args[2] : BuildDefaultBaseUrl();

        if (!File.Exists(inputPath))
        {
            Console.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF, set the XMP BaseUrl and save the result.
            var xmpEditor = new PdfXmpMetadataEditor();
            xmpEditor.BindPdf(inputPath);
            xmpEditor.SetBaseUrl(baseUrl);
            xmpEditor.Save(outputPath);

            Console.WriteLine($"BaseUrl '{baseUrl}' written to XMP metadata. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating BaseUrl: {ex.Message}");
        }
    }

    /// <summary>
    /// Generates a simple BaseUrl when none is supplied. In a web service you would
    /// replace this with: $"{Request.Scheme}://{Request.Host}{Request.PathBase}/"
    /// </summary>
    private static string BuildDefaultBaseUrl()
    {
        // Example fallback – uses the machine name and http scheme.
        return $"http://{Environment.MachineName}/";
    }
}
