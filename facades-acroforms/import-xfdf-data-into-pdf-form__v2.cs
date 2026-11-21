using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";   // source PDF with form fields
        const string xfdfPath = "data.xfdf"; // XFDF file containing field values
        const string outputPath = "output.pdf"; // PDF after importing data

        // Verify that the required files exist
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

        // Load the PDF document and bind it to the Form facade
        using (Document doc = new Document(pdfPath))
        using (Form formFacade = new Form(doc))
        {
            // Import form field values from the XFDF file into the PDF.
            // The ImportXfdf overload expects a Stream, so open the file as a stream.
            using (FileStream xfdfStream = File.OpenRead(xfdfPath))
            {
                formFacade.ImportXfdf(xfdfStream);
            }

            // Save the updated PDF to the specified output path.
            formFacade.Save(outputPath);
        }

        Console.WriteLine($"Form data imported and saved to '{outputPath}'.");
    }
}
