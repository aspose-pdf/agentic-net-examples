using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input XML file containing form data (XFA or AcroForm data)
        const string xmlDataPath = "formData.xml";

        // List of source PDF files to which the XML data will be applied
        var sourcePdfs = new List<string>
        {
            "document1.pdf",
            "document2.pdf",
            "document3.pdf"
        };

        // Directory where the synchronized PDFs will be saved
        const string outputDir = "SynchronizedPdfs";

        // Validate inputs
        if (!File.Exists(xmlDataPath))
        {
            Console.Error.WriteLine($"XML data file not found: {xmlDataPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Process each PDF in parallel to speed up synchronization
        Parallel.ForEach(sourcePdfs, pdfPath =>
        {
            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"Source PDF not found: {pdfPath}");
                return;
            }

            try
            {
                // Load the PDF document
                using (Document doc = new Document(pdfPath))
                {
                    // Bind the XML form data to the document.
                    // This imports the data into the form fields (AcroForm or XFA).
                    doc.BindXml(xmlDataPath);

                    // Build output file path preserving original name
                    string outputPath = Path.Combine(outputDir, Path.GetFileName(pdfPath));

                    // Save the updated PDF
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Synchronized PDF saved: {Path.GetFileName(pdfPath)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        });
    }
}