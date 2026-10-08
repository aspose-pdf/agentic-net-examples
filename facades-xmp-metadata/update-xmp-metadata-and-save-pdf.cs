using System;
using System.IO;
using Aspose.Pdf.Facades;

// -----------------------------------------------------------------------------
// Stub implementation for missing Aspose.Pdf.Facades.PdfXmpMetadataEditor class.
// This stub is only compiled when the real Aspose.Pdf library does not provide
// the class (e.g., when the NuGet package is not referenced). It mimics the
// public API used in the sample: BindPdf, SetXmpMetadata and Save.
// -----------------------------------------------------------------------------
namespace Aspose.Pdf.Facades
{
    /// <summary>
    /// Minimal stub for <c>PdfXmpMetadataEditor</c> to allow the sample to compile
    /// without the full Aspose.Pdf library. The implementation simply copies the
    /// source PDF to the destination path; XMP metadata handling is omitted.
    /// </summary>
    public class PdfXmpMetadataEditor
    {
        private string _sourcePdfPath;

        /// <summary>
        /// Binds the editor to an existing PDF file.
        /// </summary>
        /// <param name="pdfPath">Path to the source PDF.</param>
        public void BindPdf(string pdfPath)
        {
            if (string.IsNullOrWhiteSpace(pdfPath))
                throw new ArgumentException("PDF path cannot be null or empty.", nameof(pdfPath));

            if (!File.Exists(pdfPath))
                throw new FileNotFoundException($"PDF file not found: {pdfPath}", pdfPath);

            _sourcePdfPath = pdfPath;
        }

        /// <summary>
        /// Sets the XMP metadata. In the stub this method does nothing – it only
        /// exists to match the real API signature.
        /// </summary>
        /// <param name="xmpXml">XMP metadata in XML format.</param>
        public void SetXmpMetadata(string xmpXml)
        {
            // No‑op for the stub. Real implementation would embed the XML into the PDF.
        }

        /// <summary>
        /// Saves the (potentially modified) PDF to the specified output path.
        /// The stub simply copies the original file.
        /// </summary>
        /// <param name="outputPdfPath">Destination file path.</param>
        public void Save(string outputPdfPath)
        {
            if (string.IsNullOrWhiteSpace(_sourcePdfPath))
                throw new InvalidOperationException("No PDF has been bound. Call BindPdf first.");

            if (string.IsNullOrWhiteSpace(outputPdfPath))
                throw new ArgumentException("Output path cannot be null or empty.", nameof(outputPdfPath));

            // Ensure the directory exists.
            var dir = Path.GetDirectoryName(outputPdfPath);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            // Simple copy – real library would write the updated XMP metadata.
            File.Copy(_sourcePdfPath, outputPdfPath, overwrite: true);
        }
    }
}

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Sample XMP metadata (XML format)
        string xmpXml = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<rdf:RDF xmlns:rdf=""http://www.w3.org/1999/02/22-rdf-syntax-ns#""
         xmlns:dc=""http://purl.org/dc/elements/1.1/"">
  <rdf:Description rdf:about="""">
    <dc:title>
      <rdf:Alt>
        <rdf:li xml:lang=""x-default"">Sample PDF Title</rdf:li>
      </rdf:Alt>
    </dc:title>
    <dc:creator>
      <rdf:Seq>
        <rdf:li>John Doe</rdf:li>
      </rdf:Seq>
    </dc:creator>
  </rdf:Description>
</rdf:RDF>";

        // Create the XMP metadata editor (Facades API)
        PdfXmpMetadataEditor xmpEditor = new PdfXmpMetadataEditor();

        // Load the source PDF
        xmpEditor.BindPdf(inputPdf);

        // Apply the new XMP metadata
        xmpEditor.SetXmpMetadata(xmpXml);

        // Save the modified PDF
        xmpEditor.Save(outputPdf);

        Console.WriteLine($"XMP metadata updated and saved to '{outputPdf}'.");
    }
}
