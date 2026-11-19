using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Optimization; // for OptimizationOptions

class Program
{
    static void Main()
    {
        const string xmlPath = "input.xml";
        const string pdfPath = "output.pdf";
        const int imageDpi = 300; // Desired image resolution in DPI

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"File not found: {xmlPath}");
            return;
        }

        try
        {
            // Load the PDF content from an XML source
            XmlLoadOptions loadOptions = new XmlLoadOptions();
            using (Document doc = new Document(xmlPath, loadOptions))
            {
                // Configure optimization options to control the DPI of raster images
                var opt = new OptimizationOptions();
                opt.ImageCompressionOptions.CompressImages = true;
                // MaxResolution limits the DPI of images when the document is saved
                opt.ImageCompressionOptions.MaxResolution = imageDpi;

                // Apply the optimization (this will down‑sample images to the requested DPI)
                doc.OptimizeResources(opt);

                // Save the document as PDF (no PdfSaveOptions needed for image DPI)
                doc.Save(pdfPath);
            }

            Console.WriteLine($"PDF saved to '{pdfPath}' with image resolution limited to {imageDpi} DPI.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
