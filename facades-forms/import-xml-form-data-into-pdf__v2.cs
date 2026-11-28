using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string xmlDataPath = "formdata.xml";
        const string outputPdfPath = "output.pdf";

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

        try
        {
            // Form is a Facades class that implements IDisposable – wrap in using for deterministic cleanup
            using (var form = new Form())
            {
                // Bind the existing PDF document to the Form object
                form.BindPdf(inputPdfPath);

                // Import form field values from the XML file using a stream (ImportXml, not ImportXmlData)
                using (FileStream xmlStream = new FileStream(xmlDataPath, FileMode.Open, FileAccess.Read))
                {
                    form.ImportXml(xmlStream);
                }

                // Save the modified PDF with populated form fields
                form.Save(outputPdfPath);
            }

            Console.WriteLine($"Form data imported and PDF saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
