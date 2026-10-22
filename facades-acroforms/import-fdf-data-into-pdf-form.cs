using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string fdfPath = "data.fdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(fdfPath))
        {
            Console.Error.WriteLine($"FDF not found: {fdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Create a Form facade bound to the loaded document
            using (Form formFacade = new Form(pdfDoc))
            {
                // Open the FDF file as a stream
                using (FileStream fdfStream = File.OpenRead(fdfPath))
                {
                    // Import form data from the FDF stream into the PDF
                    formFacade.ImportFdf(fdfStream);
                }
            }

            // Save the updated PDF document
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"Form data imported and saved to '{outputPath}'.");
    }
}
