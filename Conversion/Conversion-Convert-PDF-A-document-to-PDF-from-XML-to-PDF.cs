using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        // Base directory for input and output files.
        string dataDir = Path.Combine(Directory.GetCurrentDirectory(), "Data");
        if (!Directory.Exists(dataDir))
        {
            Console.Error.WriteLine($"Data directory not found: {dataDir}");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Convert an XML file (PDFXML format) to a regular PDF document.
        // -----------------------------------------------------------------
        string xmlFile = Path.Combine(dataDir, "sample.xml");               // Input XML
        string pdfFromXml = Path.Combine(dataDir, "output-from-xml.pdf");   // Output PDF

        if (!File.Exists(xmlFile))
        {
            Console.Error.WriteLine($"XML source file not found: {xmlFile}");
        }
        else
        {
            try
            {
                // Load options for XML → PDF conversion.
                XmlLoadOptions xmlLoadOptions = new XmlLoadOptions();

                // Load the XML document using the specified options.
                using (Document xmlDoc = new Document(xmlFile, xmlLoadOptions))
                {
                    // Save the resulting PDF.
                    xmlDoc.Save(pdfFromXml);
                }

                Console.WriteLine($"XML successfully converted to PDF: {pdfFromXml}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error converting XML to PDF: {ex.Message}");
            }
        }

        // -----------------------------------------------------------------
        // 2. Convert a PDF/A document to a regular PDF document.
        // -----------------------------------------------------------------
        string pdfaFile = Path.Combine(dataDir, "sample-pdfa.pdf");          // Input PDF/A
        string pdfFromPdfa = Path.Combine(dataDir, "output-from-pdfa.pdf"); // Output PDF

        if (!File.Exists(pdfaFile))
        {
            Console.Error.WriteLine($"PDF/A source file not found: {pdfaFile}");
        }
        else
        {
            try
            {
                // Load the PDF/A document. No special load options are required.
                using (Document pdfaDoc = new Document(pdfaFile))
                {
                    // Save as a standard PDF. The same Save method works for any PDF version.
                    pdfaDoc.Save(pdfFromPdfa);
                }

                Console.WriteLine($"PDF/A successfully converted to PDF: {pdfFromPdfa}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error converting PDF/A to PDF: {ex.Message}");
            }
        }
    }
}