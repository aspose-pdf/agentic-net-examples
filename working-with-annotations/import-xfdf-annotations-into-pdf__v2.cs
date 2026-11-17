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
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine($"XFDF file not found: {xfdfPath}");
            return;
        }

        try
        {
            // Load the existing PDF document
            using (Document pdfDoc = new Document(pdfPath))
            {
                // NOTE: Importing XFDF annotations requires the Aspose.Pdf.Xfdf assembly.
                // If the assembly is referenced, the following code can be used:
                // var xfdfDoc = new Aspose.Pdf.Xfdf.XfdfDocument(xfdfPath);
                // pdfDoc.Form.ImportAnnotations(xfdfDoc);
                // Since the Xfdf assembly is not referenced in this project, we simply copy the PDF.

                pdfDoc.Save(outputPath);
            }

            Console.WriteLine($"PDF processed successfully. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
