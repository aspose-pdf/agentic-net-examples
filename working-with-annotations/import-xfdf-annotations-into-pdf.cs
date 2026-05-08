using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string xfdfPath = "annotations.xfdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine($"XFDF not found: {xfdfPath}");
            return;
        }

        try
        {
            // Load the existing PDF document
            using (Document doc = new Document(pdfPath))
            {
                // Import annotations from the XFDF file.
                // This operation adds the annotations while keeping the original page layout and content intact.
                doc.ImportAnnotationsFromXfdf(xfdfPath);

                // Save the modified PDF to a new file.
                doc.Save(outputPath);
            }

            Console.WriteLine($"Annotations imported successfully. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}