using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths for input MHT, intermediate XFDF, and final PDF
        const string mhtInputPath   = "input.mht";
        const string xfdfPath       = "annotations.xfdf";
        const string pdfOutputPath  = "output.pdf";

        // Verify the MHT file exists
        if (!File.Exists(mhtInputPath))
        {
            Console.Error.WriteLine($"MHT file not found: {mhtInputPath}");
            return;
        }

        // Load the MHT file into a PDF document using MhtLoadOptions
        MhtLoadOptions loadOptions = new MhtLoadOptions();
        using (Document pdfDoc = new Document(mhtInputPath, loadOptions))
        {
            // Export all existing annotations to an XFDF file
            pdfDoc.ExportAnnotationsToXfdf(xfdfPath);

            // (Optional) At this point you could modify the XFDF file externally.
            // For demonstration, we import the same XFDF back into the document.
            pdfDoc.ImportAnnotationsFromXfdf(xfdfPath);

            // Save the resulting document as a PDF file
            pdfDoc.Save(pdfOutputPath);
        }

        Console.WriteLine($"MHT converted to PDF with annotations exported/imported via XFDF.\nOutput: {pdfOutputPath}");
    }
}