using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";   // source PDF with form fields
        const string xmlPath       = "fields.xml"; // XML containing field values
        const string outputPdfPath = "output.pdf"; // result PDF

        // Verify that required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found – {inputPdfPath}");
            return;
        }
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"Error: XML file not found – {xmlPath}");
            return;
        }

        // Use the Form facade to work with AcroForm data
        using (Form form = new Form())
        {
            // Bind the source PDF document
            form.BindPdf(inputPdfPath);

            // Import field values from the XML stream.
            // The second argument (false) ensures that template changes are NOT ignored,
            // which keeps the original field order intact.
            using (FileStream xmlStream = new FileStream(xmlPath, FileMode.Open, FileAccess.Read))
            {
                form.ImportXml(xmlStream, false);
            }

            // Save the updated PDF with imported field values
            form.Save(outputPdfPath);
        }

        Console.WriteLine($"Form fields imported successfully. Output saved to '{outputPdfPath}'.");
    }
}