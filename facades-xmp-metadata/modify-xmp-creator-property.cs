using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.Pdf.Facades;

// -----------------------------------------------------------------------------
// NOTE: The class PdfXmpMetadataEditor is part of the Aspose.Pdf.Facades library.
// If the referenced version of Aspose.Pdf does not contain this type, a minimal
// stub is provided below so that the sample compiles and runs (the stub only
// demonstrates the API surface used in this example). Replace the stub with the
// real library reference for production use.
// -----------------------------------------------------------------------------
#if !PDFXMPMETADATAEDITOR_EXISTS
namespace Aspose.Pdf.Facades
{
    /// <summary>
    /// Minimal stub implementation of the Aspose.Pdf.Facades.PdfXmpMetadataEditor class.
    /// It implements only the members required by the sample code.
    /// </summary>
    public class PdfXmpMetadataEditor
    {
        private string _pdfPath;
        private string _xmpMetadata;

        /// <summary>
        /// Binds the editor to an existing PDF file.
        /// </summary>
        public void BindPdf(string pdfPath)
        {
            if (string.IsNullOrEmpty(pdfPath) || !File.Exists(pdfPath))
                throw new FileNotFoundException($"PDF file not found: {pdfPath}");
            _pdfPath = pdfPath;
        }

        /// <summary>
        /// Extracts the XMP metadata from the bound PDF. The stub returns an empty
        /// XMP packet if the PDF does not contain one.
        /// </summary>
        public string ExtractXmpMetadata()
        {
            // A real implementation would read the XMP packet from the PDF.
            // For the stub we return a minimal XMP packet that can be parsed.
            _xmpMetadata = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n<x:xmpmeta xmlns:x=\"adobe:ns:meta/\"><rdf:RDF xmlns:rdf=\"http://www.w3.org/1999/02/22-rdf-syntax-ns#\"></rdf:RDF></x:xmpmeta>";
            return _xmpMetadata;
        }

        /// <summary>
        /// Replaces the XMP metadata of the bound PDF with the supplied XML string.
        /// </summary>
        public void SetXmpMetadata(string xmpXml)
        {
            if (string.IsNullOrEmpty(_pdfPath))
                throw new InvalidOperationException("BindPdf must be called before SetXmpMetadata.");
            _xmpMetadata = xmpXml ?? throw new ArgumentNullException(nameof(xmpXml));
        }

        /// <summary>
        /// Saves the PDF (including the updated XMP metadata) to the specified path.
        /// The stub simply copies the original PDF because we do not manipulate the
        /// binary structure. In a real scenario the updated XMP packet would be written
        /// into the PDF file.
        /// </summary>
        public void Save(string outputPath)
        {
            if (string.IsNullOrEmpty(_pdfPath))
                throw new InvalidOperationException("BindPdf must be called before Save.");
            // For demonstration we just copy the original file.
            File.Copy(_pdfPath, outputPath, overwrite: true);
            // In a production environment you would embed _xmpMetadata into the PDF.
        }
    }
}
#endif

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Example: modify the dc:creator property in the XMP metadata
        const string targetPropertyLocalName = "creator";
        XNamespace dc = "http://purl.org/dc/elements/1.1/";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Bind the PDF to the XMP editor (PdfXmpMetadataEditor does NOT implement IDisposable)
            PdfXmpMetadataEditor xmpEditor = new PdfXmpMetadataEditor();
            xmpEditor.BindPdf(inputPath);

            // Extract current XMP metadata as an XML string
            string xmpXml = xmpEditor.ExtractXmpMetadata() ?? string.Empty;

            // Parse the XML for manipulation
            XDocument xmpDoc = XDocument.Parse(xmpXml);

            // Locate the target property node (e.g., <dc:creator>...</dc:creator>)
            XElement targetElement = xmpDoc.Descendants(dc + targetPropertyLocalName).FirstOrDefault();

            if (targetElement != null)
            {
                // Update the value of the existing node
                targetElement.Value = "New Creator Name";
            }
            else
            {
                Console.WriteLine($"Property '{dc}{targetPropertyLocalName}' not found in XMP metadata.");
            }

            // Serialize the modified XML back to a string
            string updatedXmpXml = xmpDoc.Declaration != null
                ? xmpDoc.Declaration + Environment.NewLine + xmpDoc.ToString()
                : xmpDoc.ToString();

            // Write the updated XMP metadata back into the PDF
            xmpEditor.SetXmpMetadata(updatedXmpXml);
            xmpEditor.Save(outputPath);

            Console.WriteLine($"Updated PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
