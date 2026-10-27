using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";   // existing PDF with form fields
        const string fdfPath = "checkboxes.fdf"; // FDF containing checkbox definitions
        const string outputPdfPath = "merged_output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(fdfPath))
        {
            Console.Error.WriteLine($"FDF not found: {fdfPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Bind the PDF to the Form facade
            using (Form pdfForm = new Form())
            {
                pdfForm.BindPdf(pdfDoc);

                // Import the FDF data. ImportFdf expects a Stream, so we open the file as a FileStream.
                using (FileStream fdfStream = new FileStream(fdfPath, FileMode.Open, FileAccess.Read))
                {
                    pdfForm.ImportFdf(fdfStream);
                }
            }

            // Save the updated PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Merged PDF saved to '{outputPdfPath}'.");
    }
}
