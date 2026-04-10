using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string xfdfPath = "annotations.xfdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Create a FileStream to write XFDF data
            using (FileStream xfdfStream = new FileStream(xfdfPath, FileMode.Create, FileAccess.Write))
            {
                // Export all annotations to the XFDF stream
                doc.ExportAnnotationsToXfdf(xfdfStream);
                // The using block ensures the stream is closed
            }

            // No changes to the PDF are made, so saving is optional
            // doc.Save("output.pdf");
        }

        Console.WriteLine($"Annotations exported to '{xfdfPath}'.");
    }
}