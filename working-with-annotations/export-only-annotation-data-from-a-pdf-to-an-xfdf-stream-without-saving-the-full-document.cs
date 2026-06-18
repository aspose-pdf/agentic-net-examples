using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputXfdfPath = "annotations.xfdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create a memory stream to hold the XFDF data
            using (MemoryStream xfdfStream = new MemoryStream())
            {
                // Export all annotations from the document into the stream
                pdfDoc.ExportAnnotationsToXfdf(xfdfStream);

                // Reset the stream position before reading or writing it elsewhere
                xfdfStream.Position = 0;

                // Write the XFDF stream to a file (optional, demonstrates usage)
                using (FileStream file = new FileStream(outputXfdfPath, FileMode.Create, FileAccess.Write))
                {
                    xfdfStream.CopyTo(file);
                }

                Console.WriteLine($"Annotations exported to '{outputXfdfPath}'.");
            }
        }
    }
}