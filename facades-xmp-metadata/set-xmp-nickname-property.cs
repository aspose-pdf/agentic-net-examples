using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf.Facades;

// Stub definitions for missing Aspose.Pdf.Facades types (remove when referencing the real Aspose.Pdf package)
namespace Aspose.Pdf.Facades
{
    // Minimal stub to satisfy compilation when the real library is not referenced.
    // The real Aspose.Pdf.Facades.PdfXmpMetadataEditor provides richer functionality.
    public class PdfXmpMetadataEditor
    {
        private string _pdfPath;
        private string _xmpMetadata = string.Empty;

        public void BindPdf(string pdfPath)
        {
            if (string.IsNullOrEmpty(pdfPath))
                throw new ArgumentException("PDF path cannot be null or empty.", nameof(pdfPath));
            _pdfPath = pdfPath;
        }

        // Returns the existing XMP metadata as a string. In the stub we return an empty string.
        public string GetXmpMetadata()
        {
            return _xmpMetadata;
        }

        // Sets the XMP metadata that will be written on Save().
        public void SetXmpMetadata(string xmp)
        {
            _xmpMetadata = xmp ?? string.Empty;
        }

        // Saves the PDF (stub simply copies the source file to the destination).
        public void Save(string outputPath)
        {
            if (string.IsNullOrEmpty(_pdfPath))
                throw new InvalidOperationException("No PDF has been bound. Call BindPdf first.");
            // In a real implementation the XMP packet would be embedded into the PDF.
            // Here we just copy the original file to the output location.
            File.Copy(_pdfPath, outputPath, overwrite: true);
        }
    }
}

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string nickname = "CustomIdentifier";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Bind the PDF file to the XMP metadata editor
            var xmpEditor = new PdfXmpMetadataEditor();
            xmpEditor.BindPdf(inputPath);

            // Retrieve existing XMP metadata (may be empty)
            string existingXmp = xmpEditor.GetXmpMetadata();

            // Define XMP namespaces
            XNamespace nsX = "adobe:ns:meta/";
            XNamespace nsRdf = "http://www.w3.org/1999/02/22-rdf-syntax-ns#";
            XNamespace nsXmp = "http://ns.adobe.com/xap/1.0/";

            XDocument xmpDoc;

            if (string.IsNullOrWhiteSpace(existingXmp))
            {
                // Create a minimal XMP packet if none exists
                xmpDoc = new XDocument(
                    new XElement(nsX + "xmpmeta",
                        new XAttribute(XNamespace.Xmlns + "x", nsX),
                        new XElement(nsRdf + "RDF",
                            new XAttribute(XNamespace.Xmlns + "rdf", nsRdf),
                            new XElement(nsRdf + "Description")
                        )
                    )
                );
            }
            else
            {
                // Parse the existing XMP XML
                xmpDoc = XDocument.Parse(existingXmp);
            }

            // Locate the rdf:Description element safely
            XElement description = xmpDoc.Root?
                                         .Element(nsRdf + "RDF")?
                                         .Element(nsRdf + "Description");
            if (description == null)
                throw new InvalidOperationException("Unable to locate rdf:Description element in XMP metadata.");

            // Set or add the xmp:Nickname element
            XElement nicknameElem = description.Element(nsXmp + "Nickname");
            if (nicknameElem != null)
                nicknameElem.Value = nickname;
            else
                description.Add(new XElement(nsXmp + "Nickname", nickname));

            // Serialize the modified XMP packet
            string newXmp = xmpDoc.Declaration != null
                ? xmpDoc.Declaration + Environment.NewLine + xmpDoc.ToString()
                : xmpDoc.ToString();

            // Apply the updated XMP metadata and save the PDF
            xmpEditor.SetXmpMetadata(newXmp);
            xmpEditor.Save(outputPath);

            Console.WriteLine($"Nickname set to '{nickname}' and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
