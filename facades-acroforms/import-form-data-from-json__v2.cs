using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";      // source PDF with form fields
        const string jsonPath = "data.json";    // JSON file containing form values
        const string outputPdf = "filled_output.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"JSON not found: {jsonPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Use the Facade Form to import JSON data
            using (Aspose.Pdf.Facades.Form pdfForm = new Aspose.Pdf.Facades.Form())
            {
                pdfForm.BindPdf(pdfDoc);

                // Import JSON from a stream; missing fields are ignored automatically
                using (FileStream jsonStream = File.OpenRead(jsonPath))
                {
                    pdfForm.ImportJson(jsonStream);
                }
            }

            // Save the updated PDF
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"Form data imported and saved to '{outputPdf}'.");
    }
}
