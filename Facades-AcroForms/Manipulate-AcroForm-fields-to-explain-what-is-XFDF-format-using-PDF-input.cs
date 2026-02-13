using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths to the source PDF, the XFDF file and the output PDF.
        string pdfPath = "input.pdf";
        string xfdfPath = "data.xfdf";
        string outputPath = "output.pdf";

        // Verify that the required files exist.
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

        // Load the PDF document. If it contains an AcroForm, its fields can be populated.
        Document pdfDoc = new Document(pdfPath);

        // XFDF (XML Forms Data Format) is an XML representation of form field values
        // and annotations. It can be imported into a PDF to fill its AcroForm fields.
        using (FileStream xfdfStream = File.OpenRead(xfdfPath))
        {
            // Import field values from the XFDF file into the PDF document.
            XfdfReader.ReadFields(xfdfStream, pdfDoc);
        }

        // Save the updated PDF with the imported field values.
        pdfDoc.Save(outputPath);
        Console.WriteLine($"PDF saved with XFDF data to '{outputPath}'.");
    }
}