using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Optimization;

class Program
{
    static void Main()
    {
        // Paths for the source XML and the resulting compressed PDF
        const string xmlPath   = "source.xml";
        const string pdfPath   = "generated.pdf";
        const string outputPdf = "generated_compressed.pdf";

        // Verify that the XML source exists
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        try
        {
            // ------------------------------------------------------------
            // 1. Generate a PDF document from the XML source.
            //    Document.BindXml loads the XML and creates the PDF structure.
            // ------------------------------------------------------------
            using (Document doc = new Document())
            {
                doc.BindXml(xmlPath);               // Load XML and build PDF
                doc.Save(pdfPath);                  // Save the initial PDF

                // --------------------------------------------------------
                // 2. Apply stream compression to reduce file size.
                //    OptimizationOptions.CompressObjects packs objects into
                //    compressed streams.
                // --------------------------------------------------------
                OptimizationOptions opt = new OptimizationOptions
                {
                    CompressObjects = true          // Enable object stream compression
                };

                doc.OptimizeResources(opt);         // Apply the optimization strategy

                // --------------------------------------------------------
                // 3. Save the compressed PDF.
                // --------------------------------------------------------
                doc.Save(outputPdf);
            }

            Console.WriteLine($"Compressed PDF saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}