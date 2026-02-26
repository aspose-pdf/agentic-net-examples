using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths to the source PCL file, the FDF annotation file, and the output PDF.
        const string pclPath      = "source.pcl";
        const string fdfPath      = "annotations.fdf";
        const string outputPdfPath = "annotated_output.pdf";

        // Verify that input files exist.
        if (!File.Exists(pclPath))
        {
            Console.Error.WriteLine($"PCL file not found: {pclPath}");
            return;
        }
        if (!File.Exists(fdfPath))
        {
            Console.Error.WriteLine($"FDF file not found: {fdfPath}");
            return;
        }

        // Load the PCL file and convert it to a PDF document.
        // PclLoadOptions tells Aspose.Pdf that the source format is PCL.
        using (Document pdfDoc = new Document(pclPath, new PclLoadOptions()))
        {
            // Import annotations from the FDF file into the PDF document.
            using (FileStream fdfStream = File.OpenRead(fdfPath))
            {
                // FdfReader reads FDF annotations and adds them to the document.
                FdfReader.ReadAnnotations(fdfStream, pdfDoc);
            }

            // Save the resulting PDF with the imported annotations.
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Annotations imported and PDF saved to '{outputPdfPath}'.");
    }
}