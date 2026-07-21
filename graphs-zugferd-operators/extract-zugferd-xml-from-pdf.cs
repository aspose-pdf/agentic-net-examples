using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "invoice.pdf";          // input PDF containing ZUGFeRD XML
        const string xmlOutput = "ZUGFeRD.xml";        // file to save the extracted XML

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {pdfPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document pdfDoc = new Document(pdfPath))
            {
                bool extracted = false;

                // Access embedded files via the EmbeddedFiles collection
                if (pdfDoc.EmbeddedFiles != null)
                {
                    foreach (FileSpecification fileSpec in pdfDoc.EmbeddedFiles)
                    {
                        // ZUGFeRD data is stored as an XML attachment (commonly ends with .xml)
                        if (!string.IsNullOrEmpty(fileSpec.Name) &&
                            fileSpec.Name.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
                        {
                            // Ensure the stream is positioned at the beginning
                            if (fileSpec.Contents.CanSeek)
                                fileSpec.Contents.Position = 0;

                            // Write the XML stream to the desired output file
                            using (FileStream outStream = new FileStream(xmlOutput, FileMode.Create, FileAccess.Write))
                            {
                                fileSpec.Contents.CopyTo(outStream);
                            }

                            Console.WriteLine($"Extracted ZUGFeRD XML to '{xmlOutput}'.");
                            extracted = true;
                            break; // assuming only one ZUGFeRD XML attachment
                        }
                    }
                }

                if (!extracted)
                {
                    Console.WriteLine("No XML attachment (ZUGFeRD) found in the PDF.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during extraction: {ex.Message}");
        }
    }
}
