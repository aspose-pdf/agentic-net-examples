using System;
using System.IO;
using Aspose.Pdf;

namespace PdfGenerationLogger
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create sample XML files (evaluation mode limits to 4 files)
            string[] xmlFiles = new string[2];
            for (int i = 0; i < xmlFiles.Length; i++)
            {
                string xmlFileName = $"sample{i + 1}.xml";
                xmlFiles[i] = xmlFileName;
                string xmlContent = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n<Document>\n    <Paragraph>Hello from XML file " + (i + 1) + "</Paragraph>\n</Document>";
                File.WriteAllText(xmlFileName, xmlContent);
                Console.WriteLine($"Created XML file: {xmlFileName}");
            }

            // Process each XML file
            foreach (string xmlPath in xmlFiles)
            {
                Console.WriteLine($"\n--- Processing '{xmlPath}' ---");
                string pdfPath = Path.ChangeExtension(xmlPath, ".pdf");
                string logPath = Path.ChangeExtension(xmlPath, ".log");

                try
                {
                    using (Document pdfDocument = new Document())
                    {
                        Console.WriteLine("Binding XML to PDF document...");
                        pdfDocument.BindXml(xmlPath);
                        Console.WriteLine("Saving PDF to file...");
                        pdfDocument.Save(pdfPath);
                        Console.WriteLine($"PDF saved as '{pdfPath}'.");

                        Console.WriteLine("Validating PDF against PDF/A-1B...");
                        bool isValid = pdfDocument.Validate(logPath, PdfFormat.PDF_A_1B);
                        Console.WriteLine($"Validation result: {(isValid ? "Valid" : "Invalid")}");
                        Console.WriteLine($"Validation log written to '{logPath}'.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing '{xmlPath}': {ex.Message}");
                }
            }

            Console.WriteLine("\nProcessing completed.");
        }
    }
}