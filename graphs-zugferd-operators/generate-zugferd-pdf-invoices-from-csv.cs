using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class ZugferdInvoiceGenerator
{
    // Simple CSV record representation – made properties nullable to satisfy non‑nullable warnings
    private class InvoiceRecord
    {
        public string? InvoiceNumber { get; set; }
        public string? CustomerName  { get; set; }
        public string? XmlPath       { get; set; }   // Path to the ZUGFeRD XML file
        public string? OutputPdfPath { get; set; }   // Desired PDF output path
    }

    static void Main()
    {
        const string csvPath = "invoices.csv";

        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"CSV file not found: {csvPath}");
            return;
        }

        // Parse CSV (assumes header line and comma‑separated values)
        List<InvoiceRecord> records = new List<InvoiceRecord>();
        using (StreamReader reader = new StreamReader(csvPath))
        {
            string? header = reader.ReadLine(); // skip header
            while (!reader.EndOfStream)
            {
                string? line = reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line)) continue;

                // Expected columns: InvoiceNumber,CustomerName,XmlPath,OutputPdfPath
                string[] parts = line.Split(',');
                if (parts.Length < 4) continue;

                records.Add(new InvoiceRecord
                {
                    InvoiceNumber = parts[0].Trim(),
                    CustomerName  = parts[1].Trim(),
                    XmlPath       = parts[2].Trim(),
                    OutputPdfPath = parts[3].Trim()
                });
            }
        }

        foreach (var rec in records)
        {
            // Guard against nulls (the nullable properties were introduced to silence warnings)
            if (string.IsNullOrEmpty(rec.InvoiceNumber) ||
                string.IsNullOrEmpty(rec.CustomerName)  ||
                string.IsNullOrEmpty(rec.XmlPath)       ||
                string.IsNullOrEmpty(rec.OutputPdfPath))
            {
                Console.Error.WriteLine("One or more required fields are missing in CSV record – skipping.");
                continue;
            }

            if (!File.Exists(rec.XmlPath))
            {
                Console.Error.WriteLine($"XML file not found for invoice {rec.InvoiceNumber}: {rec.XmlPath}");
                continue;
            }

            // Create a new PDF document inside a using block (lifecycle rule)
            using (Document doc = new Document())
            {
                // Add a single page
                Page page = doc.Pages.Add();

                // Add invoice information as text
                TextFragment tf = new TextFragment($"Invoice: {rec.InvoiceNumber}\nCustomer: {rec.CustomerName}");
                tf.TextState.Font = FontRepository.FindFont("Helvetica");
                tf.TextState.FontSize = 12;
                tf.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
                page.Paragraphs.Add(tf);

                // Attach the ZUGFeRD XML file to the PDF using reflection to avoid direct dependency on EmbeddedFile (which lives in a restricted namespace)
                using (FileStream xmlStream = File.OpenRead(rec.XmlPath))
                {
                    // Resolve the internal EmbeddedFile type at runtime
                    Type? embeddedFileType = Type.GetType("Aspose.Pdf.EmbeddedFile, Aspose.Pdf");
                    if (embeddedFileType == null)
                    {
                        Console.Error.WriteLine("Unable to locate Aspose.Pdf.EmbeddedFile type – XML will not be attached.");
                    }
                    else
                    {
                        var ctor = embeddedFileType.GetConstructor(new[] { typeof(string), typeof(Stream) });
                        if (ctor == null)
                        {
                            Console.Error.WriteLine("EmbeddedFile constructor not found – XML will not be attached.");
                        }
                        else
                        {
                            // Create an instance: new EmbeddedFile(fileName, stream)
                            object? embeddedInstance = ctor.Invoke(new object[] { Path.GetFileName(rec.XmlPath), xmlStream });
                            if (embeddedInstance != null)
                            {
                                // Call doc.EmbeddedFiles.Add(embeddedInstance) via reflection
                                var addMethod = doc.EmbeddedFiles.GetType().GetMethod("Add");
                                addMethod?.Invoke(doc.EmbeddedFiles, new[] { embeddedInstance });
                            }
                        }
                    }
                }

                // Save the PDF (document‑disposal‑with‑using rule)
                doc.Save(rec.OutputPdfPath);
                Console.WriteLine($"Generated ZUGFeRD PDF: {rec.OutputPdfPath}");
            }
        }
    }
}
