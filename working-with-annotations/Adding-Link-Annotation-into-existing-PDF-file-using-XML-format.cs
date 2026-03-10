using System;
using System.IO;
using Aspose.Pdf;                     // Core PDF API
using Aspose.Pdf.Annotations;        // For annotation types (if needed)

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";   // Existing PDF
        const string xfdfPath      = "link_annotation.xfdf"; // XFDF (XML) defining the link annotation
        const string outputPdfPath = "output.pdf";  // Resulting PDF with the imported link

        // Verify files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine($"XFDF file not found: {xfdfPath}");
            return;
        }

        // Load the PDF, import the XFDF annotations, and save the result.
        // Document implements IDisposable, so wrap it in a using block (lifecycle rule).
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Import annotations from the XFDF (XML) file.
            // This method adds the link annotation defined in the XFDF to the PDF.
            pdfDoc.ImportAnnotationsFromXfdf(xfdfPath);

            // Save the updated PDF. No SaveOptions are needed because we keep PDF format.
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Link annotation imported and saved to '{outputPdfPath}'.");
    }
}