using System;
using System.IO;
using System.Collections.Generic;
using System.Xml;
using Aspose.Pdf.Facades;          // Facade classes for form manipulation
using Aspose.Pdf.Annotations;    // XfdfReader to parse XFDF content

class Program
{
    static void Main()
    {
        // Input PDF file that contains AcroForm fields
        const string pdfPath = "input.pdf";

        // Output XFDF file that will hold the exported form data
        const string xfdfPath = "output.xfdf";

        // Verify that the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{pdfPath}'.");
            return;
        }

        try
        {
            // -----------------------------------------------------------------
            // 1. Bind the PDF to the Form facade.
            // -----------------------------------------------------------------
            using (Form form = new Form())
            {
                form.BindPdf(pdfPath);

                // -----------------------------------------------------------------
                // 2. Export the AcroForm fields to XFDF (XML Forms Data Format).
                // -----------------------------------------------------------------
                using (FileStream xfdfStream = File.Create(xfdfPath))
                {
                    form.ExportXfdf(xfdfStream);
                }
            }

            // -----------------------------------------------------------------
            // 3. Read the generated XFDF file and display its content.
            //    XFDF is an XML representation of the form field values.
            // -----------------------------------------------------------------
            string xfdfContent = File.ReadAllText(xfdfPath);
            Console.WriteLine("=== XFDF Export ===");
            Console.WriteLine(xfdfContent);
            Console.WriteLine("===================\n");

            // -----------------------------------------------------------------
            // 4. (Optional) Parse the XFDF using XfdfReader to show field names
            //    and their exported values.
            // -----------------------------------------------------------------
            using (FileStream readStream = File.OpenRead(xfdfPath))
            using (XmlReader xmlReader = XmlReader.Create(readStream))
            {
                // GetElements now returns IDictionary<string, string>
                var fields = XfdfReader.GetElements(xmlReader);

                Console.WriteLine("Parsed XFDF fields:");
                foreach (KeyValuePair<string, string> kvp in fields)
                {
                    Console.WriteLine($"  Field: {kvp.Key}  Value: {kvp.Value}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }
}
