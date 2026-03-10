using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string ofdPath = "input.ofd";          // OFD source file
        const string xfdfPath = "annotations.xfdf"; // temporary XFDF file
        const string pdfPath = "output.pdf";        // final PDF output

        if (!File.Exists(ofdPath))
        {
            Console.Error.WriteLine($"File not found: {ofdPath}");
            return;
        }

        // Load OFD using the appropriate load options
        using (Document doc = new Document(ofdPath, new OfdLoadOptions()))
        {
            // Export all annotations to XFDF file
            doc.ExportAnnotationsToXfdf(xfdfPath);

            // (Optional) modify the XFDF file here if needed

            // Import the XFDF annotations back into the document
            doc.ImportAnnotationsFromXfdf(xfdfPath);

            // Save the document as PDF
            doc.Save(pdfPath);
        }

        Console.WriteLine($"OFD converted to PDF with annotations. Output saved to '{pdfPath}'.");
    }
}