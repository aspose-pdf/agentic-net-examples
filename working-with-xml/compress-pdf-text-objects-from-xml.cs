using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Optimization;

class Program
{
    static void Main()
    {
        // Input XML file that will be converted to PDF
        const string xmlPath = "input.xml";
        // Output PDF file with compressed text objects
        const string pdfPath = "output.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load XML using the proper load options (required for XML sources)
        XmlLoadOptions loadOptions = new XmlLoadOptions();

        // Create the PDF document from the XML content
        using (Document pdfDocument = new Document(xmlPath, loadOptions))
        {
            // Prepare optimization options: enable compression of PDF objects
            OptimizationOptions optOptions = new OptimizationOptions
            {
                CompressObjects = true   // Pack objects into streams and compress them
            };

            // Apply the optimization to the document
            pdfDocument.OptimizeResources(optOptions);

            // Save the optimized PDF
            pdfDocument.Save(pdfPath);
        }

        Console.WriteLine($"PDF generated and compressed: {pdfPath}");
    }
}