using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the source PDF, the XML data to import, and the output PDF
        string inputPdfPath = "input.pdf";
        string importXmlPath = "data.xml";
        string outputPdfPath = "output.pdf";

        // Verify that the required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(importXmlPath))
        {
            Console.Error.WriteLine($"Import XML not found: {importXmlPath}");
            return;
        }

        // Create a Form object using only the source PDF path
        using (Form form = new Form(inputPdfPath))
        {
            try
            {
                // Open the XML stream and import the form data
                using (FileStream importStream = File.OpenRead(importXmlPath))
                {
                    form.ImportXml(importStream);
                }
            }
            catch (Exception ex)
            {
                // Aspose may throw a FormException that contains a "FieldName" property
                var fieldProp = ex.GetType().GetProperty("FieldName");
                if (fieldProp != null)
                {
                    string fieldName = fieldProp.GetValue(ex) as string;
                    Console.Error.WriteLine($"Missing form field: {fieldName}");
                }
                else
                {
                    // Log any other import errors
                    Console.Error.WriteLine($"Error importing form data: {ex.Message}");
                }
            }

            // Save the modified PDF to the destination path
            form.Save(outputPdfPath);
        }

        Console.WriteLine($"Form data imported and saved to '{outputPdfPath}'.");
    }
}