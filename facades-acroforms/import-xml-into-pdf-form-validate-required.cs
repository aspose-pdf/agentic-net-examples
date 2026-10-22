using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string xmlDataPath = "data.xml";
        const string outputPath = "output.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(xmlDataPath))
        {
            Console.Error.WriteLine($"XML data not found: {xmlDataPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document pdfDoc = new Document(pdfPath))
            {
                // Bind the PDF to the Form facade and import XML data
                using (Form formFacade = new Form(pdfDoc))
                {
                    // Import form data from the XML file – Form.ImportXml expects a Stream
                    using (FileStream xmlStream = File.OpenRead(xmlDataPath))
                    {
                        formFacade.ImportXml(xmlStream);
                    }
                }

                // Validate required fields
                bool hasViolations = false;
                var fields = pdfDoc.Form?.Fields;
                if (fields != null)
                {
                    foreach (var field in fields)
                    {
                        // Determine if the field is marked as required (via reflection, as not all field types expose it directly)
                        bool isRequired = false;
                        var requiredProp = field.GetType().GetProperty("Required");
                        if (requiredProp != null && requiredProp.PropertyType == typeof(bool))
                        {
                            var propValue = requiredProp.GetValue(field);
                            if (propValue != null)
                                isRequired = (bool)propValue;
                        }

                        if (isRequired)
                        {
                            string value = field.Value?.ToString() ?? string.Empty;
                            if (string.IsNullOrWhiteSpace(value))
                            {
                                hasViolations = true;
                                Console.WriteLine($"Validation error: Required field '{field.Name}' is empty.");
                            }
                        }
                    }
                }

                if (!hasViolations)
                {
                    Console.WriteLine("All required fields are filled.");
                }

                // Save the updated PDF
                pdfDoc.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
