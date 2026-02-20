using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the source PDF that contains AcroForm fields.
        const string pdfPath = "input.pdf";

        // Path where the exported XML representation of the form will be saved.
        const string xmlPath = "form_fields.xml";

        // Verify that the PDF file exists before proceeding.
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{pdfPath}'.");
            return;
        }

        try
        {
            // Create a Form facade instance. This class provides methods to work with AcroForm fields.
            using (Form form = new Form())
            {
                // Bind the PDF document to the Form facade.
                form.BindPdf(pdfPath);

                // Export the form fields to XML. The XML format is a plain‑text representation
                // where each field is described by an element containing its name, type and value.
                // This makes it easy to inspect, edit, or exchange form data with other systems.
                using (FileStream xmlStream = new FileStream(xmlPath, FileMode.Create, FileAccess.Write))
                {
                    form.ExportXml(xmlStream);
                }

                Console.WriteLine($"Form fields have been exported to XML file: {xmlPath}");
            }

            // Optional: display the generated XML content on the console.
            Console.WriteLine("\n--- Exported XML Content ---");
            Console.WriteLine(File.ReadAllText(xmlPath));
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}