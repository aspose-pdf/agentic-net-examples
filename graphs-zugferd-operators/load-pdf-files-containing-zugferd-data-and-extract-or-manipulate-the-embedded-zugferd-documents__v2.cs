using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the PDF that contains ZUGFeRD data
        const string inputPdf = "invoice.pdf";

        // Folder where extracted ZUGFeRD XML files will be saved
        const string outputDir = "ZugFeRD_Extracted";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the PDF document (lifecycle rule: wrap in using)
            using (Document doc = new Document(inputPdf))
            {
                // -----------------------------------------------------------------
                // Extract embedded files (ZUGFeRD XML is stored as an embedded file)
                // -----------------------------------------------------------------
                if (doc.EmbeddedFiles.Count == 0)
                {
                    Console.WriteLine("No embedded files found in the PDF.");
                }
                else
                {
                    // EmbeddedFiles collection uses 1‑based indexing (global rule)
                    for (int i = 1; i <= doc.EmbeddedFiles.Count; i++)
                    {
                        // In Aspose.Pdf the collection holds FileSpecification objects
                        FileSpecification spec = doc.EmbeddedFiles[i];
                        string outPath = Path.Combine(outputDir, spec.Name);

                        // Save the embedded file to disk
                        using (FileStream fs = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                        {
                            // The Contents stream holds the actual file data
                            spec.Contents.CopyTo(fs);
                        }

                        Console.WriteLine($"Extracted embedded file: {outPath}");
                    }
                }

                // ---------------------------------------------------------------
                // Example manipulation: replace the ZUGFeRD XML with a modified version
                // ---------------------------------------------------------------
                const string modifiedXmlPath = "modified.xml";

                if (File.Exists(modifiedXmlPath))
                {
                    // Remove the first embedded file (if any) before adding the new one
                    if (doc.EmbeddedFiles.Count > 0)
                    {
                        // Delete expects the attachment name, not an index
                        FileSpecification firstSpec = doc.EmbeddedFiles[1];
                        doc.EmbeddedFiles.Delete(firstSpec.Name);
                    }

                    // Add the new XML as an embedded file
                    using (FileStream xmlStream = File.OpenRead(modifiedXmlPath))
                    {
                        // NOTE: The FileSpecification constructor expects (Stream, string)
                        // where the first argument is the file content stream and the second
                        // is the name that will appear inside the PDF.
                        var newSpec = new FileSpecification(xmlStream, "ZUGFeRD-invoice.xml");
                        doc.EmbeddedFiles.Add(newSpec);
                    }

                    // Save the updated PDF (still a PDF, so no SaveOptions needed)
                    string updatedPdfPath = Path.Combine(outputDir, "invoice_updated.pdf");
                    doc.Save(updatedPdfPath);
                    Console.WriteLine($"Updated PDF saved to: {updatedPdfPath}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
