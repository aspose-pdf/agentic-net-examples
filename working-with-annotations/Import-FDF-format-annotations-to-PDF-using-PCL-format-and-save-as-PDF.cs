using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string pclPath = "input.pcl";
        const string fdfPath = "annotations.fdf";
        const string outputPdf = "output.pdf";

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

        // Load the PCL file as a PDF document using PclLoadOptions
        PclLoadOptions pclLoadOptions = new PclLoadOptions();
        using (Document pdfDoc = new Document(pclPath, pclLoadOptions))
        {
            // Import annotations from the FDF file into the PDF document
            using (FileStream fdfStream = File.OpenRead(fdfPath))
            {
                FdfReader.ReadAnnotations(fdfStream, pdfDoc);
            }

            // Save the resulting document as a PDF file
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with imported annotations saved to '{outputPdf}'.");
    }
}