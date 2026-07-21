using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Optimization;

class Program
{
    static void Main()
    {
        // Paths for the source XML (generated from a PDF) and the output compressed PDF.
        const string xmlInputPath  = "input.xml";
        const string pdfOutputPath = "compressed_output.pdf";

        // Verify the XML source exists.
        if (!File.Exists(xmlInputPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlInputPath}");
            return;
        }

        try
        {
            // Load the XML representation of the document.
            // XmlLoadOptions is the correct load option for XML files.
            XmlLoadOptions loadOptions = new XmlLoadOptions();
            using (Document doc = new Document(xmlInputPath, loadOptions))
            {
                // Prepare optimization options: enable Flate compression of object streams.
                // Setting CompressObjects to true packs PDF objects into compressed streams.
                OptimizationOptions opt = new OptimizationOptions
                {
                    CompressObjects = true
                };

                // Apply the optimization to the document.
                doc.OptimizeResources(opt);

                // Save the optimized document as PDF.
                doc.Save(pdfOutputPath);
            }

            Console.WriteLine($"Compressed PDF saved to '{pdfOutputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}