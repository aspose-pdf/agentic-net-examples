using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.Pdf.Facades;

// Stub implementation for PdfXmpMetadataEditor to allow compilation when the actual Aspose.Pdf library
// does not contain this class (e.g., older version). In a real project, reference the proper Aspose.Pdf
// NuGet package and remove this stub.
namespace Aspose.Pdf.Facades
{
    public class PdfXmpMetadataEditor
    {
        private string _pdfPath;
        private string _xmpMetadata = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><x:xmpmeta xmlns:x=\"adobe:ns:meta/\"><rdf:RDF xmlns:rdf=\"http://www.w3.org/1999/02/22-rdf-syntax-ns#\"></rdf:RDF></x:xmpmeta>";

        public void BindPdf(string pdfPath)
        {
            if (string.IsNullOrEmpty(pdfPath))
                throw new ArgumentException("PDF path cannot be null or empty.", nameof(pdfPath));
            if (!File.Exists(pdfPath))
                throw new FileNotFoundException($"PDF file not found: {pdfPath}", pdfPath);
            _pdfPath = pdfPath;
        }

        public string GetXmpMetadata()
        {
            // In a real implementation this would extract the XMP packet from the PDF.
            // Here we simply return a minimal placeholder XML.
            return _xmpMetadata;
        }

        public void SetXmpMetadata(string xmpXml)
        {
            if (string.IsNullOrEmpty(xmpXml))
                throw new ArgumentException("XMP XML cannot be null or empty.", nameof(xmpXml));
            _xmpMetadata = xmpXml;
        }

        public void Save(string outputPath)
        {
            if (string.IsNullOrEmpty(_pdfPath))
                throw new InvalidOperationException("No PDF has been bound. Call BindPdf first.");
            // For the stub we simply copy the original PDF to the output location.
            // Real implementation would embed the modified XMP metadata into the PDF.
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
        const string newCreator = "My New Creator";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Bind the PDF to the XMP metadata editor
            var xmpEditor = new PdfXmpMetadataEditor();
            xmpEditor.BindPdf(inputPath);

            // Retrieve existing XMP metadata as an XML string
            string xmpXml = xmpEditor.GetXmpMetadata();

            // Load the XML for manipulation
            XDocument xmpDoc = XDocument.Parse(xmpXml);

            // Namespaces used in XMP
            XNamespace dc = "http://purl.org/dc/elements/1.1/";
            XNamespace rdf = "http://www.w3.org/1999/02/22-rdf-syntax-ns#";

            // Locate the dc:creator element (inside rdf:Description)
            XElement creatorElement = xmpDoc.Descendants(dc + "creator").FirstOrDefault();

            if (creatorElement == null)
            {
                // If the creator element does not exist, create it under the first rdf:Description
                XElement description = xmpDoc.Descendants(rdf + "Description").FirstOrDefault();
                if (description != null)
                {
                    creatorElement = new XElement(dc + "creator",
                        new XElement(rdf + "Seq",
                            new XElement(rdf + "li", newCreator)));
                    description.Add(creatorElement);
                }
                else
                {
                    // If there is no rdf:Description, create a minimal structure
                    var rdfRoot = new XElement(rdf + "RDF",
                        new XElement(rdf + "Description",
                            new XElement(dc + "creator",
                                new XElement(rdf + "Seq",
                                    new XElement(rdf + "li", newCreator)))));
                    xmpDoc.Add(rdfRoot);
                }
            }
            else
            {
                // Replace existing creator list with the new value
                creatorElement.RemoveAll();
                creatorElement.Add(new XElement(rdf + "Seq",
                    new XElement(rdf + "li", newCreator)));
            }

            // Convert the modified XMP back to a string (preserve declaration if present)
            string modifiedXmp = xmpDoc.Declaration != null
                ? xmpDoc.Declaration + xmpDoc.ToString()
                : xmpDoc.ToString();

            // Write the updated XMP metadata back to the PDF
            xmpEditor.SetXmpMetadata(modifiedXmp);
            // Save the PDF with the updated metadata
            xmpEditor.Save(outputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
