using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string xmlData = "data.xml";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(xmlData))
        {
            Console.Error.WriteLine($"XML data file not found: {xmlData}");
            return;
        }

        try
        {
            // Instantiate Form with only the source PDF (no destination).
            using (Form form = new Form(inputPdf))
            {
                using (FileStream xmlStream = new FileStream(xmlData, FileMode.Open, FileAccess.Read))
                {
                    try
                    {
                        // Import the XML data into the form.
                        form.ImportXml(xmlStream);
                    }
                    catch (Exception ex)
                    {
                        // Aspose.Pdf may throw a generic Exception when a field referenced in the XML does not exist.
                        // Attempt to read the optional FieldName property via reflection to log the missing field.
                        var fieldProp = ex.GetType().GetProperty("FieldName");
                        if (fieldProp != null)
                        {
                            var missingField = fieldProp.GetValue(ex) as string;
                            Console.Error.WriteLine($"Missing form field: {missingField ?? "<unknown>"}");
                        }
                        else
                        {
                            Console.Error.WriteLine($"Form import error: {ex.Message}");
                        }
                    }
                }

                // Save the modified PDF to the desired output path using the non‑obsolete overload.
                form.Save(outputPdf);
                Console.WriteLine($"Form fields imported and saved to {outputPdf}");
            }
        }
        catch (Exception ex)
        {
            // Catch any other unexpected errors.
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}
