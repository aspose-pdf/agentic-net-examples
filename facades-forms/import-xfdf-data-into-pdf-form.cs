using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath   = "input.pdf";   // existing PDF with form fields
        const string xfdfPath  = "data.xfdf";   // XFDF file containing field values
        const string outputPdf = "output.pdf";  // PDF after importing XFDF data

        // Verify input files exist
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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(pdfPath))
        {
            // The Form facade (Aspose.Pdf.Facades.Form) handles XFDF import/export.
            // It works with streams, not file paths.
            using (Form formFacade = new Form(pdfDoc))
            {
                // Import field values from the XFDF file. Matching field names are populated automatically.
                using (FileStream xfdfStream = File.OpenRead(xfdfPath))
                {
                    formFacade.ImportXfdf(xfdfStream);
                }

                // Save the updated PDF to a new file
                formFacade.Save(outputPdf);
            }
        }

        Console.WriteLine($"XFDF data imported successfully to '{outputPdf}'.");
    }
}
