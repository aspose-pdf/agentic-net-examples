using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf; // Core PDF API – load option classes (e.g., XmlLoadOptions) are in this namespace

class XmlToPdfBatchProcessor
{
    /// <summary>
    /// Converts a collection of XML files to PDF files concurrently.
    /// Each XML file is loaded with XmlLoadOptions and saved as a PDF.
    /// The method creates a separate Document instance per file to guarantee thread safety.
    /// </summary>
    /// <param name="xmlFiles">Paths to source XML files.</param>
    /// <param name="outputDirectory">Directory where PDF files will be written.</param>
    public static void ConvertAll(IEnumerable<string> xmlFiles, string outputDirectory)
    {
        // Ensure the output directory exists
        Directory.CreateDirectory(outputDirectory);

        // Process files in parallel; each iteration works with its own Document instance
        Parallel.ForEach(xmlFiles, xmlPath =>
        {
            if (!File.Exists(xmlPath))
            {
                Console.Error.WriteLine($"File not found: {xmlPath}");
                return;
            }

            // Derive PDF file name from XML file name
            string pdfFileName = Path.GetFileNameWithoutExtension(xmlPath) + ".pdf";
            string pdfPath = Path.Combine(outputDirectory, pdfFileName);

            try
            {
                // Load XML using the correct load options (XmlLoadOptions resides in Aspose.Pdf)
                XmlLoadOptions loadOptions = new XmlLoadOptions();

                // Create a Document with XML source and load options – each thread gets its own instance
                using (Document pdfDocument = new Document(xmlPath, loadOptions))
                {
                    // Save the generated PDF
                    pdfDocument.Save(pdfPath);
                }

                Console.WriteLine($"Converted '{xmlPath}' → '{pdfPath}'");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{xmlPath}': {ex.Message}");
            }
        });
    }

    // Example entry point
    static void Main()
    {
        // Example list of XML files to convert
        List<string> xmlFiles = new List<string>
        {
            "input1.xml",
            "input2.xml",
            "input3.xml"
        };

        string outputDir = "GeneratedPdfs";

        ConvertAll(xmlFiles, outputDir);
    }
}
