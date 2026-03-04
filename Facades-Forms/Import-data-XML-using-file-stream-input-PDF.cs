using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";   // source PDF with form fields
        const string xmlPath      = "data.xml";    // XML file containing field values
        const string outputPdfPath = "output.pdf"; // PDF after XML import

        // Verify that the required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML not found: {xmlPath}");
            return;
        }

        try
        {
            // Form facade: first argument = source PDF, second argument = destination PDF
            using (Form form = new Form(inputPdfPath, outputPdfPath))
            {
                // Open the XML file as a stream and import its data into the form
                using (FileStream xmlStream = new FileStream(xmlPath, FileMode.Open, FileAccess.Read))
                {
                    form.ImportXml(xmlStream);
                }

                // Save the modified PDF (destination path was supplied in the constructor)
                form.Save();
            }

            Console.WriteLine($"XML data imported successfully. Output saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}