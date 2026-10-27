using System;
using System.IO;
using System.Collections.Generic;
using System.Xml;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Folder containing PDFs to validate
        const string inputFolder = "PdfFiles";
        // Folder where XML logs will be written
        const string logFolder = "ValidationLogs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(logFolder);

        // Store compliance results per file
        var complianceResults = new Dictionary<string, double>();

        // Process each PDF file in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileName(pdfPath);
            string logPath = Path.Combine(logFolder, Path.ChangeExtension(fileName, ".xml"));

            // Load PDF inside a using block for deterministic disposal
            using (Document doc = new Document(pdfPath))
            {
                // Validate the PDF for PDF/UA compliance. The Validate method writes a detailed
                // validation report to the supplied logPath and returns a boolean indicating
                // overall compliance.
                bool isCompliant = doc.Validate(logPath, PdfFormat.PDF_UA_1);

                // For the purpose of the dashboard we treat a fully compliant document as 100%
                // and a non‑compliant one as 0%.
                double compliance = isCompliant ? 100.0 : 0.0;
                complianceResults[fileName] = compliance;

                // Augment the generated log with a simple compliance element (optional).
                // If you prefer to keep the original Aspose log untouched, you can skip this
                // additional write step.
                XmlWriterSettings settings = new XmlWriterSettings { Indent = true };
                using (XmlWriter writer = XmlWriter.Create(logPath, settings))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("ValidationLog");
                    writer.WriteAttributeString("File", fileName);
                    writer.WriteElementString("CompliancePercentage", compliance.ToString("F2"));
                    writer.WriteEndElement(); // ValidationLog
                    writer.WriteEndDocument();
                }
            }

            Console.WriteLine($"Validated {fileName}, compliance: {complianceResults[fileName]:F2}% (log: {logPath})");
        }

        // Generate a simple dashboard on the console
        Console.WriteLine();
        Console.WriteLine("=== Validation Dashboard ===");
        double total = 0;
        foreach (var kvp in complianceResults)
        {
            Console.WriteLine($"{kvp.Key}: {kvp.Value:F2}% compliant");
            total += kvp.Value;
        }

        if (complianceResults.Count > 0)
        {
            double average = total / complianceResults.Count;
            Console.WriteLine($"Overall average compliance: {average:F2}%");
        }
        else
        {
            Console.WriteLine("No PDF files were processed.");
        }
    }
}
