using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string pdfPath = "invoice_zugferd.pdf";
        const string xmlOutputPath = "extracted_invoice.xml";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load the ZUGFeRD PDF. No special LoadOptions are required for standard PDFs.
        using (Document doc = new Document(pdfPath))
        {
            // ZUGFeRD embeds the invoice XML as an associated file.
            // The PDF catalog entry "AF" (Associated Files) holds references to these files.
            // GetCatalogValue returns the raw object; we need to cast it to the appropriate type.
            // In Aspose.Pdf the associated files are exposed as an array of FileSpecification objects.
            // The following code demonstrates the typical extraction pattern.

            object afEntry = doc.GetCatalogValue("AF");
            if (afEntry == null)
            {
                Console.WriteLine("No associated files (AF entry) found in the PDF.");
                return;
            }

            // The AF entry is an array; iterate to find the XML file.
            // The exact type depends on the library version; we use dynamic to avoid compile‑time type issues.
            // In practice you would cast to Aspose.Pdf.FileSpecification[] or similar.
            dynamic afArray = afEntry;
            bool xmlFound = false;

            foreach (var fileSpec in afArray)
            {
                // Each file specification has a FileName property.
                // We look for a file ending with ".xml" (ZUGFeRD convention).
                string fileName = fileSpec.FileName as string;
                if (!string.IsNullOrEmpty(fileName) && fileName.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
                {
                    // The embedded file stream is accessible via the EmbeddedFile property.
                    // Again we use dynamic to keep the code generic.
                    Stream xmlStream = fileSpec.EmbeddedFile as Stream;
                    if (xmlStream != null)
                    {
                        // Save the extracted XML to disk.
                        using (FileStream outStream = new FileStream(xmlOutputPath, FileMode.Create, FileAccess.Write))
                        {
                            xmlStream.CopyTo(outStream);
                        }

                        Console.WriteLine($"Extracted XML saved to: {xmlOutputPath}");
                        xmlFound = true;

                        // Example processing: read the XML content into a string.
                        string xmlContent = File.ReadAllText(xmlOutputPath);
                        // TODO: add your XML processing logic here (e.g., deserialize, validate, etc.).
                        Console.WriteLine("XML content preview (first 200 chars):");
                        Console.WriteLine(xmlContent.Substring(0, Math.Min(200, xmlContent.Length)));
                    }
                    break;
                }
            }

            if (!xmlFound)
            {
                Console.WriteLine("No embedded XML invoice found in the PDF.");
            }
        }
    }
}