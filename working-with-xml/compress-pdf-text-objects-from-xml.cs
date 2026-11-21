using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text; // <-- added to resolve TextFragment
using Aspose.Pdf.Optimization;

class Program
{
    static void Main()
    {
        const string xmlPath = "input.xml";   // XML that defines PDF content
        const string outputPdf = "output.pdf"; // Destination compressed PDF

        // Verify the XML source exists
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        try
        {
            // Document lifecycle must be wrapped in a using block (rule: document-disposal-with-using)
            using (Document pdfDoc = new Document())
            {
                // Load the XML and translate it into PDF objects using the core API.
                // This replaces the removed Document.LoadXml method.
                XDocument xDoc = XDocument.Load(xmlPath);
                Page page = pdfDoc.Pages.Add();

                // Simple example: each element's inner text becomes a separate paragraph.
                // Adjust the parsing logic to match the actual XML schema you use.
                foreach (XElement element in xDoc.Descendants())
                {
                    if (!string.IsNullOrWhiteSpace(element.Value))
                    {
                        TextFragment tf = new TextFragment(element.Value.Trim());
                        // Optional: you can style the fragment here (font, size, color, etc.)
                        page.Paragraphs.Add(tf);
                    }
                }

                // Apply high‑level resource optimization (stream compression, unused object removal, etc.)
                OptimizationOptions opt = OptimizationOptions.All();
                opt.CompressImages = true;          // compress images if any
                opt.ImageQuality = 75;              // adjust quality as needed
                opt.RemoveUnusedObjects = true;    // drop objects not referenced
                opt.RemoveUnusedStreams = true;    // drop empty streams
                opt.SubsetFonts = true;             // embed only used glyphs
                pdfDoc.OptimizeResources(opt);

                // Save the optimized PDF
                pdfDoc.Save(outputPdf);
            }

            Console.WriteLine($"Compressed PDF saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
