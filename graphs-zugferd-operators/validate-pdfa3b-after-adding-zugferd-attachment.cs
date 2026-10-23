using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";      // existing PDF/A‑3 document
        const string zugferdXmlPath = "invoice.xml";   // ZUGFeRD XML file to embed
        const string outputPdfPath  = "output_with_zugferd.pdf";
        const string validationLogPath = "validation.log"; // log file for PDF/A validation

        // Verify input files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(zugferdXmlPath))
        {
            Console.Error.WriteLine($"ZUGFeRD XML not found: {zugferdXmlPath}");
            return;
        }

        // Load the PDF/A‑3 document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Create a FileSpecification for the ZUGFeRD XML and embed it
            using (FileStream xmlStream = File.OpenRead(zugferdXmlPath))
            {
                var zugferdSpec = new FileSpecification(xmlStream, Path.GetFileName(zugferdXmlPath), "ZUGFeRD invoice data")
                {
                    // Set MIME type and relationship (AFRelationship.Data indicates attached data)
                    MIMEType      = "application/xml",
                    AFRelationship = AFRelationship.Data
                };

                // Add the file specification to the EmbeddedFiles collection (required for PDF/A‑3)
                doc.EmbeddedFiles.Add(zugferdSpec);
            }

            // Run PDF/A‑3U compliance check. The Validate method returns a bool and writes details to a log file.
            bool isCompliant = doc.Validate(validationLogPath, PdfFormat.PDF_A_3U);

            // Output compliance result
            if (isCompliant)
            {
                Console.WriteLine("Document remains PDF/A‑3U compliant after adding ZUGFeRD attachment.");
            }
            else
            {
                Console.WriteLine("Document is NOT PDF/A‑3U compliant after adding ZUGFeRD attachment.");
                Console.WriteLine("Compliance errors (from log file):");
                if (File.Exists(validationLogPath))
                {
                    foreach (var line in File.ReadAllLines(validationLogPath))
                    {
                        Console.WriteLine($"- {line}");
                    }
                }
                else
                {
                    Console.WriteLine("- No log file was generated.");
                }
            }

            // Save the modified PDF (still a PDF, so no special SaveOptions needed)
            doc.Save(outputPdfPath);
            Console.WriteLine($"Modified PDF saved to '{outputPdfPath}'.");
        }
    }
}
