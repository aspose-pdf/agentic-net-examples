using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputXfdf = "output.xfdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Create a FileStream to write the XFDF data
            using (FileStream xfdfStream = new FileStream(outputXfdf, FileMode.Create, FileAccess.Write))
            {
                // Export all annotations (including AcroForm fields) to the XFDF stream
                doc.ExportAnnotationsToXfdf(xfdfStream);
            }
        }

        Console.WriteLine($"XFDF file created at '{outputXfdf}'.");
    }
}