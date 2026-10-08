using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Path to the PDF file (first argument or default)
        string pdfPath = args.Length > 0 ? args[0] : "input.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        try
        {
            // Load the PDF document first – PdfXmpMetadata expects a Document instance
            using (Document pdfDoc = new Document(pdfPath))
            {
                // Use the PdfXmpMetadata facade to retrieve the raw XMP packet
                using (PdfXmpMetadata xmpFacade = new PdfXmpMetadata(pdfDoc))
                {
                    // GetXmpMetadata returns a byte[]; convert it to a UTF‑8 string
                    byte[] xmpBytes = xmpFacade.GetXmpMetadata();
                    if (xmpBytes == null || xmpBytes.Length == 0)
                    {
                        Console.WriteLine("No XMP metadata found in the PDF.");
                        return;
                    }

                    string xmpXml = Encoding.UTF8.GetString(xmpBytes);

                    // Parse the XMP XML and collect declared namespaces
                    XDocument doc = XDocument.Parse(xmpXml);
                    var namespaces = doc.Root?
                                       .Attributes()
                                       .Where(a => a.IsNamespaceDeclaration)
                                       .Select(a => a.Value)
                                       .Distinct()
                                       .ToArray();

                    if (namespaces == null || namespaces.Length == 0)
                    {
                        Console.WriteLine("No XMP namespaces found in the PDF.");
                    }
                    else
                    {
                        Console.WriteLine("XMP namespaces present in the PDF:");
                        foreach (var ns in namespaces)
                        {
                            Console.WriteLine(ns);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}
