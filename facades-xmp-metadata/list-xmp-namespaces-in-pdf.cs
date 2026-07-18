using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Input PDF path – either first command‑line argument or a default file name
        string inputPath = args.Length > 0 ? args[0] : "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfXmpMetadata implements IDisposable, so use a using block for deterministic cleanup
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            // Bind the PDF file to the facade
            xmp.BindPdf(inputPath);

            // Retrieve the raw XMP metadata as a byte array
            byte[] rawData = xmp.GetXmpMetadata();

            if (rawData == null || rawData.Length == 0)
            {
                Console.WriteLine("No XMP metadata found in the PDF.");
                return;
            }

            // Convert the byte array to a UTF‑8 string containing the XML
            string xmlContent = Encoding.UTF8.GetString(rawData);

            // Parse the XML using LINQ to XML
            XDocument xmpDoc = XDocument.Parse(xmlContent);

            // Namespace declarations are represented by attributes where IsNamespaceDeclaration == true
            var namespaceAttributes = xmpDoc.Root
                                            .Attributes()
                                            .Where(a => a.IsNamespaceDeclaration);

            Console.WriteLine("XMP Namespaces present in the PDF:");
            foreach (var nsAttr in namespaceAttributes)
            {
                // The default namespace has the name "xmlns", otherwise it's "xmlns:prefix"
                string prefix = nsAttr.Name.LocalName == "xmlns" ? "(default)" : nsAttr.Name.LocalName;
                Console.WriteLine($"  Prefix: {prefix}, URI: {nsAttr.Value}");
            }
        }
    }
}