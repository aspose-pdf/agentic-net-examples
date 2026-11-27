using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "FormD.pdf";
        const string xfdfPath  = "FormD_fields.xfdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Enumerate all form fields and output their names and values
                foreach (Field field in doc.Form)
                {
                    Console.WriteLine($"Field Name: {field.FullName}, Value: {field.Value}");
                }

                // Export the form fields (as annotations) to an XFDF file using a stream
                using (FileStream xfdfStream = new FileStream(xfdfPath, FileMode.Create, FileAccess.Write))
                {
                    doc.ExportAnnotationsToXfdf(xfdfStream);
                }

                Console.WriteLine($"Form data exported to {xfdfPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
