using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF (the invoice) and ZUGFeRD XML file
        const string pdfPath = "invoice.pdf";
        const string xmlPath = "invoice.xml";
        const string outputPath = "invoice_zugferd.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML not found: {xmlPath}");
            return;
        }

        // Load the existing PDF document
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Create a FileSpecification for the XML file to embed
            // The constructor that takes a file path automatically reads the file data.
            FileSpecification fileSpec = new FileSpecification(xmlPath)
            {
                Name = Path.GetFileName(xmlPath),          // File name inside the PDF
                Description = "ZUGFeRD Invoice XML"       // Optional description
            };

            // Optionally set the modification date (good practice)
            fileSpec.Params.ModDate = DateTime.UtcNow;

            // Add the file specification (embedded file) to the PDF
            pdfDoc.EmbeddedFiles.Add(fileSpec);

            // Verify that the XML was embedded
            bool found = false;
            foreach (FileSpecification ef in pdfDoc.EmbeddedFiles)
            {
                if (ef.Name.Equals(fileSpec.Name, StringComparison.OrdinalIgnoreCase))
                {
                    found = true;
                    long dataLength = 0;
                    // Prefer the size reported in the Params dictionary
                    if (ef.Params != null && ef.Params.Size > 0)
                    {
                        dataLength = ef.Params.Size;
                    }
                    else if (ef.Contents != null)
                    {
                        // Fallback: read the stream to determine length
                        using (var ms = new MemoryStream())
                        {
                            ef.Contents.CopyTo(ms);
                            dataLength = ms.Length;
                        }
                    }
                    Console.WriteLine($"Embedded file found: {ef.Name}, size {dataLength} bytes");
                    break;
                }
            }

            if (!found)
                Console.WriteLine("Embedded file not found after insertion.");

            // Save the PDF with the embedded ZUGFeRD XML
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"PDF with embedded ZUGFeRD saved to '{outputPath}'.");
    }
}
