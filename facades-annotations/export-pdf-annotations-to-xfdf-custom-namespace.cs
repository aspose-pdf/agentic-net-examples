using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";          // source PDF with annotations
        const string xfdfPath = "annotations.xfdf";   // output XFDF file
        const string customNs = "http://example.com/customxfdf"; // enterprise namespace

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(pdfPath))
            {
                // PdfAnnotationEditor does not implement IDisposable, so a plain instance is fine
                PdfAnnotationEditor editor = new PdfAnnotationEditor();
                editor.BindPdf(doc);

                // Export all annotations to a memory stream using the overload that accepts a string[]
                using (MemoryStream ms = new MemoryStream())
                {
                    int startPage = 1;
                    int endPage   = doc.Pages.Count;
                    // Pass a null cast to string[] to select the correct overload (all annotation types)
                    editor.ExportAnnotationsXfdf(ms, startPage, endPage, (string[])null);
                    ms.Position = 0;

                    // Load the generated XFDF, inject the custom namespace, and write to file
                    XmlDocument xml = new XmlDocument();
                    xml.Load(ms);

                    XmlElement root = xml.DocumentElement;
                    if (root != null && string.IsNullOrEmpty(root.NamespaceURI))
                    {
                        // Add the custom namespace as the default xmlns attribute
                        root.SetAttribute("xmlns", customNs);
                    }

                    xml.Save(xfdfPath);
                }
            }

            Console.WriteLine($"Annotations exported to '{xfdfPath}' with namespace '{customNs}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
