using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the PDF form, the XML data file, and the output PDF
        const string pdfPath = "form.pdf";
        const string xmlPath = "data.xml";
        const string outputPath = "filled_form.pdf";

        // Verify that input files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Open the XML file as a stream
        using (FileStream xmlStream = new FileStream(xmlPath, FileMode.Open, FileAccess.Read))
        {
            // Initialize the Form facade with the target PDF
            Form form = new Form(pdfPath);

            // Import the XML data into the PDF form fields
            form.ImportXml(xmlStream);

            // Collect any required‑field violations
            List<string> violations = new List<string>();

            // Iterate over all field names in the form
            foreach (string fieldName in form.FieldNames)
            {
                // Check whether the field is marked as required
                if (form.IsRequiredField(fieldName))
                {
                    // Retrieve the current value of the field
                    string value = form.GetField(fieldName);

                    // Empty or whitespace values constitute a violation
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        violations.Add($"Required field '{fieldName}' is empty.");
                    }
                }
            }

            // Output validation results
            if (violations.Count > 0)
            {
                Console.WriteLine("Validation violations detected:");
                foreach (string v in violations)
                {
                    Console.WriteLine(v);
                }
            }
            else
            {
                Console.WriteLine("All required fields are populated.");
            }

            // Save the updated PDF with imported data
            form.Save(outputPath);
        }
    }
}