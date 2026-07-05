using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document (lifecycle: load)
        using (Document doc = new Document(inputPdf))
        {
            // Create a memory stream that will receive the XFDF data
            using (MemoryStream xfdfStream = new MemoryStream())
            {
                // Export all annotations from the document into the stream
                doc.ExportAnnotationsToXfdf(xfdfStream);

                // Reset the stream position so it can be read from the beginning
                xfdfStream.Position = 0;

                // OPTIONAL: write the XFDF content to a file for verification
                const string outputXfdf = "annotations.xfdf";
                using (FileStream file = new FileStream(outputXfdf, FileMode.Create, FileAccess.Write))
                {
                    xfdfStream.CopyTo(file);
                }

                Console.WriteLine($"Annotations exported to '{outputXfdf}'.");
            }
        }
    }
}