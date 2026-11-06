using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string pdfPath    = "input.pdf";
        const string xfdfPath   = "annotations.xfdf";
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
            // Load the PDF from a FileStream (no temporary files)
            using (FileStream pdfStream = File.OpenRead(pdfPath))
            using (Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document(pdfStream))
            {
                // Load the XFDF annotations from a stream
                using (FileStream xfdfStream = File.OpenRead(xfdfPath))
                {
                    // Import annotations directly from the XFDF stream
                    pdfDoc.ImportAnnotationsFromXfdf(xfdfStream);
                }

                // Save the updated PDF
                pdfDoc.Save(outputPath);
            }

            Console.WriteLine($"Annotations imported and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}