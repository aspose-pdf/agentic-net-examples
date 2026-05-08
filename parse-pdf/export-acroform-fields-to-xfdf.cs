using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class ExportAcroFormToXfdf
{
    static void Main()
    {
        // Path to the source PDF containing AcroForm fields
        const string pdfPath = "input.pdf";

        // Path where the XFDF file will be written
        const string xfdfPath = "output.xfdf";

        // Ensure the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            // Create a FileStream for the XFDF output (write mode, create or overwrite)
            using (FileStream xfdfStream = new FileStream(xfdfPath, FileMode.Create, FileAccess.Write))
            {
                // Export all annotations (including AcroForm widget annotations) to XFDF
                // The ExportAnnotationsToXfdf method accepts a Stream, so we pass the FileStream.
                doc.ExportAnnotationsToXfdf(xfdfStream);
            }

            // Optionally, you can also access the Form object if needed
            Form acroForm = doc.Form;
            // Example: list the number of form fields
            Console.WriteLine($"Number of AcroForm fields: {acroForm.Count}");
        }

        Console.WriteLine($"AcroForm fields exported to XFDF: {xfdfPath}");
    }
}