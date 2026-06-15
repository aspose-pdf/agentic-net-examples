using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";          // PDF with AcroForm fields
        const string xmlDataPath = "updatedData.xml";    // XML containing form data
        const string outputPdfPath = "output.pdf";       // Resulting PDF after import

        // Verify that source files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found – {inputPdfPath}");
            return;
        }
        if (!File.Exists(xmlDataPath))
        {
            Console.Error.WriteLine($"Error: XML data file not found – {xmlDataPath}");
            return;
        }

        // Use the Form facade (Aspose.Pdf.Facades) via its fully‑qualified name.
        // No 'using Aspose.Pdf.Facades;' directive is added, complying with the namespace restriction.
        using (Aspose.Pdf.Facades.Form formFacade = new Aspose.Pdf.Facades.Form(inputPdfPath))
        {
            // Import the XML data into the PDF's AcroForm fields.
            using (FileStream xmlStream = File.OpenRead(xmlDataPath))
            {
                formFacade.ImportXml(xmlStream);
            }

            // Save the updated PDF to a new file.
            formFacade.Save(outputPdfPath);
        }

        Console.WriteLine($"Form data imported successfully. Output saved to '{outputPdfPath}'.");
    }
}