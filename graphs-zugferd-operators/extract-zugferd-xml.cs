using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF that contains a ZUGFeRD XML attachment
        // Resolve the path relative to the executable directory to avoid FileNotFoundException
        string inputPdf = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "invoice.pdf");
        // Output XML file name (saved next to the executable)
        string outputXml = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "invoice.xml");

        if (!File.Exists(inputPdf))
        {
            Console.WriteLine($"Error: Could not find the input PDF file at '{inputPdf}'." );
            Console.WriteLine("Make sure the file exists and the path is correct.");
            return;
        }

        using (Document pdfDocument = new Document(inputPdf))
        {
            // Ensure the PDF has an EmbeddedFiles collection
            if (pdfDocument.EmbeddedFiles != null && pdfDocument.EmbeddedFiles.Count > 0)
            {
                // Evaluation mode allows at most 4 items in a collection
                int maxItems = Math.Min(pdfDocument.EmbeddedFiles.Count, 4);
                bool extracted = false;

                for (int i = 1; i <= maxItems; i++) // Aspose collections are 1‑based
                {
                    FileSpecification fileSpec = pdfDocument.EmbeddedFiles[i];
                    // Identify the ZUGFeRD XML by MIME type or file extension
                    bool isXml = false;
                    if (!string.IsNullOrEmpty(fileSpec.MIMEType) &&
                        fileSpec.MIMEType.Equals("application/xml", StringComparison.OrdinalIgnoreCase))
                    {
                        isXml = true;
                    }
                    else if (!string.IsNullOrEmpty(fileSpec.Name) &&
                             fileSpec.Name.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
                    {
                        isXml = true;
                    }

                    if (isXml)
                    {
                        // The actual file data is stored in the Contents stream of FileSpecification
                        if (fileSpec.Contents != null)
                        {
                            using (Stream xmlStream = fileSpec.Contents)
                            using (FileStream fileStream = new FileStream(outputXml, FileMode.Create, FileAccess.Write))
                            {
                                xmlStream.CopyTo(fileStream);
                            }
                            Console.WriteLine($"ZUGFeRD XML extracted to '{outputXml}'.");
                            extracted = true;
                        }
                        else
                        {
                            Console.WriteLine("Attachment found but it does not contain a contents stream.");
                        }
                        break; // stop after first matching XML file
                    }
                }

                if (!extracted)
                {
                    Console.WriteLine("No ZUGFeRD XML attachment found in the PDF.");
                }
            }
            else
            {
                Console.WriteLine("The PDF does not contain any embedded files.");
            }
        }
    }
}
