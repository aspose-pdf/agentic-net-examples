using System;
using System.Diagnostics;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class ExportAnnotationsTests
{
    static void Main()
    {
        // Create a simple PDF document with a single blank page (no annotations)
        using (Document doc = new Document())
        {
            // Add an empty page – this ensures the document is valid but contains no annotations
            doc.Pages.Add();

            // Initialize the PdfAnnotationEditor facade and bind it to the document
            PdfAnnotationEditor editor = new PdfAnnotationEditor();
            editor.BindPdf(doc);

            // Export annotations to an in‑memory stream (XFDF format)
            using (MemoryStream xfdfStream = new MemoryStream())
            {
                editor.ExportAnnotationsToXfdf(xfdfStream);

                // Reset stream position for reading
                xfdfStream.Position = 0;

                // Load the XFDF XML to verify its structure
                XDocument xfdfXml = XDocument.Load(xfdfStream);

                // The root element of a valid XFDF file must be <xfdf>
                Debug.Assert(xfdfXml.Root != null && xfdfXml.Root.Name.LocalName.Equals("xfdf", StringComparison.OrdinalIgnoreCase),
                    "Exported XFDF does not contain a valid <xfdf> root element.");

                // When there are no annotations, the <annots> element should be absent or empty
                var annotsElement = xfdfXml.Root.Element("annots");
                Debug.Assert(annotsElement == null || !annotsElement.HasElements,
                    "XFDF should not contain any annotation entries for a document without annotations.");

                // Additional sanity check: the stream should contain data
                Debug.Assert(xfdfStream.Length > 0, "Exported XFDF stream is empty.");

                Console.WriteLine("ExportAnnotationsToXfdf test passed – valid XFDF generated for a PDF with no annotations.");
            }

            // Clean up the editor (Dispose is optional as it does not hold unmanaged resources)
            editor.Close();
        }
    }
}