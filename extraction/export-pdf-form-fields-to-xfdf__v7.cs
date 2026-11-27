using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "FormE.pdf";
        const string outputPath = "FormE_fields.xfdf";

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
                foreach (var field in doc.Form.Fields)
                {
                    Console.WriteLine($"Field Name: {field.FullName}, Value: {field.Value}");
                }

                // Export the form field data to XFDF (custom XML generation – XfdfSaveOptions does not exist)
                var xfdf = new XDocument(
                    new XElement("xfdf",
                        new XElement("fields",
                            doc.Form.Fields.Select(f =>
                                new XElement("field",
                                    new XAttribute("name", f.FullName),
                                    new XAttribute("value", f.Value?.ToString() ?? string.Empty)
                                )
                            )
                        )
                    )
                );

                xfdf.Save(outputPath);
            }

            Console.WriteLine($"Form data exported to XFDF: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
